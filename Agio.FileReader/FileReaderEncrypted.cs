using Agio.FileReader.Abstract;
using System;
using System.Threading.Tasks;

namespace Agio.FileReader
{
    /// <summary>
    /// Specifies a reader for encrypted files
    /// </summary>
    /// <typeparam name="TEncrypter">The type of the encrypter responsible for the decryption of the content of the encrypted files to read</typeparam>
    /// <typeparam name="TReader">The type of the reader to read the encrypted files</typeparam>
    public abstract class FileReaderEncrypted<TEncrypter, TReader> : IFileReaderEncrypted where TEncrypter : IEncrypter
                                                                                         where TReader : IFileReader
    {
        #region Attributes

        /// <summary>
        /// The encrypter to decrypt the content fo the encrypted files
        /// </summary>
        private TEncrypter _encrypter;

        /// <summary>
        /// The reader to read the encrypted files
        /// </summary>
        private TReader _reader;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the current class with the specified paramenters
        /// </summary>
        /// <param name="encrypter">The encrypter to decrypt the content fo the encrypted files</param>
        /// <param name="reader">The reader to read the encrypted files</param>
        public FileReaderEncrypted(TEncrypter encrypter, TReader reader)
        {
            _encrypter = encrypter;
            _reader = reader;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Reads and decrypts the content of the file of the specified path
        /// </summary>
        /// <param name="path">The path of the file to be read and decrypted</param>
        /// <returns>
        /// Returns a System.String that represents the decrypted content of the file of the specified path
        /// </returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="System.IO.PathTooLongException"></exception>
        /// <exception cref="System.IO.DirectoryNotFoundException"></exception>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public string ReadEncrypted(string path)
        {
            try
            {
                var task = Task.Run(async () => await ReadEncryptedAsync(path));

                task.Wait();

                return task.Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Reads asynchronously and decrypts the content of the file of the specified path
        /// </summary>
        /// <param name="path">The path of the file to be read and decrypted</param>
        /// <returns>
        /// Returns a System.String that represents the decrypted content of the file of the specified path
        /// </returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="System.IO.PathTooLongException"></exception>
        /// <exception cref="System.IO.DirectoryNotFoundException"></exception>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public async Task<string> ReadEncryptedAsync(string path)
        {
            var content = await _reader.ReadAsync(path);
            var result = _encrypter.Decrypt(content);

            return result;
        }

        #endregion
    }
}
