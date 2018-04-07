using System.Threading.Tasks;

namespace Agio.FileReader.Abstract
{
    /// <summary>
    /// Describes FileReader methods
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// When implemented read the file content from the specified path
        /// </summary>
        /// <param name="path">Path to the file to be read</param>
        /// <returns>A System.String instance that represents the file content</returns>
        string Read(string path);

        /// <summary>
        /// When implemented read the file content from the specified path asynchronously
        /// </summary>
        /// <param name="path">Path to the file to be read</param>
        /// <returns>A System.String instance that represents the file content</returns>
        Task<string> ReadAsync(string path);
    }
}
