namespace Agio.FileReader.Abstract
{
    /// <summary>
    /// Describes necessary methods for encrypt or decrypt file contents
    /// </summary>
    public interface IEncrypter
    {
        /// <summary>
        /// When implemented encrypts the specified content
        /// </summary>
        /// <param name="content">The content to encrypt</param>
        /// <returns>The encrypted content</returns>
        string Encrypt(string content);

        /// <summary>
        /// When implemented decrypts the specified content
        /// </summary>
        /// <param name="content">The content to decrypt</param>
        /// <returns>The decrypted content</returns>
        string Decrypt(string content);
    }
}
