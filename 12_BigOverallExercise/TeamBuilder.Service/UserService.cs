namespace TeamBuilder.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using Data;
    using System.Xml.Linq;

    public class UserService
    {
        public virtual void Register(string username, string password, string firstName, string lastName, string age, string gender)
        {
            Gender userGender;
            bool isGenderValid = Enum.TryParse(gender, out userGender);

            User user = new User()
            {
                Username = username,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Age = int.Parse(age),
                Gender = userGender
            };

            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void Delete(string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                User user = context.Users.SingleOrDefault(u => u.Username == username);

                user.IsDeleted = true;
                context.SaveChanges();
            }
        }

        public void AddUsers(List<User> users)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }

        public List<User> GetUsersFromXml(string filePath)
        {
            XDocument xmlUsers = XDocument.Load(filePath);
            List<User> users = new List<User>();

            var usersElements = xmlUsers.Root.Elements();

            foreach (var u in usersElements)
            {
                User user = new User();

                string username = u.Element("username").Value;
                string password = u.Element("password").Value;
                string firstName = u.Element("first-name").Value;
                string lastName = u.Element("last-name").Value;
                int age = int.Parse(u.Element("age").Value);
                string gender = u.Element("gender").Value;

                user.Username = username;
                user.Password = password;
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Age = age;
                user.Gender = (Gender)Enum.Parse(typeof(Gender), gender, true);

                users.Add(user);
            }

            return users;
        }

        public User GetUserByUsername(string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Users.SingleOrDefault(u => u.Username == username);
            }
        }

        public bool IsExistingByUsername(string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Users.Any(u => u.Username == username);
            }
        }

        public bool IsUsernameValid(string username)
        {
            if (username.Length >= 3 && username.Length <= 25)
            {
                return true;
            }

            return false;
        }

        public bool IsPasswordValid(string password)
        {
            if (password.Length < 6 || password.Length > 30)
            {
                return false;
            }

            if (!password.Any(c => char.IsUpper(c)) || !password.Any(c => char.IsDigit(c)))
            {
                return false;
            }

            return true;
        }

        public bool IsAgeValid(string age)
        {
            try
            {
                int userAge = int.Parse(age);

                if (userAge > 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {

                throw new ArgumentException("Age not valid!");
            }
        }

        public bool IsGenderValid(string gender)
        {
            if (gender == "Male" || gender == "Female")
            {
                return true;
            }

            return false;
        }
    }
}
