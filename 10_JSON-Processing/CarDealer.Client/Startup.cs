namespace CarDealer.Client
{
    using Models;
    using Data;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;

    public class Startup
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();
            //context.Database.Initialize(true);

            //ImportSuppliers(context);

            //ImportParts(context);

            //ImportCars(context);

            //ImportCustomers(context);

            //ImportSales(context);

            // Problem 6, Query 1
            //OrderedCustomers(context);

            // Problem 6, Query 2
            //CarsFromMakeToyota(context);

            // Problem 6, Query 3
            //LocalSuppliers(context);

            // Problem 6, Query 4
            //CarsWithTheirListOfParts(context);

            // Problem 6, Query 5
            //TotalSalesByCustomer(context);

            // Problem 6, Query 6
            //SalesWithAppliedDiscount(context);
        }

        public static void ImportSuppliers(CarDealerContext context)
        {
            var jsonSuppliers = File.ReadAllText(@"../../Import/suppliers.json");

            List<Supplier> suppliers = JsonConvert.DeserializeObject<List<Supplier>>(jsonSuppliers);

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
        }

        public static void ImportParts(CarDealerContext context)
        {
            var jsonParts = File.ReadAllText(@"../../Import/parts.json");

            List<Part> parts = JsonConvert.DeserializeObject<List<Part>>(jsonParts);

            int number = 0;
            int suppliersCount = context.Suppliers.Count();

            foreach (Part p in parts)
            {
                p.Supplier = context.Suppliers.Find(number % suppliersCount + 1);

                number++;
            }

            context.Parts.AddRange(parts);
            context.SaveChanges();
        }

        public static void ImportCars(CarDealerContext context)
        {
            var jsonCars = File.ReadAllText(@"../../Import/cars.json");

            List<Car> cars = JsonConvert.DeserializeObject<List<Car>>(jsonCars);

            int number = 0;
            int partsCount = context.Parts.Count();

            foreach (Car car in cars)
            {
                for (int i = 0; i < 20; i++)
                {
                    car.Parts.Add(context.Parts.Find(number % partsCount + 1));

                    number++;
                }
            }

            context.Cars.AddRange(cars);
            context.SaveChanges();
        }

        public static void ImportCustomers(CarDealerContext context)
        {
            var jsonCustomers = File.ReadAllText(@"../../Import/customers.json");

            List<Customer> customers = JsonConvert.DeserializeObject<List<Customer>>(jsonCustomers);

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        public static void ImportSales(CarDealerContext context)
        {
            int number = 0;
            int carsCount = context.Cars.Count();
            int customersCount = context.Customers.Count();

            double[] discounts = new double[] { 0.0, 0.05, 0.1, 0.15, 0.2, 0.3, 0.4, 0.5};
            Random random = new Random();
            
            for (int i = 0; i < carsCount; i++)
            {
                int index = random.Next(0, discounts.Length);
                Sale sale = new Sale();
                sale.Car = context.Cars.Find(number % carsCount + 1);
                sale.Customer = context.Customers.Find(number * 2 % customersCount + 1);

                double discount = discounts[index];

                sale.Discount = discount;

                number++;

                context.Sales.Add(sale);
            }

            context.SaveChanges();

            List<Sale> sales = context.Sales.ToList();

            foreach (Sale s in sales)
            {
                if (s.Customer.IsYoungDriver == true)
                {
                    s.Discount += 0.05;
                }
            }

            context.SaveChanges();
        }

        public static void OrderedCustomers(CarDealerContext context)
        {
            List<Sale> sales = new List<Sale>();

            var customers = context.Customers
                .OrderBy(c => c.BirthDate)
                .ThenBy(c => c.IsYoungDriver)
                .Select(c => new
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate,
                    IsYoungDriver = c.IsYoungDriver,
                    Sales = sales
                })
                .ToList();

            var jsonCustomers = JsonConvert.SerializeObject(customers, Formatting.Indented);

            Console.WriteLine(jsonCustomers);
        }

        public static void CarsFromMakeToyota(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "Toyota")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .Select(c => new
                {
                    Id = c.Id,
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance
                })
                .ToList();

            var jsonCars = JsonConvert.SerializeObject(cars, Formatting.Indented);

            Console.WriteLine(jsonCars);
        }

        public static void LocalSuppliers(CarDealerContext context)
        {
            var suppliers = context.Suppliers
                .Where(s => s.IsImporter == false)
                .Select(s => new
                {
                    Id = s.Id,
                    Name = s.Name,
                    PartsCount = s.Parts.Count()
                })
                .ToList();

            var jsonSuppliers = JsonConvert.SerializeObject(suppliers, Formatting.Indented);

            Console.WriteLine(jsonSuppliers);
        }

        public static void CarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new
                {
                    Car = new
                    {
                        Make = c.Make,
                        Model = c.Model,
                        TravelledDistance = c.TravelledDistance
                    },
                    Parts = c.Parts.Select(p => new
                    {
                        Name = p.Name,
                        Price = p.Price
                    })
                })
                .ToList();

            var jsonCars = JsonConvert.SerializeObject(cars, Formatting.Indented);

            Console.WriteLine(jsonCars);
        }

        public static void TotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count() >= 1)
                .Select(c => new
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count(),
                    SpentMoney = c.Sales.Sum(s => s.Car.Parts.Sum(p => p.Price))
                })
                .OrderByDescending(c => c.SpentMoney)
                .ThenByDescending(c => c.BoughtCars)
                .ToList();

            var jsonCustomers = JsonConvert.SerializeObject(customers, Formatting.Indented);

            Console.WriteLine(jsonCustomers);
        }

        public static void SalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .ToList()
                .Select(s => new
                {
                    Car = new
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    CustomerName = s.Customer.Name,
                    Discount = s.Discount,
                    Price = s.Car.Parts.Sum(p => p.Price),
                    PriceWithDiscount = s.Car.Parts.Sum(p => p.Price) * (decimal)(1 - s.Discount)
                })
                .ToList();

            var jsonSales = JsonConvert.SerializeObject(sales, Formatting.Indented);

            Console.WriteLine(jsonSales);
        }
    }
}
