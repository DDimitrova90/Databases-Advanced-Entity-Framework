namespace _09_Hospital_Database.Migrations
{
    using System.Data.Entity.Migrations;
  
    internal sealed class Configuration : DbMigrationsConfiguration<_09_Hospital_Database.HospitalContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "_09_Hospital_Database.HospitalContext";
        }

        protected override void Seed(_09_Hospital_Database.HospitalContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
