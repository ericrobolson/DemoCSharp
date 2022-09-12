using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Client.DesktopGL;

public class Game1 : Game, Cybo.IEngine, Cybo.IRenderer
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Cybo.App _app;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _app = new Cybo.App();
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    /// <summary>
    /// Need to listen for exit event so app can be shut down.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected override void OnExiting(object sender, EventArgs args)
    {
        _app.ShutDown();
        base.OnExiting(sender, args);
    }


    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _app.Update(this, gameTime.ElapsedGameTime.TotalSeconds);

        if (_app.ShouldExit())
        {
            Exit();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _app.Render(this, gameTime.ElapsedGameTime.TotalSeconds);
        base.Draw(gameTime);
    }

    public void ClearScreen(Cybo.Color c)
    {
        GraphicsDevice.Clear(ToColor(c));
    }


    static Color ToColor(Cybo.Color c)
    {
        return new Color(c.R, c.G, c.B, c.A);
    }

    public void SpriteBatchBegin(Cybo.BlendState blendState)
    {
        var mgBlendState = BlendState.NonPremultiplied;
        switch (blendState)
        {
            case Cybo.BlendState.NonPremultiplied:
                mgBlendState = BlendState.NonPremultiplied;
                break;
            default:
                throw new System.NotImplementedException($"{blendState.ToString()}");
        }

        _spriteBatch.Begin(blendState: mgBlendState);
    }

    public void SpriteBatchEnd()
    {
        _spriteBatch.End();
    }

    public Cybo.Texture2d TextureFromStream(Stream stream)
    {
        return new Cybo.Texture2d(Texture2D.FromStream(GraphicsDevice, stream));
    }

    public Stream LoadFile(string filePath)
    {
        var path = $"Content/{filePath}";
        return new System.IO.FileStream(path, System.IO.FileMode.Open);
    }

    public void DrawTexture(Cybo.Texture2d texture, Cybo.Vec2 position, Cybo.Aabb? subImage, Cybo.Color color)
    {
        var screenPosition = new Vector2((float)position.X, (float)position.Y);

        Rectangle? spriteRectangle = null;
        if (subImage != null)
        {
            spriteRectangle = new Rectangle(subImage.Value.Position.X, subImage.Value.Position.Y, subImage.Value.Size.X, subImage.Value.Size.Y);
        }

        _spriteBatch.Draw((Texture2D)texture.Asset, screenPosition, spriteRectangle, ToColor(color));
    }
}
