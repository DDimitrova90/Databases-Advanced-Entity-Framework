namespace Sales.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Sale
    {
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public int ProductId { get; set; }

        public int CustomerId { get; set; }

        public int StoreLocationId { get; set; }

        public virtual Product Product { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual StoreLocation StoreLocation { get; set; }
    }
}
