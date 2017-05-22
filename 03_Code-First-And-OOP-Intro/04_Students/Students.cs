namespace _04_Students
{
    using System;

    public class Students
    {
        public static void Main()
        {
            string input = Console.ReadLine();

            while (input != "End")
            {
                Student student = new Student();
                student.Name = input;

                input = Console.ReadLine();
            }

            Console.WriteLine(Student.count);
        }
    }
}
