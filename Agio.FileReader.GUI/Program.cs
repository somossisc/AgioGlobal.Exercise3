using Agio.FileReader.Abstract;
using Agio.FileReader.GUI.Enums;
using Agio.FileReader.GUI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agio.FileReader.GUI
{
    public static class Program
    {
        #region Constants

        private const string EXIT_CMD = "exit";
        private const string PROMPT_TXT = "> ";

        private const char YES_CMD = 'y';
        private const char NO_CMD = 'n';

        private const string ENCRYPTED_JSON = @"SampleFiles\encrypted.json";
        private const string ENCRYPTED_TEXT = @"SampleFiles\Encrypted.txt";
        private const string ENCRYPTED_XML = @"SampleFiles\Encrypted.xml";

        private const string PRIVATE_JSON = @"SampleFiles\private.json";
        private const string PRIVATE_TEXT = @"SampleFiles\Private.txt";
        private const string PRIVATE_XML = @"SampleFiles\Private.xml";

        private const string PUBLIC_JSON = @"SampleFiles\public.json";
        private const string PUBLIC_TEXT = @"SampleFiles\Public.txt";
        private const string PUBLIC_XML = @"SampleFiles\Public.xml";

        #endregion

        #region Public Methods

        public static void Main(string[] args)
        {
            Console.WriteLine(GuiMessages.Welcome);

            var exit = false;

            while (!exit)
            {
                var fileType = GetEnumOption<FileType>(GuiMessages.AskForFileTypes);
                var isEncrypted = GetYesOrNo(GuiMessages.AskForEncryption);
                var isSecure = GetYesOrNo(GuiMessages.AskForUsingRole);
                var role = RoleType.Undefined;

                if (isSecure)
                    role = GetEnumOption<RoleType>(GuiMessages.AskForRole);

                Console.WriteLine(GuiMessages.FileContent);

                try
                {
                    Console.WriteLine(ReadFileContent(fileType, isEncrypted, isSecure, role));
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                exit = GetExit();
            }

            Console.WriteLine(GuiMessages.GoodBye);
            Console.ReadKey();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Reads a file content depending of the specified parameters
        /// </summary>
        /// <param name="fileType">The type of file to read</param>
        /// <param name="isEncrypted">Indicates if the file content is encrypted or not</param>
        /// <param name="isSecure">Indicates if the system should check for user role</param>
        /// <param name="role">The user role</param>
        /// <returns>
        /// Returns the file content read
        /// </returns>
        private static string ReadFileContent(FileType fileType, bool isEncrypted, bool isSecure, RoleType role = RoleType.Undefined)
        {
            IFileReader reader = null;
            IEncrypter encrypter = null;
            IAuthorizer authorizer = null;
            IFileReaderSecurized securizedReader = null;
            string content = string.Empty;
            string path;

            if (isEncrypted)
                encrypter = new Encrypter();

            if (isSecure)
                authorizer = new Authorizer(GetPermissions());

            if (fileType == FileType.Text)
            {
                reader = new FileReaderText();

                if (isSecure)
                {
                    path = isEncrypted ? ENCRYPTED_TEXT : PRIVATE_TEXT;
                    securizedReader = new FileReaderSecurizedText(authorizer, (FileReaderText)reader);
                    content = securizedReader.Read(Enum.GetName(typeof(RoleType), role), path);
                }
                else
                {
                    path = isEncrypted ? ENCRYPTED_TEXT : PUBLIC_TEXT;
                    content = reader.Read(path);
                }

                if (isEncrypted)
                    content = encrypter.Decrypt(content);
            }
            else if (fileType == FileType.Xml)
            {
                reader = new FileReaderXml();

                if (isSecure)
                {
                    path = isEncrypted ? ENCRYPTED_XML : PRIVATE_XML;
                    securizedReader = new FileReaderSecurizedXml(authorizer, (FileReaderXml)reader);
                    content = securizedReader.Read(Enum.GetName(typeof(RoleType), role), path);
                }
                else
                {
                    path = isEncrypted ? ENCRYPTED_XML : PUBLIC_XML;
                    content = reader.Read(path);
                }

                if (isEncrypted)
                    content = encrypter.Decrypt(content);
            }
            else if (fileType == FileType.Json)
            {
                reader = new FileReaderJson();

                if (isSecure)
                {
                    path = isEncrypted ? ENCRYPTED_JSON : PRIVATE_JSON;
                    securizedReader = new FileReaderSecurizedJson(authorizer, (FileReaderJson)reader);
                    content = securizedReader.Read(Enum.GetName(typeof(RoleType), role), path);
                }
                else
                {
                    path = isEncrypted ? ENCRYPTED_JSON : PUBLIC_JSON;
                    content = reader.Read(path);
                }

                if (isEncrypted)
                    content = encrypter.Decrypt(content);
            }

            return content;
        }

        /// <summary>
        /// Determines if the user had type yes or no
        /// </summary>
        /// <param name="question">The question to be made to the user</param>
        /// <returns>Returns true if the answer is Yes, or false in other case</returns>
        private static bool GetYesOrNo(string question)
        {
            string cmd = null;

            Console.Write(question);
            Console.Write(GuiMessages.TypeYesOrNo);
            Console.Write(Environment.NewLine);

            var success = false;

            while (!success)
            {
                Console.Write(PROMPT_TXT);

                cmd = Console.ReadLine().ToLower();

                success = CheckYesNoCommand(cmd);
            }

            var result = char.Equals(cmd.First(), YES_CMD);

            return result;
        }

        /// <summary>
        /// Checks if user had type yes or no commands
        /// </summary>
        /// <param name="cmd">The command typed by the user</param>
        private static bool CheckYesNoCommand(string cmd)
        {
            return !string.IsNullOrWhiteSpace(cmd) 
                && (char.Equals(cmd.First(), YES_CMD) 
                    || char.Equals(cmd.First(), NO_CMD));
        }

        /// <summary>
        /// Returns if the user had type "exit" command
        /// </summary>
        private static bool GetExit()
        {
            Console.WriteLine(GuiMessages.AskForExit);
            Console.Write(PROMPT_TXT);

            var cmd = Console.ReadLine().ToLower();
            var result = string.Equals(cmd, EXIT_CMD);

            return result;
        }

        /// <summary>
        /// Show all options for the specified enum type and returns the user selection
        /// </summary>
        /// <typeparam name="TEnum">The enum type to get user selection</typeparam>
        /// <param name="question">The question to be shown to the user</param>
        private static TEnum GetEnumOption<TEnum>(string question) where TEnum : struct, IConvertible
        {
            Console.WriteLine(question);

            foreach (var type in Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Where(itm => Convert.ToInt32(itm) != -1))
            {
                var name = Enum.GetName(typeof(TEnum), type);

                Console.WriteLine(GuiMessages.EmumCommandFormat, Convert.ToInt32(type), name);
            }

            Console.Write(PROMPT_TXT);

            var result = Enum.ToObject(typeof(TEnum), Convert.ToInt32(Console.ReadLine()));

            return (TEnum)result;
        }

        /// <summary>
        /// Returns a dictionary with the sample files permissions
        /// </summary>
        private static IDictionary<string, IEnumerable<string>> GetPermissions()
        {
            var result = new Dictionary<string, IEnumerable<string>>
            {
                { PRIVATE_JSON, new List<string> { Enum.GetName(typeof(RoleType), RoleType.Admin) } },
                { PRIVATE_TEXT, new List<string> { Enum.GetName(typeof(RoleType), RoleType.Admin) } },
                { PRIVATE_XML, new List<string> { Enum.GetName(typeof(RoleType), RoleType.Admin) } },
                { PUBLIC_JSON, new List<string> { Enum.GetName(typeof(RoleType), RoleType.Admin), Enum.GetName(typeof(RoleType), RoleType.User) } },
                { PUBLIC_TEXT, new List<string> { Enum.GetName(typeof(RoleType), RoleType.Admin), Enum.GetName(typeof(RoleType), RoleType.User) } },
                { PUBLIC_XML, new List<string> { Enum.GetName(typeof(RoleType), RoleType.Admin), Enum.GetName(typeof(RoleType), RoleType.User) } },
                { ENCRYPTED_JSON, new List<string> { Enum.GetName(typeof(RoleType), RoleType.Admin) } },
                { ENCRYPTED_TEXT, new List<string> { Enum.GetName(typeof(RoleType), RoleType.Admin) } },
                { ENCRYPTED_XML, new List<string> { Enum.GetName(typeof(RoleType), RoleType.Admin) } }
            };

            return result;
        }

        #endregion
    }
}
