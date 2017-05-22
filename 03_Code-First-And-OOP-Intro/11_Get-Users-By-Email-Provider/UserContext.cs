namespace _11_Get_Users_By_Email_Provider
{
    using System.Data.Entity;

    public partial class UserContext : DbContext
    {
        public UserContext()
            : base("name=UsersContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
