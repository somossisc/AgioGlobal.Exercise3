using Agio.FileReader.Abstract;

namespace Agio.FileReader
{
    public sealed class FileReaderEncryptedJson : FileReaderEncrypted<IEncrypter, FileReaderJson>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the current class with the specified parameters
        /// </summary>
        /// <param name="encrypter">The encrypter to decrypt the content fo the encrypted files</param>
        /// <param name="fileReaderText">The reader to read the encrypted JSON files</param>
        public FileReaderEncryptedJson(IEncrypter encrypter, FileReaderJson fileReaderJson)
            : base(encrypter, fileReaderJson)
        {
        }

        #endregion
    }
}
