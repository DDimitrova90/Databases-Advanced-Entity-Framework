namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Service;
    using System;
    using System.Linq;

    public class ModifyUserCommand
    {
        private readonly UserService userService;
        private readonly TownService townService;

        public ModifyUserCommand(UserService userService, TownService townService)
        {
            this.userService = userService;
            this.townService = townService;
        }

        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string[] data)
        {
            string username = data[0];
            string propType = data[1];
            string value = data[2];

            User user = this.userService.GetUserByUsername(username);

            if (user == null)
            {
                throw new ArgumentException($"User {username} not found!");
            }

            if (propType == "Password")
            {
                if (!(value.Any(c => char.IsLower(c)) && value.Any(c => char.IsDigit(c))))
                {
                    throw new ArgumentException($"Value {value} not valid.\nInvalid Password!");
                }

                user.Password = value;
            }
            else if (propType == "BornTown")
            {
                Town town = this.townService.GetByTownName(value);

                if (town == null)
                {
                    throw new ArgumentException($"Value {value} not valid.\nTown {value} not found!");
                }

                user.BornTown = town;
            }
            else if (propType == "CurrentTown")
            {
                Town town = this.townService.GetByTownName(value);

                if (town == null)
                {
                    throw new ArgumentException($"Value {value} not valid.\nTown {value} not found!");
                }

                user.CurrentTown = town;
            }
            else
            {
                throw new ArgumentException($"Property {propType} not supported!");
            }

            this.userService.UpdateUser(user);

            return $"User {username} {propType} is {value}.";
        }
    }
}
