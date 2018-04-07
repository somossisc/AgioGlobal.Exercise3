using System.Threading.Tasks;

namespace Agio.FileReader.Abstract
{
    public interface IFileReaderEncrypted
    {
        /// <summary>
        /// When implemented reads and decrypts the content of the file of the specified path
        /// </summary>
        /// <param name="path">The path of the file to be read and decrypted</param>
        /// <returns>
        /// Returns a System.String that represents the decrypted content of the file of the specified path
        /// </returns>
        string ReadEncrypted(string path);

        /// <summary>
        /// When implemented reads asynchronously and decrypts the content of the file of the specified path
        /// </summary>
        /// <param name="path">The path of the file to be read and decrypted</param>
        /// <returns>
        /// Returns a System.String that represents the decrypted content of the file of the specified path
        /// </returns>
        Task<string> ReadEncryptedAsync(string path);
    }
}
