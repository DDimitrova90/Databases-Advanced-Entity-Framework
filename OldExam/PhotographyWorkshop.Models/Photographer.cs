namespace PhotographyWorkshop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Photographer
    {
        public Photographer()
        {
            this.Lenses = new HashSet<Len>();
            this.Accessories = new HashSet<Accessory>();
            this.Workshops = new HashSet<Workshop>();
            this.WorkshopsTrainer = new HashSet<Workshop>();
        }
  
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [RegularExpression(@"\+\d{1,3}\/\d{8,10}")]
        public string Phone { get; set; }

        public int? PrimaryCameraDSLRId { get; set; }

        public virtual DSLRCamera PrimaryCameraDSLR { get; set; }

        public int? PrimaryCameraMirrorlessId { get; set; }

        public virtual MirrorlessCamera PrimaryCameraMirrorless { get; set; }

        public int? SecondaryCameraDSLRId { get; set; }

        public virtual DSLRCamera SecondaryCameraDSLR { get; set; }

        public int? SecondaryCameraMirrorlessId { get; set; }

        public virtual MirrorlessCamera SecondaryCameraMirrorless { get; set; }

        public virtual ICollection<Len> Lenses { get; set; }

        public virtual ICollection<Accessory> Accessories { get; set; }

        public virtual ICollection<Workshop> Workshops { get; set; }

        public virtual ICollection<Workshop> WorkshopsTrainer { get; set; }
    }
}
