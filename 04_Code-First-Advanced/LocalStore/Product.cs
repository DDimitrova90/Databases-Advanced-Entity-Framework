namespace LocalStore
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DistributorName { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        // 2.Local Store Improvement

        public float Weight { get; set; }

        public float Quantity { get; set; }
    }
}
