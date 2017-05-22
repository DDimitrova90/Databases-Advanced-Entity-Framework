namespace _12_Remove_Inactive_Users
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RemoveInactiveUsers
    {
        public static void Main()
        {
            UserContext context = new UserContext();

            string loginDate = Console.ReadLine();
            DateTime date = Convert.ToDateTime(loginDate);

            int usersCount = context.Users.Where(u => u.LastTimeLoggedIn < date).Count();
            List<User> users = context.Users.Where(u => u.LastTimeLoggedIn < date).ToList();

            foreach (User us in users)
            {
                us.IsDeleted = true;
            }

            if (usersCount == 0)
            {
                Console.WriteLine("No users have been deleted");
            }
            else
            {
                Console.WriteLine($"{usersCount} user has been deleted");
            }

            foreach (User us in users)
            {
                context.Users.Remove(us);
            }

            context.SaveChanges();
        }
    }
}
