namespace _11_Get_Users_By_Email_Provider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class GetUsersByEmailProvider
    {
        public static void Main()
        {
            UserContext context = new UserContext();

            context.Users.Add(
                new User()
                {
                    Username = "Hu",
                    Password = "WVW61OYM6OV",
                    Email = "Sed@Sedid.edu",
                    RegisteredOn = new DateTime(2008, 12, 27),
                    LastTimeLoggedIn = new DateTime(2015, 01, 31),
                    Age = 53,
                    IsDeleted = false
                });

            context.Users.Add(
                new User()
                {
                    Username = "Roary",
                    Password = "UMN71WPN1AA",
                    Email = "Praesent.luctus@velfaucibusid.edu",
                    RegisteredOn = new DateTime(2000, 05, 08),
                    LastTimeLoggedIn = new DateTime(2015, 05, 08),
                    Age = 82,
                    IsDeleted = false
                });

            context.Users.Add(
                new User()
                {
                    Username = "Lee",
                    Password = "XEQ84AMC0IJ",
                    Email = "odio@lectusNullamsuscipit.org",
                    RegisteredOn = new DateTime(2008, 06, 05),
                    LastTimeLoggedIn = new DateTime(2013, 10, 24),
                    Age = 35,
                    IsDeleted = false
                });

            context.Users.Add(
                new User()
                {
                    Username = "Denton",
                    Password = "OAU39UFV7KE",
                    Email = "nonummy@sit.org",
                    RegisteredOn = new DateTime(2008, 04, 26),
                    LastTimeLoggedIn = new DateTime(2016, 02, 22),
                    Age = 48,
                    IsDeleted = false
                });

            context.SaveChanges();

            string emailProvider = Console.ReadLine();

            List<User> users = context.Users
                .Where(u => u.Email.EndsWith(emailProvider))
                .ToList();

            foreach (User user in users)
            {
                Console.WriteLine($"{user.Username} {user.Email}");
            }
        }
    }
}
