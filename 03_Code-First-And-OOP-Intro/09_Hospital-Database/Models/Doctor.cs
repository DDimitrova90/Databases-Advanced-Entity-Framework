namespace _09_Hospital_Database.Models
{
    using System.Collections.Generic;

    public class Doctor
    {
        public Doctor()
        {
            this.Visitations = new HashSet<Visitation>();
        }

        public int DoctorId { get; set; }

        public string Name { get; set; }

        public string Stecialty { get; set; }

        public virtual ICollection<Visitation> Visitations { get; set; }
    }
}
