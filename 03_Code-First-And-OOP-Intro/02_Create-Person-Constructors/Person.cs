namespace _02_Create_Person_Constructors
{
    public class Person
    {
        public Person()
        {
            this.Name = "No name";
            this.Age = 1;
        }

        public Person(int age)
        {
            this.Name = "No name";
            this.Age = age;
        }

        public Person(string name)
        {
            this.Name = name;
            this.Age = 1;
        }

        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        // OR
        //public Person() : this("No name", 1) {}

        //public Person(int age) : this("No name", age) {}

        //public Person(string name) : this(name, 1) {}

        //public Person(string name, int age) 
        //{
        //    this.Name = name;
        //    this.Age = age;
        //}

        public string Name { get; set; }
        public int Age { get; set; }
    }
}
