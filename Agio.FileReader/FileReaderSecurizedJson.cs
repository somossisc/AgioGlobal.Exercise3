using Agio.FileReader.Abstract;

namespace Agio.FileReader
{
    /// <summary>
    /// Implements a JSON file reader securized
    /// </summary>
    public sealed class FileReaderSecurizedJson : FileReaderSecurized<IAuthorizer, FileReaderJson>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the current class with the specified parameters
        /// </summary>
        /// <param name="authorizer">The authorizer to manage the file permissions whose should be read by the specified file reader</param>
        /// <param name="fileReaderJson">The file reader to read the JSON files</param>
        public FileReaderSecurizedJson(IAuthorizer authorizer, FileReaderJson fileReaderJson)
            : base(authorizer, fileReaderJson)
        {
        }

        #endregion
    }
}
