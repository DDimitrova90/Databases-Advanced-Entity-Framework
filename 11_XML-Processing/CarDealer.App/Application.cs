namespace CarDealer.App
{
    using System;
    using Data;
    using Models;
    using System.Xml.Linq;
    using System.Linq;

    public class Application
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
            //Cars(context);

            // Problem 6, Query 2
            //CarsFromMakeFerrari(context);

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
            XDocument xmlSuppliers = XDocument.Load("../../Import/suppliers.xml");

            var suppliers = xmlSuppliers.Root.Elements();

            foreach (var sup in suppliers)
            {
                string name = sup.Attribute("name").Value;
                bool isImporter = bool.Parse(sup.Attribute("is-importer").Value);

                Supplier supplier = new Supplier();
                supplier.Name = name;
                supplier.IsImporter = isImporter;

                context.Suppliers.Add(supplier);
            }

            context.SaveChanges();
        }

        public static void ImportParts(CarDealerContext context)
        {
            XDocument xmlParts = XDocument.Load("../../Import/parts.xml");

            var parts = xmlParts.Root.Elements();

            foreach (var part in parts)
            {
                string name = part.Attribute("name").Value;
                decimal price = decimal.Parse(part.Attribute("price").Value);
                int quantity = int.Parse(part.Attribute("quantity").Value);

                Random rnd = new Random();
                int index = rnd.Next(1, context.Suppliers.Count());

                Supplier supplier = context.Suppliers.Find(index);

                Part newPart = new Part()
                {
                    Name = name,
                    Price = price,
                    Quantity = quantity,
                    Supplier = supplier
                };

                context.Parts.Add(newPart);
            }

            context.SaveChanges();
        }

        public static void ImportCars(CarDealerContext context)
        {
            XDocument xmlCars = XDocument.Load("../../Import/cars.xml");

            var cars = xmlCars.Root.Elements();

            foreach (var currCar in cars)
            {
                string make = currCar.Element("make").Value;
                string model = currCar.Element("model").Value;
                long travelledDistance = long.Parse(currCar.Element("travelled-distance").Value);

                Car car = new Car()
                {
                    Make = make,
                    Model = model,
                    TravelledDistance = travelledDistance
                };


                Random rnd = new Random();

                for (int i = 0; i < 20; i++)
                {
                    int index = rnd.Next(1, context.Parts.Count());
                    car.Parts.Add(context.Parts.Find(index));
                }

                context.Cars.Add(car);
            }

            context.SaveChanges();
        }

        public static void ImportCustomers(CarDealerContext context)
        {
            XDocument xmlCustomers = XDocument.Load("../../Import/customers.xml");

            var customers = xmlCustomers.Root.Elements();

            foreach (var cust in customers)
            {
                string name = cust.Attribute("name").Value;
                DateTime birthDate = DateTime.Parse(cust.Element("birth-date").Value);
                bool isYoungDriver = bool.Parse(cust.Element("is-young-driver").Value);

                Customer customer = new Customer()
                {
                    Name = name,
                    BirthDate = birthDate,
                    IsYoungDriver = isYoungDriver
                };

                context.Customers.Add(customer);
            }

            context.SaveChanges();
        }

        public static void ImportSales(CarDealerContext context)
        {
            int carsCount = context.Cars.Count();
            int customersCount = context.Customers.Count();
            decimal[] discounts = new decimal[] { 0.0M, 0.05M, 0.1M, 0.15M, 0.2M, 0.3M, 0.4M, 0.5M };
            Random rnd = new Random();

            for (int i = 0; i < carsCount; i++)
            {
                int discountIndex = rnd.Next(0, discounts.Length - 1);
                int carIndex = rnd.Next(1, carsCount);
                int customerIndex = rnd.Next(1, customersCount);
                
                Sale sale = new Sale();
                sale.Discount = discounts[discountIndex];
                sale.Car = context.Cars.Find(carIndex);
                sale.Customer = context.Customers.Find(customerIndex);

                context.Sales.Add(sale);
            }

            context.SaveChanges();
        }

        public static void Cars(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.TravelledDistance > 2000000)
                .OrderBy(c => c.Make)
                .ThenBy(c => c.Model)
                .ToList();

            XDocument xmlCars = new XDocument();
            XElement rootElement = new XElement("cars");

            foreach (var car in cars)
            {
                XElement carElement = new XElement("car");

                XElement makeElement = new XElement("make", car.Make);
                XElement modelElement = new XElement("model", car.Model);
                XElement travelledDistanceElement = new XElement("travelled-distance", car.TravelledDistance);

                carElement.Add(makeElement);
                carElement.Add(modelElement);
                carElement.Add(travelledDistanceElement);

                rootElement.Add(carElement);
            }

            xmlCars.Add(rootElement);
            xmlCars.Save("cars.xml");
        }

        public static void CarsFromMakeFerrari(CarDealerContext context)
        {
            var cars = context.Cars
                .Where(c => c.Make == "Ferrari")
                .OrderBy(c => c.Model)
                .ThenByDescending(c => c.TravelledDistance)
                .ToList();

            XDocument xmlCars = new XDocument();
            XElement rootElement = new XElement("cars");

            foreach (var car in cars)
            {
                XElement carElement = new XElement("car");
                carElement.SetAttributeValue("id", car.Id);
                carElement.SetAttributeValue("model", car.Model);
                carElement.SetAttributeValue("travelled-distance", car.TravelledDistance);

                rootElement.Add(carElement);
            }

            xmlCars.Add(rootElement);
            xmlCars.Save("ferrari-cars.xml");
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

            XDocument xmlSuppliers = new XDocument();
            XElement rootElement = new XElement("suppliers");

            foreach (var sup in suppliers)
            {
                XElement supplierElement = new XElement("supplier");
                supplierElement.SetAttributeValue("id", sup.Id);
                supplierElement.SetAttributeValue("name", sup.Name);
                supplierElement.SetAttributeValue("parts-count", sup.PartsCount);

                rootElement.Add(supplierElement);
            }

            xmlSuppliers.Add(rootElement);
            xmlSuppliers.Save("local-suppliers.xml");
        }

        public static void CarsWithTheirListOfParts(CarDealerContext context)
        {
            var cars = context.Cars
                .Select(c => new
                {
                    Make = c.Make,
                    Model = c.Model,
                    TravelledDistance = c.TravelledDistance,
                    Parts = c.Parts.Select(p => new
                    {
                        Name = p.Name,
                        Price = p.Price
                    })
                })
                .ToList();

            XDocument xmlCars = new XDocument();
            XElement rootElement = new XElement("cars");

            foreach (var car in cars)
            {
                XElement carElement = new XElement("car");
                carElement.SetAttributeValue("make", car.Make);
                carElement.SetAttributeValue("model", car.Model);
                carElement.SetAttributeValue("travelled-distance", car.TravelledDistance);

                XElement partsElement = new XElement("parts");

                foreach (var part in car.Parts)
                {
                    XElement partElement = new XElement("part");
                    partElement.SetAttributeValue("name", part.Name);
                    partElement.SetAttributeValue("price", part.Price);

                    partsElement.Add(partElement);
                }

                carElement.Add(partsElement);
                rootElement.Add(carElement);
            }

            xmlCars.Add(rootElement);
            xmlCars.Save("cars-and-parts.xml");
        }

        public static void TotalSalesByCustomer(CarDealerContext context)
        {
            var customers = context.Customers
                .Where(c => c.Sales.Count() >= 1)
                .Select(c => new
                {
                    FullName = c.Name,
                    BoughtCars = c.Sales.Count(),
                    SpentMoney = c.Sales.Sum(s => s.Car.Parts.Sum(p => p.Price) * (1 - s.Discount))
                })
                .ToList()
                .OrderByDescending(c => c.SpentMoney)
                .ThenByDescending(c => c.BoughtCars)
                .ToList();

            XDocument xmlCustomers = new XDocument();
            XElement rootElement = new XElement("customers");

            foreach (var cust in customers)
            {
                XElement customerElement = new XElement("customer");
                customerElement.SetAttributeValue("full-name", cust.FullName);
                customerElement.SetAttributeValue("bought-cars", cust.BoughtCars);
                customerElement.SetAttributeValue("spent-money", cust.SpentMoney);

                rootElement.Add(customerElement);
            }

            xmlCustomers.Add(rootElement);
            xmlCustomers.Save("customers-total-sales.xml");
        }

        public static void SalesWithAppliedDiscount(CarDealerContext context)
        {
            var sales = context.Sales
                .Select(s => new
                {
                    Car = new
                    {
                        Make = s.Car.Make,
                        Model = s.Car.Model,
                        TravelledDistance = s.Car.TravelledDistance
                    },
                    Customer = new
                    {
                        Name = s.Customer.Name
                    },
                    Discount = s.Discount,
                    Price = s.Car.Parts.Sum(p => p.Price),
                    PriceWithDiscount = s.Car.Parts.Sum(p => p.Price) * (1 - s.Discount)
                })
                .ToList();

            XDocument xmlSales = new XDocument();
            XElement rootElement = new XElement("sales");

            foreach (var sale in sales)
            {
                XElement saleElement = new XElement("sale");

                XElement carElement = new XElement("car");
                carElement.SetAttributeValue("make", sale.Car.Make);
                carElement.SetAttributeValue("model", sale.Car.Model);
                carElement.SetAttributeValue("travelled-distance", sale.Car.TravelledDistance);

                XElement customerElement = new XElement("customer-name", sale.Customer.Name);
                XElement discountElement = new XElement("discount", sale.Discount);
                XElement priceElement = new XElement("price", sale.Price);
                XElement priceWithDiscountElement = new XElement("price-with-discount", sale.PriceWithDiscount);

                saleElement.Add(carElement);
                saleElement.Add(customerElement);
                saleElement.Add(discountElement);
                saleElement.Add(priceElement);
                saleElement.Add(priceWithDiscountElement);

                rootElement.Add(saleElement);
            }

            xmlSales.Add(rootElement);
            xmlSales.Save("sales-discounts.xml");
        }
    }
}
