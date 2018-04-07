using Agio.FileReader.Abstract;

namespace Agio.FileReader
{
    public sealed class FileReaderEncryptedText : FileReaderEncrypted<IEncrypter, FileReaderText>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the current class with the specified parameters
        /// </summary>
        /// <param name="encrypter">The encrypter to decrypt the content fo the encrypted files</param>
        /// <param name="fileReaderText">The reader to read the encrypted text files</param>
        public FileReaderEncryptedText(IEncrypter encrypter, FileReaderText fileReaderText)
            : base(encrypter, fileReaderText)
        {
        }

        #endregion
    }
}
