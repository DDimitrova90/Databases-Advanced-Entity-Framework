namespace TeamBuilder.Client.Core.Commands
{
    using Models;
    using Service;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class ImportUsersCommand
    {
        private readonly UserService userService;

        public ImportUsersCommand(UserService userService)
        {
            this.userService = userService;
        }

        // ImportUsers <filePathToXmlFile>
        public string Execute(string[] data)
        {
            string filePath = data[0];

            if (data.Length != 1)
            {
                throw new FormatException("Invalid arguments count!");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Path {filePath} is not valid!");
            }

            List<User> users = new List<User>();

            try
            {
                users = this.userService.GetUsersFromXml(filePath);
            }
            catch (Exception)
            {

                throw new FormatException("Xml format not valid!");
            }

            this.userService.AddUsers(users);

            return $"You have successfully imported {users.Count()} users!";
        }
    }
}
