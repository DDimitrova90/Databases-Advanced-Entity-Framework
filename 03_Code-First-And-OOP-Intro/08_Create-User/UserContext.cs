namespace _08_Create_User
{
    using System.Data.Entity;

    public class UserContext : DbContext
    {
        public UserContext()
            : base("name=UserContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
    }
}