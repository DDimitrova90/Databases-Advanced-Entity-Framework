namespace Sales.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Product
    {
        public Product()
        {
            this.SalesOfProduct = new HashSet<Sale>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public double Quantity { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }   // Problem 4

        public virtual ICollection<Sale> SalesOfProduct { get; set; }
    }
}
