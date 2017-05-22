namespace EmployeesApp.Models
{
    using System;
    using System.Collections.Generic;

    public class Employee
    {
        public Employee()
        {
            this.Subordinates = new HashSet<Employee>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal? Salary { get; set; }

        public DateTime? Birthday { get; set; }

        public bool IsOnHoliday { get; set; }

        public string Address { get; set; }

        public int? ManagerId { get; set; }

        public virtual Employee Manager { get; set; }

        public virtual ICollection<Employee> Subordinates { get; set; }
    }
}
