using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agio.FileReader.Tests
{
    [TestClass]
    public class Encrypter_Tests
    {
        #region Constants

        /// <summary>
        /// The decrypted content
        /// </summary>
        private const string DECRYPTED_CONTENT = "0123456789";

        /// <summary>
        /// The encrypted content
        /// </summary>
        private const string ENCRYPTED_CONTENT = "9876543210";

        #endregion

        #region Public Methods

        [TestMethod]
        public void Decrypt_NullContent_Test()
        {
            //Arrange
            var encrypter = new Encrypter();

            //Act
            var result = encrypter.Decrypt(null);

            //Assert
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void Decrypt_Content_Test()
        {
            //Arrange
            var encrypter = new Encrypter();

            //Act
            var result = encrypter.Decrypt(ENCRYPTED_CONTENT);

            //Assert
            Assert.IsTrue(string.Equals(result, DECRYPTED_CONTENT));
        }

        [TestMethod]
        public void Encrypt_NullContent_Test()
        {
            //Arrange
            var encrypter = new Encrypter();

            //Act
            var result = encrypter.Encrypt(null);

            //Assert
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void Encrypt_Content_Test()
        {
            //Arrange
            var encrypter = new Encrypter();

            //Act
            var result = encrypter.Encrypt(DECRYPTED_CONTENT);

            //Assert
            Assert.IsTrue(string.Equals(result, ENCRYPTED_CONTENT));
        }

        #endregion
    }
}
