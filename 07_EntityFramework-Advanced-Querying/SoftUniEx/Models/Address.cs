namespace SoftUniEx
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Address
    {
        public Address()
        {
            Employees = new HashSet<Employee>();
        }

        public int AddressID { get; set; }

        [Required]
        [StringLength(100)]
        public string AddressText { get; set; }

        public int? TownID { get; set; }

        public virtual Town Town { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
