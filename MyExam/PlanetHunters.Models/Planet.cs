namespace PlanetHunters.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Planet
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public double Mass { get; set; }

        [Required]
        public int HostStarSystemId { get; set; }

        public virtual StarSystem HostStarSystem { get; set; }

        public int? DiscoveryId { get; set; }

        public virtual Discovery Discovery { get; set; }
    }
}
