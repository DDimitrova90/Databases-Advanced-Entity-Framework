namespace SoftUniEx
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main()
        {
            SoftUniContext context = new SoftUniContext();

            // Problem 17.Call a Stored Procedure

            //SqlParameter firstName = new SqlParameter("@firstName", SqlDbType.VarChar);
            //firstName.Value = "Ruth";
            //SqlParameter lastName = new SqlParameter("@lastName", SqlDbType.VarChar);
            //lastName.Value = "Ellerbrock";

            //If in PROC we select * can be: context.Database.SqlQuery<Project>(....);

            //var projects = context.Database.SqlQuery<GetProjectByEmployee>("usp_GetProjectsByEmployee @firstName, @lastName", firstName, lastName);

            //foreach (var project in projects)
            //{
            //    Console.WriteLine($"{project.Name} - {project.Description}, {project.StartDate}");
            //}

            // Probem 18.Employees Maximum Salaries
            //EmployeesMaximumSalaries(context);      
        }

        public static void EmployeesMaximumSalaries(SoftUniContext context)
        {
            var departments = context.Employees
                .GroupBy(e => e.Department.Name)
                .Select(e => new { DepatmentName = e.Key, MaxSalary = e.Max(d => d.Salary) })
                .Where(e => e.MaxSalary < 30000 && e.MaxSalary > 70000)
                .ToList();

            foreach (var dep in departments)
            {
                Console.WriteLine($"{dep.DepatmentName} - {dep.MaxSalary:F2}");
            }
        }
    }
}
