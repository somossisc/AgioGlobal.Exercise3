using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agio.FileReader.Tests
{
    [TestClass]
    public class FileReaderSecurizedXml_Tests
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
        /// The path to the empty xml file
        /// </summary>
        private const string EMPTY_XML_FILEPATH = @"SampleFiles\Empty.xml";

        /// <summary>
        /// The path to the xml file with content
        /// </summary>
        private const string CONTENT_XML_FILEPATH = @"SampleFiles\Content.xml";

        #endregion

        #region Public Methods

        [TestMethod]
        public void Read_ContentXmlFileAdminRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var xmlReader = new FileReaderXml();
            var reader = new FileReaderSecurizedXml(authorizer, xmlReader);

            //Act
            var result = reader.Read(ADMIN_ROLE, CONTENT_XML_FILEPATH);

            //Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public async Task ReadAsync_ContentXmlFileAdminRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var xmlReader = new FileReaderXml();
            var reader = new FileReaderSecurizedXml(authorizer, xmlReader);

            //Act
            var result = await reader.ReadAsync(ADMIN_ROLE, CONTENT_XML_FILEPATH);

            //Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void Read_ContentXmlFileUserRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var xmlReader = new FileReaderXml();
            var reader = new FileReaderSecurizedXml(authorizer, xmlReader);

            //Act and Assert
            Assert.ThrowsException<UnauthorizedAccessException>(() => reader.Read(USER_ROLE, CONTENT_XML_FILEPATH));
        }

        [TestMethod]
        public async Task ReadAsync_ContentXmlFileUserRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var xmlReader = new FileReaderXml();
            var reader = new FileReaderSecurizedXml(authorizer, xmlReader);

            //Act and Assert
            await Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(async () =>
            {
                await reader.ReadAsync(USER_ROLE, CONTENT_XML_FILEPATH);
            });
        }

        [TestMethod]
        public void Read_EmptyXmlFileAdminRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var xmlReader = new FileReaderXml();
            var reader = new FileReaderSecurizedXml(authorizer, xmlReader);

            //Act
            var result = reader.Read(ADMIN_ROLE, EMPTY_XML_FILEPATH);

            //Assert
            Assert.IsTrue(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public async Task ReadAsync_EmptyXmlFileAdminRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var xmlReader = new FileReaderXml();
            var reader = new FileReaderSecurizedXml(authorizer, xmlReader);

            //Act
            var result = await reader.ReadAsync(ADMIN_ROLE, EMPTY_XML_FILEPATH);

            //Assert
            Assert.IsTrue(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public void Read_EmptyXmlFileUserRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var xmlReader = new FileReaderXml();
            var reader = new FileReaderSecurizedXml(authorizer, xmlReader);

            //Act
            var result = reader.Read(USER_ROLE, EMPTY_XML_FILEPATH);

            //Assert
            Assert.IsTrue(string.IsNullOrWhiteSpace(result));
        }

        [TestMethod]
        public async Task ReadAsync_EmptyXmlFileUserRoleAccess_Test()
        {
            //Arrange
            var permissions = GetPermissions();
            var authorizer = new Authorizer(permissions);
            var xmlReader = new FileReaderXml();
            var reader = new FileReaderSecurizedXml(authorizer, xmlReader);

            //Act
            var result = await reader.ReadAsync(USER_ROLE, EMPTY_XML_FILEPATH);

            //Assert
            Assert.IsTrue(string.IsNullOrWhiteSpace(result));
        }

        #endregion

        #region Private Methods

        private static IDictionary<string, IEnumerable<string>> GetPermissions()
        {
            var result = new Dictionary<string, IEnumerable<string>>
            {
                { EMPTY_XML_FILEPATH, new List<string> { USER_ROLE, ADMIN_ROLE } },
                { CONTENT_XML_FILEPATH, new List<string> { ADMIN_ROLE } }
            };

            return result;
        }

        #endregion
    }
}
