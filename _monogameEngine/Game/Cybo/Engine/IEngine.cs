namespace Cybo
{
    /// <summary>
    /// An interface an engine must provide.
    /// </summary>
    /// <typeparam name="TTexture"></typeparam>
    public interface IEngine : IRenderer
    {
        /// <summary>
        /// Loads a file from the given path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        System.IO.Stream LoadFile(string filePath);
    }
}