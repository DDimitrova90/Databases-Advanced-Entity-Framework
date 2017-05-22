namespace _01_Code_First_Student_System.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    enum HomeworkType
    {
        application,
        pdf,
        zip
    }

    public class Homework
    {
        private string contentType;

        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string ContentType   // with enum will be only: public string ContentType { get; set; }
        {                           // without private field
            get { return this.contentType; }
            set
            {
                if (value == "application" || value == "pdf" || value == "zip")
                {
                    this.contentType = value;
                }
                else
                {
                    throw new ArgumentException("The Content Type must be application, pdf or zip!");
                }
            }
        }

        [Required]
        public DateTime SubmissionDate { get; set; }

        public virtual Student Student { get; set; }
    }
}
