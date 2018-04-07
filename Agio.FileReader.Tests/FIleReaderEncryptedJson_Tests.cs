using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Agio.FileReader.Tests
{
    [TestClass]
    public class FIleReaderEncryptedJson_Tests
    {
        #region Constants

        /// <summary>
        /// The path of an encrypted empty text file
        /// </summary>
        private const string ENCRYPTED_EMPTY_JSONFILE_PATH = @"SampleFiles\empty.json";

        /// <summary>
        /// The path of a text file with encrypted content
        /// </summary>
        private const string ENCRYPTED_CONTENT_JSONFILE_PATH = @"SampleFiles\contentEncrypted.json";

        /// <summary> 
        /// The path of a text file with decrypted content
        /// </summary>
        private const string DECRYPTED_CONTENT_JSONFILE_PATH = @"SampleFiles\contentDecrypted.json";

        #endregion

        #region Public Methods

        [TestMethod]
        public void Read_EmptyJsonFile_Test()
        {
            //Arrange
            var encrypter = new Encrypter();
            var jsonReader = new FileReaderJson();
            var reader = new FileReaderEncryptedJson(encrypter, jsonReader);

            //Act
            var result = reader.ReadEncrypted(ENCRYPTED_EMPTY_JSONFILE_PATH);

            //Assert
            Assert.IsTrue(string.Equals(result, string.Empty));
        }

        [TestMethod]
        public async Task ReadAsync_EmptyJsonFile_Test()
        {
            //Arrange
            var encrypter = new Encrypter();
            var jsonReader = new FileReaderJson();
            var reader = new FileReaderEncryptedJson(encrypter, jsonReader);

            //Act
            var result = await reader.ReadEncryptedAsync(ENCRYPTED_EMPTY_JSONFILE_PATH);

            //Assert
            Assert.IsTrue(string.Equals(result, string.Empty));
        }

        [TestMethod]
        public void Read_EncryptedJsonFile_Test()
        {
            //Arrange
            var encrypter = new Encrypter();
            var jsonReader = new FileReaderJson();
            var reader = new FileReaderEncryptedJson(encrypter, jsonReader);
            var decryptedContent = jsonReader.Read(DECRYPTED_CONTENT_JSONFILE_PATH);

            //Act
            var result = reader.ReadEncrypted(ENCRYPTED_CONTENT_JSONFILE_PATH);

            //Assert
            Assert.IsTrue(string.Equals(result, decryptedContent));
        }

        [TestMethod]
        public async Task ReadAsync_EncryptedJsonFile_Test()
        {
            //Arrange
            var encrypter = new Encrypter();
            var jsonReader = new FileReaderJson();
            var reader = new FileReaderEncryptedJson(encrypter, jsonReader);
            var decryptedContent = await jsonReader.ReadAsync(DECRYPTED_CONTENT_JSONFILE_PATH);

            //Act
            var result = await reader.ReadEncryptedAsync(ENCRYPTED_CONTENT_JSONFILE_PATH);

            //Assert
            Assert.IsTrue(string.Equals(result, decryptedContent));
        }

        #endregion
    }
}
