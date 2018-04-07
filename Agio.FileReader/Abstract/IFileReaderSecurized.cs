using System.Threading.Tasks;

namespace Agio.FileReader.Abstract
{
    public interface IFileReaderSecurized
    {
        /// <summary>
        /// When implemented reads the content of the file of the specified path if the specified role is authorized
        /// </summary>
        /// <param name="role">The role which is trying to read the file of the specified path</param>
        /// <param name="path">The path of the file to be read</param>
        /// <returns>
        /// Returns the content of the file of the path or throws a System.UnauthorizedAccessException if the specified 
        /// role do not access to the file of the specified path
        /// </returns>
        string Read(string role, string path);

        /// <summary>
        /// When implemented reads asynchronously the content of the file of the specified path if the specified role is authorized
        /// </summary>
        /// <param name="role">The role which is trying to read the file of the specified path</param>
        /// <param name="path">The path of the file to be read</param>
        /// <returns>
        /// Returns the content of the file of the path or throws a System.UnauthorizedAccessException if the specified 
        /// role do not access to the file of the specified path
        /// </returns>
        Task<string> ReadAsync(string role, string path);
    }
}
