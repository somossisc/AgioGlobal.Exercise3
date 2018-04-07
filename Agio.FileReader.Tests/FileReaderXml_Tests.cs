using System;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agio.FileReader.Tests
{
    [TestClass]
    public class FileReaderXml_Tests
    {
        #region Constants

        /// <summary>
        /// The path of an empty text file
        /// </summary>
        private const string EMPTY_FILE_PATH = @"SampleFiles\Empty.xml";

        /// <summary>
        /// The path of a text file with some content
        /// </summary>
        private const string CONTENT_FILE_PATH = @"SampleFiles\Content.xml";

        #endregion

        #region Public Methods

        [TestMethod]
        public void Read_NullPath_Test()
        {
            //Arrange
            var reader = new FileReaderXml();

            //Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => reader.Read(null));
        }

        [TestMethod]
        public async Task ReadAsync_NullPath_Test()
        {
            //Arrange
            var reader = new FileReaderXml();

            //Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await reader.ReadAsync(null));
        }

        [TestMethod]
        public void Read_EmptyFile_Test()
        {
            //Arrange
            var path = EMPTY_FILE_PATH;
            var reader = new FileReaderXml();

            //Act
            var content = reader.Read(path);

            //Assert
            Assert.IsTrue(string.IsNullOrEmpty(content));
        }

        [TestMethod]
        public async Task ReadAsync_EmptyFile_Test()
        {
            //Arrange
            var path = EMPTY_FILE_PATH;
            var reader = new FileReaderXml();

            //Act
            var content = await reader.ReadAsync(path);

            //Assert
            Assert.IsTrue(string.IsNullOrEmpty(content));
        }

        [TestMethod]
        public void Read_ContentFile_Test()
        {
            //Arrange
            var path = CONTENT_FILE_PATH;
            var reader = new FileReaderXml();

            //Act
            var content = reader.Read(path);

            //Assert
            Assert.IsFalse(string.IsNullOrEmpty(content));
        }

        [TestMethod]
        public async Task ReadAsync_ContentFile_Test()
        {
            //Arrange
            var path = CONTENT_FILE_PATH;
            var reader = new FileReaderXml();

            //Act
            var content = await reader.ReadAsync(path);

            //Assert
            Assert.IsFalse(string.IsNullOrEmpty(content));
        }

        [TestMethod]
        public void Read_IsInvalidXml_Test()
        {
            var path = EMPTY_FILE_PATH;
            var reader = new FileReaderXml();
            var xmlDoc = new XmlDocument();

            //Act
            var content = reader.Read(path);

            //Assert
            Assert.ThrowsException<XmlException>(() => xmlDoc.LoadXml(content));
        }

        [TestMethod]
        public async Task ReadAsync_IsInvalidXml_Test()
        {
            //Arrange
            var path = EMPTY_FILE_PATH;
            var reader = new FileReaderXml();
            var xmlDoc = new XmlDocument();

            //Act
            var content = await reader.ReadAsync(path);

            //Assert
            Assert.ThrowsException<XmlException>(() => xmlDoc.LoadXml(content));
        }

        [TestMethod]
        public void Read_IsValidXml_Test()
        {
            var path = CONTENT_FILE_PATH;
            var reader = new FileReaderXml();
            var xmlDoc = new XmlDocument();

            //Act
            var content = reader.Read(path);
            xmlDoc.LoadXml(content);

            //Assert
            Assert.IsFalse(string.IsNullOrEmpty(xmlDoc.OuterXml));
        }

        [TestMethod]
        public async Task ReadAsync_IsValidXml_Test()
        {
            //Arrange
            var path = CONTENT_FILE_PATH;
            var reader = new FileReaderXml();
            var xmlDoc = new XmlDocument();

            //Act
            var content = await reader.ReadAsync(path);
            xmlDoc.LoadXml(content);

            //Assert
            Assert.IsFalse(string.IsNullOrEmpty(xmlDoc.OuterXml));
        }

        #endregion
    }
}
