using Agio.FileReader.Abstract;
using System.Linq;

namespace Agio.FileReader
{
    /// <summary>
    /// Implements a class which methods encrypts or decrypts file contents
    /// </summary>
    public class Encrypter : IEncrypter
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the current class
        /// </summary>
        public Encrypter()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Decrypts the specified content
        /// </summary>
        /// <param name="content">The content to be decrypted</param>
        /// <returns>
        /// Returns a System.String that represents the specified content decrypted
        /// </returns>
        /// <remarks>
        /// The decryption algorithm reverts the specified content
        /// </remarks>
        public string Decrypt(string content)
        {
            string result;

            if (string.IsNullOrWhiteSpace(content))
                result = content;
            else
                result = new string(content.Reverse().ToArray());

            return result;
        }

        /// <summary>
        /// Encrypts the specified content
        /// </summary>
        /// <param name="content">The content to be encrypted</param>
        /// <returns>
        /// Returns a System.String that represents the specified content encrypted
        /// </returns>
        /// <remarks>
        /// The encryption algorithm reverts the specified content
        /// </remarks>
        public string Encrypt(string content)
        {
            string result;

            if (string.IsNullOrWhiteSpace(content))
                result = content;
            else
                result = new string(content.Reverse().ToArray());

            return result;
        }

        #endregion
    }
}
