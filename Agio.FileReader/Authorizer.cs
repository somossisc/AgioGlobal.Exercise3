using Agio.FileReader.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Agio.FileReader
{
    /// <summary>
    /// Implements file authorizer
    /// </summary>
    public sealed class Authorizer : IAuthorizer
    {
        #region Attributes

        /// <summary>
        /// The dictionary with all paths and required roles for each file
        /// </summary>
        private IDictionary<string, IEnumerable<string>> _filesPermissions;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the current class with the specified parameters
        /// </summary>
        /// <param name="filePermissions">A dictionary with all paths as keys and their required roles as values</param>
        public Authorizer(IDictionary<string, IEnumerable<string>> filePermissions)
        {
            _filesPermissions = filePermissions;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Indicates if the specified role could either access the specified path or not
        /// </summary>
        /// <param name="path">The path to be accessed securely</param>
        /// <returns>
        /// Returns true if the role is authorized to access the specified path or false in other case
        /// </returns>
        /// <exception cref="System.ArgumentException"></exception>
        public bool IsAuthorized(string role, string path)
        {
            var result = false;

            if (_filesPermissions.ContainsKey(path))
                result = _filesPermissions[path].Contains(role);

            return result;
        }

        #endregion
    }
}
