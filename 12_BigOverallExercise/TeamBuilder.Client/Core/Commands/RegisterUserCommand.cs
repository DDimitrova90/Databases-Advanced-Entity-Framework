namespace TeamBuilder.Client.Core.Commands
{
    using System;
    using Service;

    public class RegisterUserCommand
    {
        private readonly UserService userService;

        public RegisterUserCommand(UserService userService)
        {
            this.userService = userService;
        }

        // RegisterUser <username> <password> <repeat-password> <firstName> <lastName> <age> <gender>
        public string Execute(string[] data)
        {
            string username = data[0];
            string password = data[1];
            string repeatPassword = data[2];
            string firstName = data[3];
            string lastName = data[4];
            string age = data[5];
            string gender = data[6];

            if (data.Length != 7)
            {
                throw new FormatException("Invalid arguments count!");
            }

            if (!this.userService.IsUsernameValid(username))
            {
                throw new ArgumentException($"Username {username} not valid!");
            }

            if (!this.userService.IsPasswordValid(password))
            {
                throw new ArgumentException($"Password {password} is not valid!");
            }

            if (!this.userService.IsAgeValid(age))
            {
                throw new ArgumentException("Age not valid!");
            }

            if (!this.userService.IsGenderValid(gender))
            {
                throw new ArgumentException("Gender should be either “Male” or “Female”!");
            }

            if (password != repeatPassword)
            {
                throw new InvalidOperationException("Passwords do not match!");
            }

            if (this.userService.IsExistingByUsername(username))
            {
                throw new InvalidOperationException($"Username {username} is already taken!");
            }

            if (SecurityService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should logout first!");
            }

            this.userService.Register(username, password, firstName, lastName, age, gender);

            return $"User {username} was registered successfully!";          
        }
    }
}
