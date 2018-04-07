using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Agio.FileReader.Tests
{
    [TestClass]
    public class FileReaderEncryptedXml_Tests
    {
        #region Constants

        /// <summary>
        /// The path of an encrypted empty text file
        /// </summary>
        private const string ENCRYPTED_EMPTY_XMLFILE_PATH = @"SampleFiles\Empty.xml";

        /// <summary>
        /// The path of a text file with encrypted content
        /// </summary>
        private const string ENCRYPTED_CONTENT_TEXTFILE_PATH = @"SampleFiles\ContentEncrypted.xml";

        /// <summary>
        /// The path of a text file with decrypted content
        /// </summary>
        private const string DECRYPTED_CONTENT_XMLFILE_PATH = @"SampleFiles\ContentDecrypted.xml";

        #endregion

        #region Public Methods

        [TestMethod]
        public void ReadEncrypted_EmptyXmlFile_Test()
        {
            //Arrange
            var encrypter = new Encrypter();
            var xmlReader = new FileReaderXml();
            var reader = new FileReaderEncryptedXml(encrypter, xmlReader);

            //Act
            var result = reader.ReadEncrypted(ENCRYPTED_EMPTY_XMLFILE_PATH);

            //Assert
            Assert.IsTrue(string.Equals(result, string.Empty));
        }

        [TestMethod]
        public async Task ReadEncryptedAsync_EmptyXmlFile_Test()
        {
            //Arrange
            var encrypter = new Encrypter();
            var xmlReader = new FileReaderXml();
            var reader = new FileReaderEncryptedXml(encrypter, xmlReader);

            //Act
            var result = await reader.ReadEncryptedAsync(ENCRYPTED_EMPTY_XMLFILE_PATH);

            //Assert
            Assert.IsTrue(string.Equals(result, string.Empty));
        }

        [TestMethod]
        public void ReadEncrypted_EncryptedContentXmlFile_Test()
        {
            //Arrange
            var encrypter = new Encrypter();
            var xmlReader = new FileReaderXml();
            var reader = new FileReaderEncryptedXml(encrypter, xmlReader);
            var decryptedContent = xmlReader.Read(DECRYPTED_CONTENT_XMLFILE_PATH);

            //Act
            var result = reader.ReadEncrypted(ENCRYPTED_CONTENT_TEXTFILE_PATH);

            //Assert
            Assert.IsTrue(string.Equals(result, decryptedContent));
        }

        [TestMethod]
        public async Task ReadEncryptedAsync_EncryptedContentXmlFile_Test()
        {
            //Arrange
            var encrypter = new Encrypter();
            var xmlReader = new FileReaderXml();
            var reader = new FileReaderEncryptedXml(encrypter, xmlReader);
            var decryptedContent = await xmlReader.ReadAsync(DECRYPTED_CONTENT_XMLFILE_PATH);

            //Act
            var result = await reader.ReadEncryptedAsync(ENCRYPTED_CONTENT_TEXTFILE_PATH);

            //Assert
            Assert.IsTrue(string.Equals(result, decryptedContent));
        }

        #endregion
    }
}
