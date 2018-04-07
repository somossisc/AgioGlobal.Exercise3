using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Agio.FileReader.Tests
{
    [TestClass]
    public class FileReaderEncryptedText_Tests
    {
        #region Constants

        /// <summary>
        /// The path of an encrypted empty text file
        /// </summary>
        private const string ENCRYPTED_EMPTY_TEXTFILE_PATH = @"SampleFiles\Empty.txt";

        /// <summary>
        /// The path of a text file with encrypted content
        /// </summary>
        private const string ENCRYPTED_CONTENT_TEXTFILE_PATH = @"SampleFiles\ContentEncrypted.txt";

        /// <summary>
        /// The path of a text file with decrypted content
        /// </summary>
        private const string DECRYPTED_CONTENT_TEXTFILE_PATH = @"SampleFiles\ContentDecrypted.txt";

        #endregion

        #region Public Methods

        [TestMethod]
        public void Read_EmptyTextFileContent_Test()
        {
            //Arrange
            var encrypter = new Encrypter();
            var textReader = new FileReaderText();
            var reader = new FileReaderEncryptedText(encrypter, textReader);

            //Act
            var result = reader.ReadEncrypted(ENCRYPTED_EMPTY_TEXTFILE_PATH);

            //Assert
            Assert.IsTrue(string.Equals(result, string.Empty));
        }

        [TestMethod]
        public async Task ReadAsync_EmptyTextFileContent_Test()
        {
            //Arrange
            var encrypter = new Encrypter();
            var textReader = new FileReaderText();
            var reader = new FileReaderEncryptedText(encrypter, textReader);

            //Act
            var result = await reader.ReadEncryptedAsync(ENCRYPTED_EMPTY_TEXTFILE_PATH);

            //Assert
            Assert.IsTrue(string.Equals(result, string.Empty));
        }

        [TestMethod]
        public void Read_EncryptedTextFileContent_Test()
        {
            //Arrange
            var encrypter = new Encrypter();
            var textReader = new FileReaderText();
            var reader = new FileReaderEncryptedText(encrypter, textReader);
            var decryptedContent = textReader.Read(DECRYPTED_CONTENT_TEXTFILE_PATH);

            //Act
            var result = reader.ReadEncrypted(ENCRYPTED_CONTENT_TEXTFILE_PATH);

            //Assert
            Assert.IsTrue(string.Equals(result, decryptedContent));
        }

        [TestMethod]
        public async Task ReadAsync_EncryptedTextFileContent_Test()
        {
            //Arrange
            var encrypter = new Encrypter();
            var textReader = new FileReaderText();
            var reader = new FileReaderEncryptedText(encrypter, textReader);
            var decryptedContent = await textReader.ReadAsync(DECRYPTED_CONTENT_TEXTFILE_PATH);

            //Act
            var result = await reader.ReadEncryptedAsync(ENCRYPTED_CONTENT_TEXTFILE_PATH);

            //Assert
            Assert.IsTrue(string.Equals(result, decryptedContent));
        }

        #endregion
    }
}
