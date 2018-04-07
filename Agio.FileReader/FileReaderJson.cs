using Agio.FileReader.Abstract;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Agio.FileReader
{
    /// <summary>
    /// Implements a reader for JSON files
    /// </summary>
    public sealed class FileReaderJson : IFileReader
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the current class
        /// </summary>
        public FileReaderJson()
        {

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Reads the content of a JSON file from the specified path
        /// </summary>
        /// <param name="path">Path to a JSON file</param>
        /// <returns>The content of the JSON file as string</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public string Read(string path)
        {
            try
            {
                var task = Task.Run(async () => await ReadAsync(path));

                task.Wait();

                return task.Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }

        /// <summary>
        /// Reads the content of a JSON file from the specified path
        /// </summary>
        /// <remarks>This method runs asynchronously</remarks>
        /// <param name="path">Path to a JSON file</param>
        /// <returns>The content of the JSON file as string</returns>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public async Task<string> ReadAsync(string path)
        {
            var result = string.Empty;

            //Note that a JSON file still being a text file
            using (var reader = File.OpenText(path))
            {
                //The JSON file content could be encrypted or not
                result = await reader.ReadToEndAsync();
            }

            return result;
        }

        #endregion
    }
}
