namespace Projection
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            ConfigureAutoMapping();

            EmployeesContext context = new EmployeesContext();
            context.Database.Initialize(true);

            List<EmployeeDto> employees = context.Employees
                .Where(e => e.Birthday.Value.Year < 1990)
                .OrderByDescending(e => e.Salary)
                .ProjectTo<EmployeeDto>()
                .ToList();

            foreach (EmployeeDto emp in employees)
            {
                Console.WriteLine(emp.ToString());
            }
        }

        public static void ConfigureAutoMapping()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Employee, EmployeeDto>()
                                         .ForMember(dto => dto.ManagerLastName,
                                         opt => opt.MapFrom(src => src.Manager.LastName)));
        }
    }
}
