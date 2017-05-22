namespace Sales.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Customer
    {
        public Customer()
        {
            this.SalesForCustomer = new HashSet<Sale>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public string CreditCardNumber { get; set; }

        public virtual ICollection<Sale> SalesForCustomer { get; set; }
    }
}
