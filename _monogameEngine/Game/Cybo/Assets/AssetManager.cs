using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.IO;
using System;

namespace Cybo;

/// <summary>
/// A class used for loading assets on a background thread.
/// </summary>
/// <typeparam name="Texture"></typeparam>
internal class AssetManager
{
    enum AssetStatus
    {
        Queued,
        Loaded,
    }

    struct BackgroundMsg
    {
        public bool shouldExit;
        public (AssetType, string)? Asset;
    }

    struct AssetMsg
    {
        public string Name;
        public Stream? Asset;
        public AssetType Type;
        public Exception? Error;
    }

    struct TextureHandle
    {
        public AssetStatus Status;
        public Texture2d? Asset;
    }

    ConcurrentQueue<AssetMsg> _assetInbox;
    ConcurrentQueue<BackgroundMsg> _backgroundInbox;
    Dictionary<string, TextureHandle> _textures = new Dictionary<string, TextureHandle>();
    Thread _backgroundThread;


    public AssetManager()
    {
        _backgroundInbox = new ConcurrentQueue<BackgroundMsg>();
        _assetInbox = new ConcurrentQueue<AssetMsg>();

        // Create a background thread for loading assets.
        _backgroundThread = new Thread(() =>
        {
            var sleepDurationMs = 1;
            var maxSleepDurationMs = 16;
            var shouldExit = false;

            while (!shouldExit)
            {

                if (_backgroundInbox.TryDequeue(out var msg))
                {
                    shouldExit = msg.shouldExit;
                    sleepDurationMs = 1;

                    // Load asset if present
                    if (msg.Asset != null)
                    {
                        var (assetType, filePath) = msg.Asset.Value;
                        var path = $"Content/{filePath}";
                        Stream? asset = null;
                        Exception? error = null;

                        try
                        {
                            asset = new System.IO.FileStream(path, System.IO.FileMode.Open);
                        }
                        catch (Exception ex)
                        {
                            error = ex;
                        }

                        _assetInbox.Enqueue(new AssetMsg
                        {
                            Asset = asset,
                            Type = assetType,
                            Name = filePath,
                            Error = error
                        });
                    }
                }
                // Sleep thread using exponential back off
                else
                {
                    sleepDurationMs *= 2;
                    sleepDurationMs = sleepDurationMs > maxSleepDurationMs ? maxSleepDurationMs : sleepDurationMs;
                    Thread.Sleep(sleepDurationMs);
                }
            }
        });

        _backgroundThread.Start();
    }

    /// <summary>
    /// Kills the asset manager. Stops the background thread that loads files.
    /// </summary>
    public void ShutDown()
    {
        _backgroundInbox.Enqueue(new BackgroundMsg { shouldExit = true });
    }

    /// <summary>
    /// Checks to see if there are any newly loaded assets. Returns whether there are still pending assets to be loaded.
    /// </summary>
    /// <param name="engine"></param>
    /// <returns></returns>
    public bool Poll(IEngine engine)
    {
        if (_assetInbox.TryDequeue(out var msg))
        {
            if (msg.Error != null)
            {
                Console.WriteLine(msg.Error.Message);
            }

            // Process asset if it exists
            if (msg.Asset != null)
            {
                switch (msg.Type)
                {
                    // Load to GPU + store in asset store
                    case AssetType.Texture:
                        var texture = engine.TextureFromStream(msg.Asset);
                        _textures[msg.Name] = new TextureHandle { Asset = texture, Status = AssetStatus.Loaded };
                        break;
                    default:
                        throw new Exception($"Unhandled type '{msg.Type}'!");
                }

                // Ensure stream is disposed as we don't want it hanging
                msg.Asset.Dispose();
            }
        }

        return _assetInbox.IsEmpty == false;
    }

    /// <summary>
    /// Attempts to retrieve the texture. Can also be used for preloading.
    /// </summary>
    /// <param name="texture"></param>
    /// <returns></returns>
    public Option<Texture2d> LoadTexture(string texture)
    {
        var textureLoaded = _textures.TryGetValue(texture, out var tex);
        if (textureLoaded && tex.Status == AssetStatus.Loaded && tex.Asset != null)
        {
            return new Option<Texture2d>(tex.Asset);
        }
        else if (!textureLoaded)
        {
            _textures.Add(texture, new TextureHandle { Status = AssetStatus.Queued });
            _backgroundInbox.Enqueue(new BackgroundMsg { Asset = (AssetType.Texture, texture) });
        }

        return Option<Texture2d>.None;
    }

    /// <summary>
    /// Deletes the given texture.
    /// </summary>
    /// <param name="texture"></param>
    public void DeleteTexture(string texture)
    {
        _textures.Remove(texture);
    }
}
