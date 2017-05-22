namespace _01_Code_First_Student_System
{
    using Migrations;
    using Models;
    using System.Data.Entity;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
            : base("name=StudentSystemContext")
        {
            Database.SetInitializer(new SeedData());  // Problem 2 - Seed Some Data in the Database
        }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Resource> Resources { get; set; }

        public virtual DbSet<Homework> Homeworks { get; set; }

        public virtual DbSet<License> Licenses { get; set; }
    }
}