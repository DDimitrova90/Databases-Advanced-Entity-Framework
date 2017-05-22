namespace PlanetHunters.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Astronomer
    {
        public Astronomer()
        {
            this.MadeDiscoveries = new HashSet<Discovery>();
            this.ObservedDiscoveries = new HashSet<Discovery>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public virtual ICollection<Discovery> MadeDiscoveries { get; set; }

        public virtual ICollection<Discovery> ObservedDiscoveries { get; set; }
    }
}
