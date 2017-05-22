namespace PlanetHunters.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Telescope
    {
        public Telescope()
        {
            this.Discoveries = new HashSet<Discovery>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Location { get; set; }

        public double? MirrorDiameter { get; set; }

        public virtual ICollection<Discovery> Discoveries { get; set; }
    }
}
