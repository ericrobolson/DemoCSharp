namespace Cybo
{
    public enum BlendState
    {
        NonPremultiplied
    }

    /// <summary>
    /// A simple representation for a texture.
    /// </summary>
    public class Texture2d
    {
        public Texture2d(object obj)
        {
            Asset = obj;
        }

        public object Asset { get; }
    }

    /// <summary>
    /// An interface a renderer must implement.
    /// </summary>
    public interface IRenderer
    {
        /// <summary>
        /// Creates a new texture from the given stream.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        Texture2d TextureFromStream(System.IO.Stream stream);

        /// <summary>
        /// Draws the given texture to the screen.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="subImage"></param>
        /// <param name="color"></param>
        void DrawTexture(Texture2d texture, Vec2 position, Aabb? subImage, Color color);

        /// <summary>
        /// Clears the screen and sets it to the given color.
        /// </summary>
        /// <param name="color"></param>
        void ClearScreen(Color color);

        /// <summary>
        /// Begins a new sprite batch with the given blend state.
        /// </summary>
        /// <param name="blendState"></param>
        void SpriteBatchBegin(BlendState blendState);

        /// <summary>
        /// Ends the spritebatch.
        /// </summary>
        void SpriteBatchEnd();
    }
}