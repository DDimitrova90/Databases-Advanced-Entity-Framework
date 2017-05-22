namespace PhotoShare.Client.Core.Commands
{
    using Service;
    using System;

    public class DeleteUser
    {
        private readonly UserService userService;

        public DeleteUser(UserService userService)
        {
            this.userService = userService;
        }

        // DeleteUser <username>
        public string Execute(string[] data)
        {
            string username = data[0];

            if (!this.userService.IsExistingByUsername(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }

            this.userService.Remove(username);

            return $"User {username} was deleted successfully!";

            //using (PhotoShareContext context = new PhotoShareContext())
            //{
            //    var user = context.Users.FirstOrDefault(u => u.Username == username);
            //    if (user == null)
            //    {
            //        throw new InvalidOperationException($"User with {username} was not found!");
            //    }

            //    // TODO: Delete User by username (only mark him as inactive)
            //    context.SaveChanges();

            //    return $"User {username} was deleted from the database!";
            //}
        }
    }
}
