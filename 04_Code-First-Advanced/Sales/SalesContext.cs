namespace Sales
{
    using Migrations;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;


    public class SalesContext : DbContext
    {
        public SalesContext()
            : base("name=SalesContext")
        {
            Database.SetInitializer(new SeedData()); // Problem 3
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SalesContext, Configuration>());
        }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<StoreLocation> StoreLocations { get; set; }

        public virtual DbSet<Sale> Sales { get; set; }
    }
}