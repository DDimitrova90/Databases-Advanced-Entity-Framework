namespace _09_Hospital_Database.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Visitation
    {
        public Visitation()
        {
            this.Patients = new HashSet<Patient>();
            this.Diagnoses = new HashSet<Diagnose>();
        }

        [Key]
        public int VisitationId { get; set; }

        public DateTime Date { get; set; }

        public string Comments { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }

        public virtual ICollection<Diagnose> Diagnoses { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
