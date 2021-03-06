﻿namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Service;
    using System;

    public class LogoutCommand
    {
        public string Execute(string[] data)
        {
            User user = SecurityService.GetCurrentUser();

            if (user == null)
            {
                throw new InvalidOperationException("You should login first in order to logout.");
            }

            SecurityService.Logout();

            return $"User {user.Username} successfully logged out!";
        }
    }
}
