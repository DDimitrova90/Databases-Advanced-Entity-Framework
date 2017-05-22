namespace _02_Photographers
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Photographer
    {
        public Photographer()
        {
            this.Albums = new HashSet<PhotographerAlbum>();  // here too
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime RegisterDate { get; set; }

        public DateTime? BirthDate { get; set; }

        // changed from ICollection<Album> Problem 10
        public virtual ICollection<PhotographerAlbum> Albums { get; set; }  
    }
}
