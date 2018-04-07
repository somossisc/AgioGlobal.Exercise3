namespace Agio.FileReader.Abstract
{
    /// <summary>
    /// Describes a file authorizer
    /// </summary>
    public interface IAuthorizer
    {
        /// <summary>
        /// When implemented indicates if the specified role could either access the file of the specified path or not
        /// </summary>
        /// <param name="path">The path of the file to be accessed securely</param>
        /// <returns>
        /// Returns true if the role is authorized to access the file of the specified path or false in other case
        /// </returns>
        bool IsAuthorized(string role, string path);
    }
}
