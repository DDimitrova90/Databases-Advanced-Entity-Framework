namespace _04_Football_Betting_Database.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        public Country()
        {
            this.Towns = new HashSet<Town>();
            this.Continents = new HashSet<Continent>();
        }

        [Key]
        [StringLength(3)]
        public string Code { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; }

        public virtual ICollection<Continent> Continents { get; set; }
    }
}
