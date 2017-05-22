namespace PlanetHunters.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Discovery
    {
        public Discovery()
        {
            this.Stars = new HashSet<Star>();
            this.Planets = new HashSet<Planet>();
            this.Pioneers = new HashSet<Astronomer>();
            this.Observers = new HashSet<Astronomer>();
            this.Publications = new HashSet<Publication>();
        }

        public int Id { get; set; }

        [Required]
        public DateTime DateMade { get; set; }

        [Required]
        public int TelescopeId { get; set; }

        public virtual Telescope Telescope { get; set; }

        public virtual ICollection<Publication> Publications { get; set; }

        public virtual ICollection<Star> Stars { get; set; }

        public virtual ICollection<Planet> Planets { get; set; }

        public virtual ICollection<Astronomer> Pioneers { get; set; }

        public virtual ICollection<Astronomer> Observers { get; set; }
    }
}
