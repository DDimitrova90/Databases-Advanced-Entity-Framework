namespace _09_Hospital_Database.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Medicament
    {
        public Medicament()
        {
            this.Diagnoses = new HashSet<Diagnose>();
            this.Patients = new HashSet<Patient>();
        }

        [Key]
        public int MedicamentId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Diagnose> Diagnoses { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}
