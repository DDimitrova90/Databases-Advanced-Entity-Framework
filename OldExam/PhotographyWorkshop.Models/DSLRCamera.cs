namespace PhotographyWorkshop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DSLRCamera
    {
        public DSLRCamera()
        {
            this.PrimaryForPhotographers = new HashSet<Photographer>();
            this.SecondaryForPhotographers = new HashSet<Photographer>();
        }

        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public bool IsFoolFrame { get; set; }

        [Required]
        [Range(100, int.MaxValue)]
        public int MinISO { get; set; }

        public int? MaxISO { get; set; }

        public int? MaxShutterSpeed { get; set; }

        public virtual ICollection<Photographer> PrimaryForPhotographers { get; set; }

        public virtual ICollection<Photographer> SecondaryForPhotographers { get; set; }
    }
}
