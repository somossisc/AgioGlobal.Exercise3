using Agio.FileReader.Abstract;

namespace Agio.FileReader
{
    public sealed class FileReaderEncryptedXml : FileReaderEncrypted<IEncrypter, FileReaderXml>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the current class with the specified parameters
        /// </summary>
        /// <param name="encrypter">The encrypter to decrypt the content fo the encrypted files</param>
        /// <param name="fileReaderXml">The reader to read the encrypted xml files</param>
        public FileReaderEncryptedXml(IEncrypter encrypter, FileReaderXml fileReaderXml)
            : base(encrypter, fileReaderXml)
        {
        }

        #endregion
    }
}
