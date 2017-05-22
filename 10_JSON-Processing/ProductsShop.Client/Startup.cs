namespace ProductsShop.Client
{
    using Data;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            ProductsShopContext context = new ProductsShopContext();
            //context.Database.Initialize(true);

            //ImportUsers(context);

            //ImportProducts(context);

            //ImportCategories(context);

            // Problem 3, Query 1
            //ProductsInRange(context);

            // Problem 3, Query 2
            //SuccessfullySoldProducts(context);

            // Problem 3, Query 3
            //CategoriesByProductsCount(context);

            // Problem 3, Query 4
            //UsersAndProducts(context);
        }

        public static void ImportUsers(ProductsShopContext context)
        {
            string usersJson = File.ReadAllText(@"../../Import/users.json");

            List<User> users = JsonConvert.DeserializeObject<List<User>>(usersJson);

            context.Users.AddRange(users);

            context.SaveChanges();
        }

        public static void ImportProducts(ProductsShopContext context)
        {
            string productsJson = File.ReadAllText(@"../../Import/products.json");

            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(productsJson);

            int number = 0;
            int usersCount = context.Users.Count();

            foreach (Product p in products)
            {
                p.SellerId = (number % usersCount) + 1;

                if (number % 3 != 0)
                {
                    p.BuyerId = (number * 2 % usersCount) + 1;
                }

                number++;
            }

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        public static void ImportCategories(ProductsShopContext context)
        {
            string categoriesJson = File.ReadAllText(@"../../Import/categories.json");

            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(categoriesJson);

            int number = 0;
            int productsCount = context.Products.Count();

            foreach (Category cat in categories)
            {
                int categoryProductsCount = number % 3;

                for (int i = 0; i < categoryProductsCount; i++)
                {
                    cat.Products.Add(context.Products.Find(number % productsCount + 1));
                }

                number++;
            }

            context.Categories.AddRange(categories);
            context.SaveChanges();
        }

        public static void ProductsInRange(ProductsShopContext context)
        {
            var products = context.Products
                .Where(p => p.Price >= 500 && p.Price <= 1000)
                .OrderBy(p => p.Price)
                .Select(p => new
                {
                    Name = p.Name,
                    Price = p.Price,
                    Seller = p.Seller.FirstName == null ? p.Seller.LastName : p.Seller.FirstName + " " + p.Seller.LastName
                })
                .ToList();

            var jsonProducts = JsonConvert.SerializeObject(products, Formatting.Indented);

            Console.WriteLine(jsonProducts);
        }

        public static void SuccessfullySoldProducts(ProductsShopContext context)
        {
            var users = context.Users
                .Where(u => u.SoldProducts.Count() >= 1)
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .Select(u => new
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SoldProducts = u.SoldProducts
                    .Where(p => p.Buyer != null)
                    .Select(p => new
                    {
                        Name = p.Name,
                        Price = p.Price,
                        BuyerFirstName = p.Buyer.FirstName,
                        BuyerLastName = p.Buyer.LastName
                    })
                })
                .ToList();

            var jsonUsers = JsonConvert.SerializeObject(users, Formatting.Indented);

            Console.WriteLine(jsonUsers);
        }

        public static void CategoriesByProductsCount(ProductsShopContext context)
        {
            var categories = context.Categories
                .OrderBy(c => c.Name)
                .ToList()
                .Select(c => new
                {
                    Category = c.Name,
                    ProductsCount = c.Products.Count(),  //or c.Products.Select(p => p.Id).DefaultIfEmpty(0).Count()
                    AveragePrice = c.Products.Count() == 0 ? 0 : c.Products.Sum(p => p.Price) / c.Products.Count(),
                    TotalRevenue = c.Products.Sum(p => p.Price)
                })
                .ToList();

            var jsonCategories = JsonConvert.SerializeObject(categories, Formatting.Indented);

            Console.WriteLine(jsonCategories);
        }

        public static void UsersAndProducts(ProductsShopContext context)
        {
            var users = context.Users
                .Where(u => u.SoldProducts.Count() >= 1)
                .OrderByDescending(u => u.SoldProducts.Count())
                .ThenBy(u => u.LastName)
                .ToList()
                .Select(u => new
                {
                    UsersCount = context.Users.Where(us => us.SoldProducts.Count() >= 1).Count(),
                    Users = new[]
                    {
                        new
                        {
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                            Age = u.Age,
                            SoldProducts = new
                            {
                                Count = u.SoldProducts.Count(),
                                Products = u.SoldProducts.Select(p => new
                                {
                                    Name = p.Name,
                                    Price = p.Price
                                })
                            }
                        }
                    }
                })
                .ToList();

            var jsonUsers = JsonConvert.SerializeObject(users, Formatting.Indented);

            // or var jsonUsers = JsonConvert.SerializeObject(new { UsersCount = users.Count, Users = users}, Formatting.Indented);

            // we can save it to file
            // File.WriteAllText("../../users-groups.json", json);

            Console.WriteLine(jsonUsers);
        }
    }
}
