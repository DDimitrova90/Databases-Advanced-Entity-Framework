namespace TeamBuilder.Service
{
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SecurityService
    {
        private static User loggedUser;

        public static void Login(string username, string password)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User user = context.Users.SingleOrDefault(u => u.Username == username && u.Password == password);

                if (user == null || user.IsDeleted == true)
                {
                    throw new ArgumentException("Invalid username or password!");
                }

                if (loggedUser != null)
                {
                    throw new InvalidOperationException("You should logout first!");
                }

                loggedUser = user;
            }
        }

        public static void Logout()
        {
            if (loggedUser == null)
            {
                throw new InvalidOperationException("You should login first!");
            }

            loggedUser = null;
        }

        public static bool IsAuthenticated()
        {
            return loggedUser != null;
        }

        public static User GetCurrentUser()
        {
            return loggedUser;
        }
    }
}
