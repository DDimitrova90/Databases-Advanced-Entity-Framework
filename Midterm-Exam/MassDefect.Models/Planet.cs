namespace MassDefect.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Planet
    {
        public Planet()
        {
            this.Inhabitants = new HashSet<Person>();
            this.OriginAnomalies = new HashSet<Anomaly>();
            this.TeleportAnomalies = new HashSet<Anomaly>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int SunId { get; set; }

        public virtual Star Sun { get; set; }

        public int SolarSystemId { get; set; }

        public virtual SolarSystem SolarSystem { get; set; }

        public virtual ICollection<Person> Inhabitants { get; set; }

        [InverseProperty("OriginPlanet")]
        public virtual ICollection<Anomaly> OriginAnomalies { get; set; }

        [InverseProperty("TeleportPlanet")]
        public virtual ICollection<Anomaly> TeleportAnomalies { get; set; }
    }
}
