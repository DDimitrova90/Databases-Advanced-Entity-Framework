namespace Projection
{
    using Models;
    using System;
    using System.Data.Entity;

    public class SeedData : DropCreateDatabaseAlways<EmployeesContext>
    {
        protected override void Seed(EmployeesContext context)
        {
            Employee emp1 = new Employee()
            {
                FirstName = "Bratko",
                LastName = "Tanev",
                Address = "Neide si",
                Birthday = new DateTime(1991, 10, 22),
                Salary = 1026.50M,
                IsOnHoliday = false
            };

            Employee emp2 = new Employee()
            {
                FirstName = "Kolio",
                LastName = "Koliov",
                Address = "Tuk i tam",
                Birthday = new DateTime(1986, 05, 12),
                Salary = 530.26M,
                IsOnHoliday = true
            };

            Employee emp3 = new Employee()
            {
                FirstName = "Zdravko",
                LastName = "Zdravkov",
                Address = "Sofia",
                Birthday = new DateTime(1988, 08, 01),
                Salary = 800.26M,
                IsOnHoliday = true
            };

            Employee emp4 = new Employee()
            {
                FirstName = "Tashko",
                LastName = "Tashev",
                Address = "Na ulicata",
                Birthday = new DateTime(1989, 10, 05),
                Salary = 725.10M,
                IsOnHoliday = false
            };

            Employee emp5 = new Employee()
            {
                FirstName = "Gancho",
                LastName = "Ganev",
                Address = "Mladost",
                Birthday = new DateTime(1990, 01, 02),
                Salary = 630.89M,
                IsOnHoliday = false
            };

            context.Employees.AddRange(new [] { emp1, emp2, emp3, emp4, emp5 });

            context.SaveChanges();

            emp3.Manager = emp1;
            emp5.Manager = emp1;
            emp2.Manager = emp1;
            //emp4.Manager = emp2;
            emp1.Subordinates.Add(emp2);
            emp1.Subordinates.Add(emp3);
            emp1.Subordinates.Add(emp5);
            //emp2.Subordinates.Add(emp4);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
