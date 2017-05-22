namespace _03_Oldest_Family_Member
{
    using System;

    public class OldestFamilyMember
    {
        public static void Main()
        {
            int n = int.Parse(Console.ReadLine());

            Family family = new Family();

            for (int i = 0; i < n; i++)
            {
                string[] personArgs = Console.ReadLine().Split();

                Person person = new Person();
                person.Name = personArgs[0];
                person.Age = int.Parse(personArgs[1]);

                family.AddMember(person);
            }

            Person oldestMember = family.GetOldestMember();

            Console.WriteLine($"{oldestMember.Name} {oldestMember.Age}");
        }
    }
}
