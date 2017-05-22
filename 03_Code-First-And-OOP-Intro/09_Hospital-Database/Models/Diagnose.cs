namespace _09_Hospital_Database.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Diagnose
    {
        public Diagnose()
        {
            this.Patients = new HashSet<Patient>();
            this.Medicaments = new HashSet<Medicament>();
            this.Visitations = new HashSet<Visitation>();
        }

        [Key]
        public int DiagnoseId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Comments { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }

        public virtual ICollection<Medicament> Medicaments { get; set; }

        public virtual ICollection<Visitation> Visitations { get; set; }
    }
}
