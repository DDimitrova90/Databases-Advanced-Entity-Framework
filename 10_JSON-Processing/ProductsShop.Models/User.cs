namespace ProductsShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            this.Friends = new HashSet<User>();
            this.BoughtProducts = new HashSet<Product>();
            this.SoldProducts = new HashSet<Product>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public virtual ICollection<Product> SoldProducts { get; set; }

        public virtual ICollection<Product> BoughtProducts { get; set; }

        public virtual ICollection<User> Friends { get; set; }
    }
}
