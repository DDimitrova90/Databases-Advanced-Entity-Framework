namespace _01_Code_First_Student_System
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SeedData : CreateDatabaseIfNotExists<StudentSystemContext>
    {
        protected override void Seed(StudentSystemContext context)
        {
            Course course1 = new Course()
            {
                Name = "Mathematics",
                Description = "For begginers",
                StartDate = new DateTime(2017, 05, 16),
                EndDate = new DateTime(2017, 06, 16),
                Price = 210M
            };

            Course course2 = new Course()
            {
                Name = "Biology",
                StartDate = new DateTime(2017, 06, 23),
                EndDate = new DateTime(2017, 07, 23),
                Price = 360M
            };

            Course course3 = new Course()
            {
                Name = "History",
                Description = "Not for begginers",
                StartDate = new DateTime(2016, 10, 03),
                EndDate = new DateTime(2016, 12, 03),
                Price = 310M
            };

            Course course4 = new Course()
            {
                Name = "Geography",
                StartDate = new DateTime(2016, 01, 15),
                EndDate = new DateTime(2016, 03, 15),
                Price = 420M
            };

            context.Courses.AddRange(new[] { course1, course2, course3, course4 });

            Resource resource1 = new Resource()
            {
                Name = "Presentation one",
                Type = "presentation",   //or Type = ResourceType.presentation with enum
                URL = "www.eee.w.ww.w.",
                Course = course1
            };

            Resource resource2 = new Resource()
            {
                Name = "Video1",
                Type = "video",   //or Type = ResourceType.video with enum
                URL = "www.pppp.fff",
                Course = course3
            };

            Resource resource3 = new Resource()
            {
                Name = "Other",
                Type = "other",    //or Type = ResourceType.other with enum
                URL = "www.arrrrr",
                Course = course2
            };

            context.Resources.AddRange(new[] { resource1, resource2, resource3 });

            Student student1 = new Student()
            {
                Name = "Gosho",
                PhoneNumber = "0864795246",
                RegistrationDate = new DateTime(2016, 03, 08),
                Courses = new List<Course>() { course1, course4 }
            };

            Student student2 = new Student()
            {
                Name = "Pesho",
                PhoneNumber = "098422147531",
                RegistrationDate = new DateTime(2015, 09, 11),
                Courses = new List<Course>() { course2, course3, course4 }
            };

            Student student3 = new Student()
            {
                Name = "Tosho",
                PhoneNumber = "0888852148888",
                RegistrationDate = new DateTime(2014, 08, 13),
                Courses = new List<Course>() { course1, course2 }
            };

            context.Students.AddRange(new[] { student1, student2, student3 });

            Homework homework1 = new Homework()
            {
                Content = "dr-br",
                ContentType = "application",   //or ContentType = HomeworkType.application with enum
                SubmissionDate = new DateTime(2017, 07, 14),
                Student = student2
            };

            Homework homework2 = new Homework()
            {
                Content = "grrrr",
                ContentType = "pdf",    //or ContentType = HomeworkType.pdf with enum
                SubmissionDate = new DateTime(2016, 12, 05),
                Student = student3
            };

            Homework homework3 = new Homework()
            {
                Content = "fiuuuuu",
                ContentType = "zip",    //or ContentType = HomeworkType.zip with enum
                SubmissionDate = new DateTime(2016, 02, 10),
                Student = student1
            };

            context.Homeworks.AddRange(new[] { homework1, homework2, homework3 });

            context.SaveChanges();
            base.Seed(context);
        }
    }
}

