namespace _04_Football_Betting_Database.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Continent
    {
        public Continent()
        {
            this.Countries = new HashSet<Country>();          
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
    }
}
