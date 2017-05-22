namespace _09_Hospital_Database.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Patient
    {
        public Patient()
        {
            this.Medicaments = new HashSet<Medicament>();
        }

        [Key]
        public int PatientId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [RegularExpression(@"([A-Za-z0-9][A-Za-z0-9.-_]+[A-Za-z0-9])@([A-Za-z]+.)([A-Za-z]+.)*")]
        public string Email { get; set; }

        [Required]
        public string BirthDate { get; set; }

        public byte Picture { get; set; }

        public bool HasInsurance { get; set; }

        public virtual Visitation Visitation { get; set; }

        public virtual Diagnose Diagnose { get; set; }

        public virtual ICollection<Medicament> Medicaments { get; set; }
    }
}
