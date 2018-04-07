using Agio.FileReader.Abstract;

namespace Agio.FileReader
{
    /// <summary>
    /// Implements a text file reader securized
    /// </summary>
    public sealed class FileReaderSecurizedText : FileReaderSecurized<IAuthorizer, FileReaderText>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the current class with the specified parameters
        /// </summary>
        /// <param name="authorizer">The authorizer to manage the file permissions whose should be read by the specified file reader</param>
        /// <param name="fileReaderText">The file reader to read the text files</param>
        public FileReaderSecurizedText(IAuthorizer authorizer, FileReaderText fileReaderText)
            : base(authorizer, fileReaderText)
        {
        }

        #endregion
    }
}
