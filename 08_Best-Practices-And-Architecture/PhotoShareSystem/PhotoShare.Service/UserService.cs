namespace PhotoShare.Service
{
    using Models;
    using Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;

    public class UserService
    {
        public virtual void Add(string username, string password, string email)
        {
            User user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false,
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now
            };

            using (PhotoShareContext context = new PhotoShareContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public bool IsExistingByUsername(string username)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Users.Any(u => u.Username == username);
            }
        }

        public User GetUserByUsername(string username)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Users.AsNoTracking().SingleOrDefault(u => u.Username == username);
            }
        }

        public void Remove(string username)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                User user = context.Users.SingleOrDefault(u => u.Username == username);

                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }

                if (user.IsDeleted != null && user.IsDeleted.Value)
                {
                    throw new InvalidOperationException($"User {username} was already deleted!");
                }

                user.IsDeleted = true;
                context.SaveChanges();
            }
        }

        public void UpdateUser(User user)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                // If we add in User Model properties like:
                // [ForeignKey("BornTown")]
                // public int BornTown_Id { get; set; }
                // we can use:
                // Attach the user: context.Users.Attach(user);
                // ModifyState: context.Entry(user).State = EntityState.Modified;
                // context.SaveChanges();
                // without the code under!!!


                User userInBase = context.Users
                    .Include("BornTown")
                    .Include("CurrentTown")
                    .SingleOrDefault(u => u.Id == user.Id); 

                if (userInBase != null)
                {
                    if (user.Password != userInBase.Password)
                    {
                        userInBase.Password = user.Password;
                    }

                    if (user.BornTown != null && 
                        (userInBase.BornTown == null || user.BornTown.Id != userInBase.BornTown.Id))
                    {
                        userInBase.BornTown = context.Towns.Find(user.BornTown.Id);
                    }

                    if (user.CurrentTown != null &&
                       (userInBase.CurrentTown == null || user.CurrentTown.Id != userInBase.CurrentTown.Id))
                    {
                        userInBase.CurrentTown = context.Towns.Find(user.CurrentTown.Id);
                    }

                    context.Entry(userInBase).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public bool AreFriends(string requesterUsername, string friendUsername)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                User requester = context.Users
                    .Include("Friends")
                    .SingleOrDefault(u => u.Username == requesterUsername);

                if (requester == null)
                {
                    return false;
                }

                return requester.Friends.Any(f => f.Username == friendUsername);
            }
        }

        public void MakeFriends(string requesterUsername, string friendUsername)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                User requester = context.Users.SingleOrDefault(u => u.Username == requesterUsername);

                User friend = context.Users.SingleOrDefault(u => u.Username == friendUsername);

                if (requester != null && friend != null)
                {
                    requester.Friends.Add(friend);
                    context.SaveChanges();
                }
            }
        }

        public IEnumerable<User> GetFriends(string username)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                User user = context.Users.Include("Friends").SingleOrDefault(u => u.Username == username);

                if (user == null)
                {
                    return new List<User>();
                }

                return user.Friends.ToList();
            }
        }
    }
}
