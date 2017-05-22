namespace _02_Create_Person_Constructors
{
    using System;

    public class CreatePersonConstructors
    {
        public static void Main()
        {
            string[] input = Console.ReadLine().Split(',');

            switch (input.Length)
            {
                case 1:
                    if (string.IsNullOrEmpty(input[0]))
                    {
                        Person p1 = new Person();
                        Console.WriteLine($"{p1.Name} {p1.Age}");
                        return;
                    }
                    else
                    {
                        int p2Age;
                        bool isNumber = int.TryParse(input[0], out p2Age);
                        Person p2;

                        if (isNumber)
                        {
                            p2 = new Person(p2Age);

                        }
                        else
                        {
                            p2 = new Person(input[0]);
                        }

                        Console.WriteLine($"{p2.Name} {p2.Age}");        
                    }
                    break;
                case 2:
                    string name = input[0];
                    int p3Age = int.Parse(input[1]);
                    Person p3 = new Person(name, p3Age);
                    Console.WriteLine($"{p3.Name} {p3.Age}");
                    break;                   
            }
        }
    }
}
