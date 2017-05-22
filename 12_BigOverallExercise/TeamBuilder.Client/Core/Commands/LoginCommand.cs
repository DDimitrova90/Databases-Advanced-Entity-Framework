namespace TeamBuilder.Client.Core.Commands
{
    using Service;
    using System;

    public class LoginCommand
    {
        // Login <username> <password>
        public string Execute(string[] data)
        {
            string username = data[0];
            string password = data[1];

            if (data.Length != 2)
            {
                throw new FormatException("Invalid arguments count!");
            }

            SecurityService.Login(username, password);

            return $"User {username} successfully logged in!";
        }
    }
}
