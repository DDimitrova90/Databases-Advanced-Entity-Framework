namespace _02_Photographers.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Picture
    {
        public Picture()
        {
            this.Albums = new HashSet<Album>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Caption { get; set; }

        [Required]
        public string FilePath { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
