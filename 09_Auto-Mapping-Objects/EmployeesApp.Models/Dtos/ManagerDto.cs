namespace EmployeesApp.Models.Dtos
{
    using System.Collections.Generic;
    using System.Text;

    public class ManagerDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<EmployeeDto> Subordinates { get; set; }

        public int SubordinatesCount { get; set; }

        public override string ToString()
        {
            StringBuilder strb = new StringBuilder();

            strb.AppendLine($"{this.FirstName} {this.LastName} | Employees: {this.SubordinatesCount}");

            foreach (EmployeeDto subordinate in this.Subordinates)
            {
                strb.AppendLine(subordinate.ToString());
            }

            return strb.ToString().Trim();
        }
    }
}
