namespace PlanetHunters.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Publication
    {
        public int Id { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public int DiscoveryId { get; set; }

        public virtual Discovery Discovery { get; set; }

        public int JournalId { get; set; }

        public virtual Journal Journal { get; set; }
    }
}
