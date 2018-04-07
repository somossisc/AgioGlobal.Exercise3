using Agio.FileReader.Abstract;

namespace Agio.FileReader
{
    /// <summary>
    /// Implements a XML file reader securized
    /// </summary>
    public sealed class FileReaderSecurizedXml : FileReaderSecurized<IAuthorizer, FileReaderXml>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the current class with the specified parameters
        /// </summary>
        /// <param name="authorizer">The authorizer to manage the file permissions whose should be read by the specified file reader</param>
        /// <param name="fileReaderXml">The file reader to read the XML files</param>
        public FileReaderSecurizedXml(IAuthorizer authorizer, FileReaderXml fileReaderXml)
            : base(authorizer, fileReaderXml)
        {
        }

        #endregion
    }
}
