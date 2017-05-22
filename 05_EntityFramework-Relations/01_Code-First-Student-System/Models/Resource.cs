namespace _01_Code_First_Student_System.Models
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    enum ResourceType
    {
        video,
        presentation,
        document,
        other
    }

    public class Resource
    {
        private string type;

        public Resource()
        {
            this.Licenses = new HashSet<License>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type    // with enum will be only: public Type Type { get; set; } 
        {                     // without private field
            get { return this.type; }
            set
            {
                if (value == "video" || value == "presentation" || value == "document" || value == "other")
                {
                    this.type = value;
                }
                else
                {
                    throw new ArgumentException("The type must be video, presentation, document or other!");
                }
            }
        }

        [Required]
        public string URL { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<License> Licenses { get; set; }
    }
}
