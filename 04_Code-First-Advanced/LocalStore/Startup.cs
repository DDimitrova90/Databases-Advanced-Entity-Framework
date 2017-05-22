namespace LocalStore
{
    public class Startup
    {
        public static void Main()
        {
            LocalStoreContext context = new LocalStoreContext();

            context.Database.Initialize(true);

            context.Products.Add(
                new Product
                {
                    Name = "Cheese",
                    DistributorName = "Bozentsi",
                    Description = "Not very solty",
                    Price = 15.86M
                });

            context.Products.Add(
                new Product
                {
                    Name = "Wine",
                    DistributorName = "Villa Yambol",
                    Description = "With very rich scent",
                    Price = 10.39M
                });

            context.Products.Add(
               new Product
               {
                   Name = "Bread",
                   DistributorName = "HomeBackery",
                   Description = "Very delicious home made bread",
                   Price = 2.15M
               });

            context.SaveChanges();
        }
    }
}
