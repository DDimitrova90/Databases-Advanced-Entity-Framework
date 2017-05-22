namespace _02_Photographers.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Album
    {
        public Album()
        {
            this.Pictures = new HashSet<Picture>();
            this.Tags = new HashSet<Tag>();
            this.Photographers = new HashSet<PhotographerAlbum>();  // here too
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string BackgroundColor { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        public virtual ICollection<Picture> Pictures { get; set; }

        // changed from ICollection<Photographer> Problem 10
        public virtual ICollection<PhotographerAlbum> Photographers { get; set; }
         
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
