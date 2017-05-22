namespace _01_Code_First_Student_System.Migrations
{
    using Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "_01_Code_First_Student_System.StudentSystemContext";
        }

        protected override void Seed(StudentSystemContext context)
        {          
        }
    }
}
