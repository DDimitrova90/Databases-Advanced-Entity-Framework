namespace Sales.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Sales.SalesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Sales.SalesContext";
        }

        protected override void Seed(Sales.SalesContext context)
        {
            List<Customer> customers = context.Customers.ToList();

            for (int i = 0; i < customers.Count; i++)
            {
                Customer cus = customers[i];
                string firtName = cus.Email.Substring(0, cus.Email.IndexOf('.'));
                cus.FirstName = firtName.Substring(0, 1).ToUpper() + firtName.Substring(1);
                string lastName = cus.Email
                    .Substring(cus.Email.IndexOf('.') + 1, cus.Email.IndexOf('@') - cus.Email.IndexOf('.') - 1);
                cus.LastName = lastName.Substring(0, 1).ToUpper() + lastName.Substring(1);
            }

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
