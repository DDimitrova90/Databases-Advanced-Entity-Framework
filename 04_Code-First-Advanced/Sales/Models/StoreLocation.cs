namespace Sales.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class StoreLocation
    {
        public StoreLocation()
        {
            this.SalesInStore = new HashSet<Sale>();
        }

        public int Id { get; set; }

        public string LocationName { get; set; }

        public virtual ICollection<Sale> SalesInStore { get; set; }
    }
}
