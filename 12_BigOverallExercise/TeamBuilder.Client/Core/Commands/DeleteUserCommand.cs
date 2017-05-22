namespace TeamBuilder.Client.Core.Commands
{
    using Models;
    using Service;
    using System;

    public class DeleteUserCommand
    {
        private readonly UserService userService;

        public DeleteUserCommand(UserService userService)
        {
            this.userService = userService;
        }

        // DeleteUser
        public string Execute(string[] data)
        {
            if (data.Length > 0)
            {
                throw new FormatException("Invalid arguments count!");
            }

            if (!SecurityService.IsAuthenticated())
            {
                throw new InvalidOperationException("You should login first!");
            }

            User loggedUser = SecurityService.GetCurrentUser();
            this.userService.Delete(loggedUser.Username);

            return $"User {loggedUser.Username} was deleted successfully!";
        }
    }
}
