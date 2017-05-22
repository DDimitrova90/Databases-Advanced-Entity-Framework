namespace _01_Code_First_Student_System
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            StudentSystemContext context = new StudentSystemContext();

            //context.Database.Initialize(true);

            // Problem 3.1
            //ListAllStudentsAndHomeworks(context);

            // Problem 3.2
            //ListAllCoursesAndResources(context);

            // Problem 3.3
            //ListCoursesWithMoreThanFiveResources(context);

            // Problem 3.4
            //ListActiveCourses(context);

            // Problem 3.5
            //PrintStudentsInfo(context); 
        }

        public static void ListAllStudentsAndHomeworks(StudentSystemContext context)
        {
            List<Student> students = context.Students.ToList();
            List<Homework> homeworks = context.Homeworks.ToList();

            foreach (Student stud in students)
            {
                Console.WriteLine($"Student: {stud.Name}");

                foreach (Homework hw in homeworks)
                {
                    if (hw.Student.Id == stud.Id)
                    {
                        Console.WriteLine($"Content: {hw.Content}, Content-Type: {hw.ContentType}");
                    }
                }
            }
        }

        public static void ListAllCoursesAndResources(StudentSystemContext context)
        {
            List<Course> courses = context.Courses
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .ToList();
            List<Resource> resources = context.Resources.ToList();

            foreach (Course cr in courses)
            {
                if (cr.Description == null)
                {
                    Console.WriteLine($"Course name: {cr.Name}, Description: No description!");
                }
                else
                {
                    Console.WriteLine($"Course name: {cr.Name}, Description: {cr.Description}");
                }

                foreach (Resource res in resources)
                {
                    if (res.Course.Id == cr.Id)
                    {
                        Console.WriteLine($"Resource name: {res.Name}, Type: {res.Type}, URL: {res.URL}");
                    }
                }
            }
        }

        public static void ListCoursesWithMoreThanFiveResources(StudentSystemContext context)
        {
            List<Course> courses = context.Courses.Where(c => c.Resources.Count() > 5).OrderByDescending(c => c.Resources.Count).ThenByDescending(c => c.StartDate).ToList();

            foreach (Course cr in courses)
            {
                Console.WriteLine($"Course name: {cr.Name} | Resources: {cr.Resources.Count}");
            }
        }

        public static void ListActiveCourses(StudentSystemContext context)
        {
            Console.Write("Please, enter date: ");
            string input = Console.ReadLine();
            DateTime date = DateTime.ParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            List<Course> courses = context.Courses
                .Where(c => c.StartDate <= date && c.EndDate >= date)
                .ToList()
                .OrderByDescending(c => c.Students.Count)
                .ThenByDescending(c => (c.EndDate - c.StartDate).Days)
                .ToList();

            foreach (Course cr in courses)
            {
                Console.WriteLine($"Course name: {cr.Name}");
                Console.WriteLine($"Start Date: {cr.StartDate} | End Date: {cr.EndDate}");
                Console.WriteLine($"Duration: {(cr.EndDate - cr.StartDate).Days} days | Students: {cr.Students.Count}");
            }
        }

        public static void PrintStudentsInfo(StudentSystemContext context)
        {
            List<Student> students = context.Students
                .ToList()
                .OrderByDescending(s => s.Courses.Select(c => c.Price).Sum())
                .ThenByDescending(s => s.Courses.Count)
                .ThenByDescending(s => s.Name)
                .ToList();

            foreach (Student st in students)
            {
                int coursesCount = st.Courses.Count;
                decimal totalPrice = st.Courses.Select(c => c.Price).Sum();
                decimal averagePrice = totalPrice / coursesCount;

                Console.WriteLine($"Student name: {st.Name} Courses: {coursesCount} Total price: {totalPrice:F2} Average price: {averagePrice:F2}");
            }
        }
    }
}
