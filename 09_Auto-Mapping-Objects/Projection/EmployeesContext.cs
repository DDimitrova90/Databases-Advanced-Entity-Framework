namespace Projection
{
    using Models;
    using System.Data.Entity;

    public class EmployeesContext : DbContext
    {
        public EmployeesContext()
            : base("name=EmployeesContext")
        {
            Database.SetInitializer(new SeedData());
        }

        public virtual DbSet<Employee> Employees { get; set; }
    }
}