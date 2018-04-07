using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agio.FileReader.Tests
{
    [TestClass]
    public class FileReaderSecurizedText_Tests
    {
        #region Constants

            /// <summary>
            /// The Admin role
            /// </summary>
        private const string ADMIN_ROLE = "Admin";

        /// <summary>
        /// The User role
        /// </summary>
        private const string USER_ROLE = "User";

        /// <summary>
        /// The path to the empty text file
        /// </summary>
        private const string EMPTY_TEXT_FILEPATH = @"SampleFiles\Empty.txt";

        /// <summary>
        /// The path to the text file with content
        /// </summary>
        private const string CONTENT_TEXT_FILEPATH = @"SampleFiles\Content.txt";

        #endregion

        #region Public Methods

        [TestMethod]
        public void Read_ContentTextFileAdminRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var textReader = new FileReaderText();
            var reader = new FileReaderSecurizedText(authorizer, textReader);

            //Act
            var result = reader.Read(ADMIN_ROLE, CONTENT_TEXT_FILEPATH);

            //Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public async Task ReadAsync_ContentTextFileAdminRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var textReader = new FileReaderText();
            var reader = new FileReaderSecurizedText(authorizer, textReader);

            //Act
            var result = await reader.ReadAsync(ADMIN_ROLE, CONTENT_TEXT_FILEPATH);

            //Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void Read_ContentTextFileUserRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var textReader = new FileReaderText();
            var reader = new FileReaderSecurizedText(authorizer, textReader);

            //Act and Assert
            Assert.ThrowsException<UnauthorizedAccessException>(() => reader.Read(USER_ROLE, CONTENT_TEXT_FILEPATH));
        }

        [TestMethod]
        public async Task ReadAsync_ContentTextFileUserRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var textReader = new FileReaderText();
            var reader = new FileReaderSecurizedText(authorizer, textReader);

            //Act and Assert
            await Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(async () =>
            {
                await reader.ReadAsync(USER_ROLE, CONTENT_TEXT_FILEPATH);
            });
        }

        [TestMethod]
        public void Read_EmptyTextFileAdminRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var textReader = new FileReaderText();
            var reader = new FileReaderSecurizedText(authorizer, textReader);

            //Act
            var result = reader.Read(ADMIN_ROLE, EMPTY_TEXT_FILEPATH);

            //Assert
            Assert.IsTrue(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public async Task ReadAsync_EmptyTextileAdminRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var textReader = new FileReaderText();
            var reader = new FileReaderSecurizedText(authorizer, textReader);

            //Act
            var result = await reader.ReadAsync(ADMIN_ROLE, EMPTY_TEXT_FILEPATH);

            //Assert
            Assert.IsTrue(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void Read_EmptyTextFileUserRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var textReader = new FileReaderText();
            var reader = new FileReaderSecurizedText(authorizer, textReader);

            //Act
            var result = reader.Read(USER_ROLE, EMPTY_TEXT_FILEPATH);

            //Assert
            Assert.IsTrue(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public async Task ReadAsync_EmptyTextFileUserRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var textReader = new FileReaderText();
            var reader = new FileReaderSecurizedText(authorizer, textReader);

            //Act
            var result = await reader.ReadAsync(USER_ROLE, EMPTY_TEXT_FILEPATH);

            //Assert
            Assert.IsTrue(string.IsNullOrWhiteSpace(result));
        }

        #endregion

        #region Private Methods

        private static IDictionary<string, IEnumerable<string>> GetPermissions()
        {
            var result = new Dictionary<string, IEnumerable<string>>
            {
                { EMPTY_TEXT_FILEPATH, new List<string> { USER_ROLE, ADMIN_ROLE } },
                { CONTENT_TEXT_FILEPATH, new List<string> { ADMIN_ROLE } }
            };

            return result;
        }

        #endregion
    }
}
