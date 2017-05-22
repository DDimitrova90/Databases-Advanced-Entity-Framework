namespace ManagementStudioLab
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main()
        {
            // Window initialization
            Console.WindowHeight = 17;
            Console.BufferHeight = 17;
            Console.WindowWidth = 50;
            Console.BufferWidth = 50;
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            // DB init
            var context = new SoftUniDBEntities();

            ListAll(context);
        }

        public static void ListAll(SoftUniDBEntities context)
        {
            Paginator projectsPaginator = new Paginator(
                context.Projects
                .Select(p => new
                {
                    p.ProjectID,
                    p.Name
                })
                .ToList()
                .Select(p => $"{p.ProjectID, 4} | {p.Name}")
                .ToList()
                , 2, 0, 14, true);

            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                Console.Clear();
                Console.WriteLine($" ID  | Project Name (Page {projectsPaginator.CurrentPage + 1} of {projectsPaginator.MaxPages})");
                Console.WriteLine("-----+-------------------------------");

                projectsPaginator.Print();
                var key = Console.ReadKey(true);

                if (!KeyboardController.PageController(key, projectsPaginator))
                {
                    return;
                }
            }
        }

        public static void ShowDetails(Project project)
        {
            // -------------------------------------------------

            Console.Clear();

            Console.WriteLine($"ID: {project.ProjectID,4} | {project.Name}");
            Utility.PrintHLine();
            Console.WriteLine(project.Description);
            Utility.PrintHLine();
            Console.WriteLine($"{project.StartDate,-24}| {project.EndDate}");
            Utility.PrintHLine();
            Console.WriteLine($"Page ");
            Utility.PrintHLine();

            int pageSize = 16 - Console.CursorTop;

            List<Employee> employees = project.Employees.ToList();
            int page = 0;
            int maxPages = (int)Math.Ceiling(employees.Count / (double)pageSize);
            int pointer = 1;

            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                Console.Clear();
                Console.WriteLine($"ID: {project.ProjectID,4} | {project.Name}");
                Utility.PrintHLine();
                Console.WriteLine(project.Description);
                Utility.PrintHLine();
                Console.WriteLine($"{project.StartDate,-24}| {project.EndDate}");
                Utility.PrintHLine();
                Console.WriteLine($"Page {page + 1} of {maxPages}");
                Console.WriteLine("-------------------------------------");

                int current = 1;

                foreach (var emp in employees.Skip(pageSize * page).Take(pageSize))
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;

                    if (current == pointer)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($"{emp.FirstName} {emp.LastName}");
                    current++;
                }

                if (page + 1 > maxPages)
                {
                    break;
                }

                var key = Console.ReadKey(true);

                switch (key.Key.ToString())
                {
                    /*
                    case "Enter":
                        Employee currentEmployee = employees.Skip((pageSize * page) + pointer - 1).First();
                        ShowDetails(currentEmployee);
                        break;
                    */
                    case "UpArrow":
                        if (pointer > 1)
                        {
                            pointer--;
                        }
                        else if (page > 0)
                        {
                            page--;
                            pointer = pageSize;
                        }
                        break;
                    case "DownArrow":
                        if (pointer < pageSize)
                        {
                            pointer++;
                        }
                        else if (page + 1 <= maxPages)
                        {
                            page++;
                            pointer = 1;
                        }
                        break;
                    case "Escape":
                        return;
                }
            }

            // ----------------------------------
        }       
    }
}
