namespace IntroEntityExercise
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class IntroEntityExercise
    {
        public static void Main()
        {
            SoftUniContext context = new SoftUniContext();

            GringottsContext contextGr = new GringottsContext();

            //EmlpoyeesFullInfo(context);

            //EmployeesWithSalaryOver(context);

            //EmployeesFromSeattle(context);

            //AddNewAddressAndUpdateEmployee(context);

            //FindEmployeesInPeriod(context);

            //AddressesByTownName(context);

            //EmployeeWithId147(context);

            //DepartmentsWithMoreThan5Employees(context);

            //FindLatest10Projects(context);

            //IncreaseSalaries(context);

            //FindEmployeesByFirstNameStartingwithSA(context);

            //FirstLetter(contextGr);

            //DeleteProjectById(context);

            //RemoveTowns(context);

            //var projects = context.Projects;

            //Stopwatch timer = new Stopwatch();
            //timer.Start();
            //PrintNamesWithNativeQuery(context);
            //timer.Stop();
      
            //Console.WriteLine($"Native: {timer.Elapsed}");

            //timer.Restart();
            //PrintNamesWithLinq(context);
            //timer.Stop();

            //Console.WriteLine($"Linq: {timer.Elapsed}");
        }

        public static void EmlpoyeesFullInfo(SoftUniContext context)
        {
            List<Employee> employees = context.Employees.ToList();

            foreach (var emp in employees.OrderBy(x => x.EmployeeID))
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} {emp.MiddleName} {emp.JobTitle} {emp.Salary}");
            }
        }

        public static void EmployeesWithSalaryOver(SoftUniContext context)
        {
            var employeesNames = context.Employees.Where(e => e.Salary > 50000).Select(e => e.FirstName);

            foreach (var name in employeesNames)
            {
                Console.WriteLine($"{name}");
            }
        }

        public static void EmployeesFromSeattle(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName);

            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} from {emp.Department.Name} - ${emp.Salary:F2}");
            }
        }

        public static void AddNewAddressAndUpdateEmployee(SoftUniContext context)
        {
            Address address = new Address();
            address.AddressText = "Vitoshka 15";
            address.TownID = 4;
            
            context.Addresses.Add(address);  
            context.SaveChanges();

            // OR
            //Address address = new Address()
            //{
            //    AddressText = "Vitoshka 15",
            //    TownID = 4,
            //};
            //context.Addresses.Add(address);
            //context.SaveChanges();

            Employee employee = context.Employees
             .FirstOrDefault(e => e.LastName == "Nakov");
            employee.Address = address;
            context.SaveChanges();

            List<Employee> employees = context.Employees
                .OrderByDescending(e => e.AddressID)
                .Take(10)
                .ToList();

            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.Address.AddressText}");
            }
        }

        public static void FindEmployeesInPeriod(SoftUniContext context)
        {
            List<Employee> employees = context
                .Employees
                .Where(e => e.Projects.Count(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003) > 0)
                .Take(30)           
                .ToList();          // or Projects.Any(condition)

            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} {emp.Manager.FirstName}");

                foreach (var proj in emp.Projects)
                {
                    Console.WriteLine($"--{proj.Name} {proj.StartDate} {proj.EndDate}");
                }
            }
        }

        public static void AddressesByTownName(SoftUniContext context)
        {
            List<Address> addresses = context.Addresses
                .OrderByDescending(a => a.Employees.Count())
                .ThenBy(a => a.Town.Name)
                .Take(10)
                .ToList();

            foreach (var adr in addresses)
            {
                Console.WriteLine($"{adr.AddressText}, {adr.Town.Name} - {adr.Employees.Count()} employees");
            }
        }

        public static void EmployeeWithId147(SoftUniContext context)
        {
            Employee employee = context.Employees.FirstOrDefault(e => e.EmployeeID == 147);

            Console.WriteLine($"{employee.FirstName} {employee.LastName} {employee.JobTitle}");

            foreach (var proj in employee.Projects.OrderBy(p => p.Name))
            {
                Console.WriteLine($"{proj.Name}");
            }
        }

        public static void DepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            List<Department> departments = context.Departments
                .Where(d => d.Employees.Count() > 5)
                .OrderBy(d => d.Employees.Count())
                .ToList();
           
            foreach (var dep in departments)
            {
                Console.WriteLine($"{dep.Name} {dep.Manager.FirstName}");

                foreach (var emp in dep.Employees)
                {
                    Console.WriteLine($"{emp.FirstName} {emp.LastName} {emp.JobTitle}");
                }
            }
        }

        public static void FindLatest10Projects(SoftUniContext context)
        {
            List<Project> projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .ToList();

            foreach (var proj in projects.OrderBy(p => p.Name))
            {
                Console.WriteLine($"{proj.Name} {proj.Description} {proj.StartDate} {proj.EndDate}");
            }
        }

        public static void IncreaseSalaries(SoftUniContext context)
        {
            List<Employee> employees = context.Employees.Where(e => e.Department.Name == "Engineering" || e.Department.Name == "Tool Design" || e.Department.Name == "Marketing" || e.Department.Name == "Information Services").ToList();

            foreach (var emp in employees)
            {
                emp.Salary += 0.12M * emp.Salary;
                context.SaveChanges();

                Console.WriteLine($"{emp.FirstName} {emp.LastName} (${emp.Salary:F6})");
            }
        }

        public static void FindEmployeesByFirstNameStartingwithSA(SoftUniContext context)
        {
            List<Employee> employees = context.Employees
                .Where(e => e.FirstName.ToUpper().StartsWith("SA"))
                .ToList();

            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle} - (${emp.Salary:F4})");
            }
        }

        public static void FirstLetter(GringottsContext contextGr)
        {
            List<string> wizzardsNames = contextGr.WizzardDeposits
                .Where(wd => wd.DepositGroup == "Troll Chest")
                .Select(wd => wd.FirstName)
                .ToList();    

            var letters = wizzardsNames.Select(n => n[0]).Distinct().OrderBy(e => e);       

            foreach (var letter in letters)
            {
                Console.WriteLine(letter);
            }
        }

        public static void DeleteProjectById(SoftUniContext context)
        {
            Project project = context.Projects.Find(2);  

            foreach (Employee emp in project.Employees)
            {
                emp.Projects.Remove(project);
            }

            context.Projects.Remove(project);
            context.SaveChanges();

            List<string> projects = context.Projects.Select(p => p.Name).Take(10).ToList();

            foreach (string proj in projects)
            {
                Console.WriteLine(proj);
            }
        }

        public static void RemoveTowns(SoftUniContext context)
        {
            string currTown = Console.ReadLine();

            Town town = context.Towns.FirstOrDefault(t => t.Name == currTown);
            var addresses = context.Addresses.Where(a => a.Town.Name == currTown);
            var employees = context.Employees.Where(e => e.Address.Town.Name == currTown);
            int addressesCount = addresses.Count();

            foreach (var emp in employees)
            {
                emp.AddressID = null;
            }

            foreach (var adr in addresses)
            {
                context.Addresses.Remove(adr);
            }

            context.Towns.Remove(town);
            context.SaveChanges();

            Console.WriteLine($"{addressesCount} address in {currTown} was deleted");
        }

        public static void PrintNamesWithNativeQuery(SoftUniContext context)
        {
            var projects = context.Database
                .SqlQuery<Project>("SELECT * FROM Projects WHERE YEAR(StartDate) = 2002");

            foreach (var proj in projects)
            {
                foreach (var emp in proj.Employees)
                {
                    Console.WriteLine(emp.FirstName);
                }
            }
        }

        public static void PrintNamesWithLinq(SoftUniContext context)
        {
            var projects = context.Projects.Where(p => p.StartDate.Year == 2002);

            foreach (var proj in projects)
            {
                foreach (var emp in proj.Employees)
                {
                    Console.WriteLine(emp.FirstName);
                }
            }
        }
    }
}