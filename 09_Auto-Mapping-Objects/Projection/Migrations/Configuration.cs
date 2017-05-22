namespace Projection.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Projection.EmployeesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Projection.EmployeesContext";
        }

        protected override void Seed(Projection.EmployeesContext context)
        {
  
        }
    }
}
