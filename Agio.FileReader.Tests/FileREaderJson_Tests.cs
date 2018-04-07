using System;
using System.Json;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agio.FileReader.Tests
{
    [TestClass]
    public class FileReaderJson_Tests
    {
        #region Constants

        /// <summary>
        /// The path of an empty JSON file
        /// </summary>
        private const string EMPTY_FILE_PATH = @"SampleFiles\empty.json";

        /// <summary>
        /// The path of a JSON file with some content
        /// </summary>
        private const string CONTENT_FILE_PATH = @"SampleFiles\content.json";

        #endregion

        #region Public Methods

        [TestMethod]
        public void Read_NullPath_Test()
        {
            //Arrange
            var reader = new FileReaderJson();

            //Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => reader.Read(null));
        }

        [TestMethod]
        public async Task ReadAsync_NullPath_Test()
        {
            //Arrange
            var reader = new FileReaderJson();

            //Act and Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () => await reader.ReadAsync(null));
        }

        [TestMethod]
        public void Read_EmptyFile_Test()
        {
            //Arrange
            var path = EMPTY_FILE_PATH;
            var reader = new FileReaderJson();

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
            var reader = new FileReaderJson();

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
            var reader = new FileReaderJson();

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
            var reader = new FileReaderJson();

            //Act
            var content = await reader.ReadAsync(path);

            //Assert
            Assert.IsFalse(string.IsNullOrEmpty(content));
        }

        [TestMethod]
        public void Read_IsInvalidJson_Test()
        {
            var path = EMPTY_FILE_PATH;
            var reader = new FileReaderJson();

            //Act
            var content = reader.Read(path);

            //Assert
            Assert.ThrowsException<ArgumentException>(() => JsonValue.Parse(content));
        }

        [TestMethod]
        public async Task ReadAsync_IsInvalidJson_Test()
        {
            //Arrange
            var path = EMPTY_FILE_PATH;
            var reader = new FileReaderJson();
            
            //Act
            var content = await reader.ReadAsync(path);

            //Assert
            Assert.ThrowsException<ArgumentException>(() => JsonValue.Parse(content));
        }

        [TestMethod]
        public void Read_IsValidJson_Test()
        {
            var path = CONTENT_FILE_PATH;
            var reader = new FileReaderJson();

            //Act
            var content = reader.Read(path);

            //Assert
            Assert.IsNotNull(JsonValue.Parse(content));
        }

        [TestMethod]
        public async Task ReadAsync_IsValidJson_Test()
        {
            //Arrange
            var path = CONTENT_FILE_PATH;
            var reader = new FileReaderJson();

            //Act
            var content = await reader.ReadAsync(path);

            //Assert
            Assert.IsNotNull(JsonValue.Parse(content));
        }

        #endregion
    }
}
