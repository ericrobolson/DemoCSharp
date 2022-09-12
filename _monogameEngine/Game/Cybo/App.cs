using Cybo.Ecs;

namespace Cybo
{
    /// <summary>
    /// The core application that powers Cybo.
    /// </summary>
    public class App
    {
        static double TICK_RATE = 1.0f / 60.0f;
        double _accumulatedTime = 0;
        readonly uint _maxTicksPerUpdate;
        World _world;
        bool _shouldExit;
        AssetManager _assetManager = new AssetManager();

        public App(uint maxTicksPerUpdate = 10)
        {
            _maxTicksPerUpdate = maxTicksPerUpdate;
            _world = new World(1000);
        }

        /// <summary>
        /// Returns whether the app should exit or not.
        /// /// </summary>
        /// <returns></returns>
        public bool ShouldExit()
        {
            return _shouldExit;
        }

        /// <summary>
        /// Shuts down the application.
        /// </summary>
        public void ShutDown()
        {
            _assetManager.ShutDown();
        }

        /// <summary>
        /// Updates the app.
        /// Takes in the elapsed time since the last tick.
        /// </summary>
        /// <param name="deltaT">The elapsed time since the last tick.</param>
        public void Update(IEngine engine, double deltaT)
        {
            UpdateSimulation(engine, deltaT);
            LoadAssets(engine);
        }

        /// <summary>
        /// Performs a render of the game state.
        /// Takes in a reference to the renderer as well as the time since the last render.
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="deltaT"></param>
        public void Render(IRenderer renderer, double deltaT)
        {
            renderer.ClearScreen(Color.White());

            // Since we're using Monogame and not the content pipeline,
            // need to begin a spritebatch and ensure that we're not using
            // a premultiplied alpha state.
            renderer.SpriteBatchBegin(BlendState.NonPremultiplied);

            // Render sprites
            var spriteList = _world.SpriteDrawList;
            for (int i = 0; i < spriteList.Count; i++)
            {
                var sprite = spriteList[i];
                _assetManager.LoadTexture(sprite.ImageFile).IsSome(texture =>
                {
                    renderer.DrawTexture(texture, sprite.Position, sprite.SubImage, sprite.Color);
                });
            }

            renderer.SpriteBatchEnd();
        }

        /// <summary>
        /// Loads all required assets.
        /// </summary>
        /// <param name="engine"></param>
        void LoadAssets(IEngine engine)
        {
            // Ensure assets are loaded.
            var activeAssets = _world.ActiveAssets;
            for (int i = 0; i < activeAssets.Count; i++)
            {
                var asset = activeAssets[i];
                switch (asset.Type)
                {
                    case AssetType.Texture:
                        _assetManager.LoadTexture(asset.FilePath);
                        break;
                    default:
                        throw new System.Exception($"unhandled {asset.Type}");
                }
            }

            // Register assets in engine
            const int MAX_ASSETS_TO_LOAD_PER_UPDATE = 10;
            for (int i = 0; i < MAX_ASSETS_TO_LOAD_PER_UPDATE; i++)
            {
                if (_assetManager.Poll(engine) == false)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Updates the simulation. If lagging, will tick it a few times to attempt to catch up.
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="deltaT"></param>
        void UpdateSimulation(IEngine engine, double deltaT)
        {
            // https://gafferongames.com/post/fix_your_timestep/
            _accumulatedTime += deltaT;
            for (var i = 0; i < _maxTicksPerUpdate; i++)
            {
                if (_accumulatedTime >= TICK_RATE)
                {
                    _accumulatedTime -= TICK_RATE;
                    _world.Tick();
                }
                else
                {
                    break;
                }
            }
        }
    }
}
