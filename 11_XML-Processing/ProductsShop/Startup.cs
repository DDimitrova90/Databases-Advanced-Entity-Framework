namespace ProductsShop
{
    using System.Linq;
    using System.Xml.Linq;
    using Data;
    using Model;
    using System;
    using System.Collections.Generic;

    public class Application
    {
        public static void Main()
        {
            ProductShopContext context = new ProductShopContext();
            //context.Database.Initialize(true);

            //ImportUsers(context);

            //ImportCategories(context);

            //ImportProducts(context);

            // Problem 4, Query 1
            //ProductsInRange(context);

            // Problem 4, Query 2
            //SoldProducts(context);

            // Problem 4, Query 3
            //CategoriesByProductsCount(context);

            // Problem 4, Query 4
            //UsersAndProducts(context);
        }

        public static void ImportCategories(ProductShopContext context)
        {
            XDocument categoriesDoc = XDocument.Load("../../Import/categories.xml");

            var categoriesElements = categoriesDoc.Root.Elements();

            foreach (var catElement in categoriesElements)
            {
                string name = catElement.Element("name").Value;

                Category category = new Category()
                {
                    Name = name
                };

                context.Categories.Add(category);
            }

            context.SaveChanges();
        }

        public static void ImportUsers(ProductShopContext context)
        {
            XDocument usersDoc = XDocument.Load("../../Import/users.xml");

            var usersElements = usersDoc.Root.Elements();

            foreach (var userElement in usersElements)
            {
                string firstName = userElement.Attribute("first-name")?.Value;
                string lastName = userElement.Attribute("last-name").Value;
                string ageStr = userElement.Attribute("age")?.Value;
                int age;

                User user = new User();
                user.FirstName = firstName;
                user.LastName = lastName;

                if (userElement.Attribute("age") != null)
                {
                    age = int.Parse(ageStr);
                    user.Age = age;
                }

                context.Users.Add(user);
            }

            context.SaveChanges();

            List<User> users = context.Users.ToList();
            int usersCount = context.Users.Count();

            foreach (User user in users)
            {
                Random rnd = new Random();
                int index = rnd.Next(1, usersCount);

                if (index != user.Id)
                {
                    user.MyFriends.Add(context.Users.Find(index));
                    context.Users.Find(index).MyFriends.Add(context.Users.Find(user.Id));
                }
            }

            context.SaveChanges();
        }

        public static void ImportProducts(ProductShopContext context)
        {
            XDocument productsDoc = XDocument.Load("../../Import/products.xml");

            var productsElements = productsDoc.Root.Elements();

            int number = 0;
            int usersCount = context.Users.Count();
            int categoriesCount = context.Categories.Count();

            foreach (var prod in productsElements)
            {
                string name = prod.Element("name").Value;
                decimal price = decimal.Parse(prod.Element("price").Value);

                Product product = new Product()
                {
                    Name = name,
                    Price = price,
                    SelledId = number % usersCount + 1
                };

                context.Products.Add(product);
                context.SaveChanges();

                if (product.Id % 3 == 0)
                {
                    product.BuyerId = number * 2 % usersCount + 1;
                }

                for (int i = 0; i < product.Id % 2 + 1; i++)
                {
                    product.Categories.Add(context.Categories.Find((number + i) % categoriesCount + 1));
                }

                number++;
            }

            context.SaveChanges();
        }

        public static void ProductsInRange(ProductShopContext context)
        {
            var products = context.Products
                .Where(p => p.BuyerId != null && p.Price >= 1000 && p.Price <= 2000)
                .OrderBy(p => p.Price)
                .ToList()
                .Select(p => new
                {
                    Name = p.Name,
                    Price = p.Price,
                    Buyer = p.Buyer.FirstName == null ? p.Buyer.LastName : p.Buyer.FirstName + " " + p.Buyer.LastName
                });

            XDocument xmlProducts = new XDocument();
            xmlProducts.Add(new XElement("products"));

            foreach (var product in products)
            {
                xmlProducts.Element("products").Add(
                    new XElement("product",
                        new XAttribute("name", product.Name),
                        new XAttribute("price", product.Price),
                        new XAttribute("buyer", product.Buyer)));
            }

            xmlProducts.Save("products-in-range.xml");
        }

        public static void SoldProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Count() >= 1)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.ProductsSold.Select(p => new
                    {
                        Name = p.Name,
                        Price = p.Price
                    })
                });

            XDocument xmlUsers = new XDocument();
            XElement rootElement = new XElement("users");

            foreach (var user in users)
            {
                XElement userElement = new XElement("user");
                userElement.SetAttributeValue("first-name", user.FirstName);
                userElement.SetAttributeValue("last-name", user.LastName);

                XElement soldProductsElement = new XElement("sold-products");

                foreach (var prod in user.SoldProducts)
                {
                    XElement productElement = new XElement("product");
                    productElement.SetElementValue("name", prod.Name);
                    productElement.SetElementValue("price", prod.Price);

                    soldProductsElement.Add(productElement);
                }

                userElement.Add(soldProductsElement);
                rootElement.Add(userElement);
            }

            xmlUsers.Add(rootElement);

            xmlUsers.Save("users-sold-products.xml");
        }

        public static void CategoriesByProductsCount(ProductShopContext context)
        {
            var categories = context.Categories
                .OrderBy(c => c.Products.Count())
                .Select(c => new
                {
                    Name = c.Name,
                    ProductsCount = c.Products.Count(),
                    AveragePrice = c.Products.Sum(p => p.Price) / c.Products.Count(),
                    TotalRevenue = c.Products.Sum(p => p.Price)
                })
                .ToList();

            XDocument xmlCategories = new XDocument();
            XElement rootElement = new XElement("categories");

            foreach (var cat in categories)
            {
                XElement categoryElement = new XElement("category");
                categoryElement.SetAttributeValue("name", cat.Name);

                XElement productsCountElement = new XElement("products-count", cat.ProductsCount);
                XElement averagePriceElement = new XElement("average-price", cat.AveragePrice);
                XElement totalRevenueElement = new XElement("total-revenue", cat.TotalRevenue);

                categoryElement.Add(productsCountElement);
                categoryElement.Add(averagePriceElement);
                categoryElement.Add(totalRevenueElement);

                rootElement.Add(categoryElement);
            }

            xmlCategories.Add(rootElement);
            xmlCategories.Save("categories-by-products.xml");
        }

        public static void UsersAndProducts(ProductShopContext context)
        {
            var users = context.Users
                .Where(u => u.ProductsSold.Count() >= 1)
                .OrderByDescending(u => u.ProductsSold.Count())
                .ThenBy(u => u.LastName)
                .Select(u => new
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Age = u.Age,
                    SoldProducts = u.ProductsSold.Select(p => new
                    {
                        Name = p.Name,
                        Price = p.Price
                    })
                })
                .ToList();

            XDocument xmlUsers = new XDocument();
            XElement rootElement = new XElement("users");
            rootElement.SetAttributeValue("count", context.Users.Count());

            foreach (var user in users)
            {
                XElement userElement = new XElement("user");
                userElement.SetAttributeValue("first-name", user.FirstName);
                userElement.SetAttributeValue("last-name", user.LastName);
                userElement.SetAttributeValue("age", user.Age);

                XElement soldProductsElement = new XElement("sold-products");
                soldProductsElement.SetAttributeValue("count", user.SoldProducts.Count());

                foreach (var prod in user.SoldProducts)
                {
                    XElement productElement = new XElement("product");
                    productElement.SetAttributeValue("name", prod.Name);
                    productElement.SetAttributeValue("price", prod.Price);

                    soldProductsElement.Add(productElement);
                }

                userElement.Add(soldProductsElement);
                rootElement.Add(userElement);
            }

            xmlUsers.Add(rootElement);
            xmlUsers.Save("users-and-products.xml");
        }
    }
}
