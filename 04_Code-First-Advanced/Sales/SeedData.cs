namespace Sales
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using System.Data.Entity.Validation;

    public class SeedData : CreateDatabaseIfNotExists<SalesContext>
    {
        protected override void Seed(SalesContext context)
        {
            context.Sales.Add(
                new Sale
                {
                    Product = new Product
                    {
                        Name = "Bread",
                        Price = 1.20M,
                        Quantity = 25
                    },
                    Customer = new Customer
                    {
                        FirstName = "Ivan",
                        LastName = "Ivanov",
                        Email = "ivan.ivanov@abv.bg",
                        CreditCardNumber = "567BGKKG986969"
                    },
                    StoreLocation = new StoreLocation { LocationName = "Mladost-1" },
                    Date = new DateTime(2017, 03, 07)
                });

            context.Sales.Add(
               new Sale
               {
                   Product = new Product
                   {
                       Name = "Cheese",
                       Price = 12.58M,
                       Quantity = 10.30
                   },
                   Customer = new Customer
                   {
   
                       FirstName = "Dragan",
                       LastName = "Draganov",
                       Email = "dragan.draganov@abv.bg",
                       CreditCardNumber = "18742GHHH75775784"
                   },
                   StoreLocation = new StoreLocation { LocationName = "Vitoshka" },
                   Date = new DateTime(2017, 01, 13)
               });

            context.Sales.Add(
               new Sale
               {
                   Product = new Product
                   {
                       Name = "Salami",
                       Price = 8.29M,
                       Quantity = 5.426
                   },
                   Customer = new Customer
                   {
                       FirstName = "Petkan",
                       LastName = "Petkanov",
                       Email = "petkan.petkanov@abv.bg",
                       CreditCardNumber = "57854JHJHFGD5421545"
                   },
                   StoreLocation = new StoreLocation { LocationName = "Studentski grad" },
                   Date = new DateTime(2017, 03, 07)
               });

            context.Sales.Add(
               new Sale
               {
                   Product = new Product
                   {
                       Name = "Eggs",
                       Price = 0.30M,
                       Quantity = 90
                   },
                   Customer = new Customer
                   {
                       FirstName = "Stefan",
                       LastName = "Stefanov",
                       Email = "stefan.stefanov@abv.bg",
                       CreditCardNumber = "321245GHFHG545HFHG"
                   },
                   StoreLocation = new StoreLocation { LocationName = "Krasno selo" },
                   Date = new DateTime(2017, 03, 07)
               });

            context.Sales.Add(
               new Sale
               {
                   Product = new Product
                   {
                       Name = "Tomato",
                       Price = 3.20M,
                       Quantity = 3.75
                   },
                   Customer = new Customer
                   {
                       FirstName = "Galio",
                       LastName = "Galev",
                       Email = "galio.galev@abv.bg",
                       CreditCardNumber = "218YFHGFT5454JHFGD"
                   },
                   StoreLocation = new StoreLocation { LocationName = "Graf Ignatiev" },
                   Date = new DateTime(2017, 03, 07)
               });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
