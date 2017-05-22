namespace TeamBuilder.Client.Core.Commands
{
    using Models;
    using Service;
    using System;

    public class LogoutCommand
    {
        // Logout
        public string Execute(string[] data)
        {
            if (data.Length > 0)
            {
                throw new FormatException("Invalid arguments count!");
            }

            User user = SecurityService.GetCurrentUser();

            if (user == null)
            {
                throw new InvalidOperationException("You should login first!");
            }

            SecurityService.Logout();

            return $"User {user.Username} successfully logged out!";
        }
    }
}
