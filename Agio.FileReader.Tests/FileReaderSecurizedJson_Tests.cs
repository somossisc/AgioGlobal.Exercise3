using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agio.FileReader.Tests
{
    [TestClass]
    public class FileReaderSecurizedJson_Tests
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
        /// The path to the empty json file
        /// </summary>
        private const string EMPTY_JSON_FILEPATH = @"SampleFiles\empty.json";

        /// <summary>
        /// The path to the json file with content
        /// </summary>
        private const string CONTENT_JSON_FILEPATH = @"SampleFiles\content.json";

        #endregion

        #region Public Methods

        [TestMethod]
        public void Read_ContentJsonFileAdminRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var jsonReader = new FileReaderJson();
            var reader = new FileReaderSecurizedJson(authorizer, jsonReader);

            //Act
            var result = reader.Read(ADMIN_ROLE, CONTENT_JSON_FILEPATH);

            //Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public async Task ReadAsync_ContentJsonFileAdminRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var jsonReader = new FileReaderJson();
            var reader = new FileReaderSecurizedJson(authorizer, jsonReader);

            //Act
            var result = await reader.ReadAsync(ADMIN_ROLE, CONTENT_JSON_FILEPATH);

            //Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void Read_ContentJsonFileUserRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var jsonReader = new FileReaderJson();
            var reader = new FileReaderSecurizedJson(authorizer, jsonReader);

            //Act and Assert
            Assert.ThrowsException<UnauthorizedAccessException>(() => reader.Read(USER_ROLE, CONTENT_JSON_FILEPATH));
        }

        [TestMethod]
        public async Task ReadAsync_ContentXmlFileUserRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var jsonReader = new FileReaderJson();
            var reader = new FileReaderSecurizedJson(authorizer, jsonReader);

            //Act and Assert
            await Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(async () =>
            {
                await reader.ReadAsync(USER_ROLE, CONTENT_JSON_FILEPATH);
            });
        }

        [TestMethod]
        public void Read_EmptyJsonFileAdminRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var jsonReader = new FileReaderJson();
            var reader = new FileReaderSecurizedJson(authorizer, jsonReader);

            //Act
            var result = reader.Read(ADMIN_ROLE, EMPTY_JSON_FILEPATH);

            //Assert
            Assert.IsTrue(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public async Task ReadAsync_EmptyJsonFileAdminRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var jsonReader = new FileReaderJson();
            var reader = new FileReaderSecurizedJson(authorizer, jsonReader);

            //Act
            var result = await reader.ReadAsync(ADMIN_ROLE, EMPTY_JSON_FILEPATH);

            //Assert
            Assert.IsTrue(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void Read_EmptyJsonFileUserRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var jsonReader = new FileReaderJson();
            var reader = new FileReaderSecurizedJson(authorizer, jsonReader);

            //Act
            var result = reader.Read(USER_ROLE, EMPTY_JSON_FILEPATH);

            //Assert
            Assert.IsTrue(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public async Task ReadAsync_EmptyJsonFileUserRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var jsonReader = new FileReaderJson();
            var reader = new FileReaderSecurizedJson(authorizer, jsonReader);

            //Act
            var result = await reader.ReadAsync(USER_ROLE, EMPTY_JSON_FILEPATH);

            //Assert
            Assert.IsTrue(string.IsNullOrWhiteSpace(result));
        }

        #endregion

        #region Private Methods

        private static IDictionary<string, IEnumerable<string>> GetPermissions()
        {
            var result = new Dictionary<string, IEnumerable<string>>
            {
                { EMPTY_JSON_FILEPATH, new List<string> { USER_ROLE, ADMIN_ROLE } },
                { CONTENT_JSON_FILEPATH, new List<string> { ADMIN_ROLE } }
            };

            return result;
        }

        #endregion
    }
}
