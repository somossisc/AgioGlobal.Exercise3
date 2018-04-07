using Agio.FileReader.Abstract;
using System;
using System.Threading.Tasks;

namespace Agio.FileReader
{
    /// <summary>
    /// Specifies a securized file reader
    /// </summary>
    /// <typeparam name="TAuth">The authorizer which will manage the file permissions whose will be read by the specified file reader</typeparam>
    /// <typeparam name="TReader">The file reader which will read the files</typeparam>
    public abstract class FileReaderSecurized<TAuth, TReader> : IFileReaderSecurized where TAuth : IAuthorizer
                                                                                     where TReader : IFileReader
    {
        #region Attributes

        /// <summary>
        /// The authorizer which will manage the file permissions whose will be read by the specified file reader
        /// </summary>
        private TAuth _authorizer;

        /// <summary>
        /// The file reader which will read the files
        /// </summary>
        private TReader _reader;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the current class with the specified parameters
        /// </summary>
        /// <param name="authorizer">The authorizer which will manage the file permissions whose will be read by the specified file reader</param>
        /// <param name="reader">The file reader which will read the files</param>
        public FileReaderSecurized(TAuth authorizer, TReader reader)
        {
            _authorizer = authorizer;
            _reader = reader;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Reads the content of the file of the specified path if the specified role is authorized
        /// </summary>
        /// <param name="role">The role which is trying to read the file of the specified path</param>
        /// <param name="path">The path of the file to be read</param>
        /// <returns>
        /// Returns the content of the file of the path or throws a System.UnauthorizedAccessException if the specified 
        /// role do not access to the file of the specified path
        /// </returns>
        public string Read(string role, string path)
        {
            try
            {
                var task = Task.Run(async () => await ReadAsync(role, path));

                task.Wait();

                return task.Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Reads asynchronously the content of the file of the specified path if the specified role is authorized
        /// </summary>
        /// <param name="role">The role which is trying to read the file of the specified path</param>
        /// <param name="path">The path of the file to be read</param>
        /// <returns>
        /// Returns the content of the file of the path or throws a System.UnauthorizedAccessException if the specified 
        /// role do not access to the file of the specified path
        /// </returns>
        public async Task<string> ReadAsync(string role, string path)
        {
            if (!_authorizer.IsAuthorized(role, path))
                throw new UnauthorizedAccessException();

            var result = await _reader.ReadAsync(path);

            return result;
        }

        #endregion
    }
}
