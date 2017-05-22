namespace _01_Define_A_Class_Person
{
    public class DefineAClassPerson
    {
        public static void Main()
        {
            Person p1 = new Person();
            p1.Name = "Pesho";
            p1.Age = 20;

            Person p2 = new Person()
            {
                Name = "Gosho",
                Age = 18
            };

            Person p3 = new Person();
            p3.Name = "Stamat";
            p3.Age = 43;
        }
    }
}
