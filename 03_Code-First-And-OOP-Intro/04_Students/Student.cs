namespace _04_Students
{
    public class Student
    {
        private string name;  
        public static int count = 0;  

        public Student()
        {
            count++;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        // OR
        //public static int count;

        //public Student(string name)
        //{
        //    this.Name = name;
        //    count++;
        //}

        //public string Name { get; set; }
    }
}
