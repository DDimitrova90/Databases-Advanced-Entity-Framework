namespace EmployeesApp.Client
{
    using AutoMapper;
    using Models;
    using Models.Dtos;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            List<Employee> managers = CreateManagers();

            ConfigureAutoMapping();

            List<ManagerDto> managerDtos = Mapper.Map<List<Employee>, List<ManagerDto>>(managers);

            foreach (ManagerDto manager in managerDtos)
            {
                Console.WriteLine(manager.ToString()); 
            }

            // Problem 1. Simple Mapping
            //Employee employee = new Employee()
            //{
            //    FirstName = "Bratko",
            //    LastName = "Tanev",
            //    Address = "Neide si",
            //    Birthday = new DateTime(1991, 10, 22),
            //    Salary = 1026.50M
            //};

            //EmployeeDto empDto = Mapper.Map<EmployeeDto>(employee);

            //Console.WriteLine($"{empDto.FirstName} {empDto.LastName} - ${empDto.Salary:F2}");
        }

        public static void ConfigureAutoMapping()
        {
            // Problem 1. Simple Mapping
            //Mapper.Initialize(cfg => cfg.CreateMap<Employee, EmployeeDto>());

            // Problem 2.Advanced Mapping
            Mapper.Initialize(cfg => 
            {
                cfg.CreateMap<Employee, EmployeeDto>();
                cfg.CreateMap<Employee, ManagerDto>()
                .ForMember(dto => dto.SubordinatesCount,
                           opt => opt.MapFrom(src => src.Subordinates.Count()));
            });
        }

        public static List<Employee> CreateManagers()
        {
            List<Employee> managers = new List<Employee>();

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

            emp3.Manager = emp1;
            emp5.Manager = emp1;
            emp2.Manager = emp1;
            emp4.Manager = emp2;
            emp1.Subordinates.Add(emp2);
            emp1.Subordinates.Add(emp3);
            emp1.Subordinates.Add(emp5);
            emp2.Subordinates.Add(emp4);

            managers.Add(emp1);
            managers.Add(emp2);

            return managers;
        }
    }
}
