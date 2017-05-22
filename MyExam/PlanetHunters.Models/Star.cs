namespace PlanetHunters.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Star
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [Range(2400, int.MaxValue)]
        public int Temperature { get; set; }

        [Required]
        public int HostStarSystemId { get; set; }

        public virtual StarSystem HostStarSystem { get; set; }

        public int? DiscoveryId { get; set; }

        public virtual Discovery Discovery { get; set; }
    }
}
