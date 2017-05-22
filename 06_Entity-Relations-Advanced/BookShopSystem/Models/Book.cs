namespace BookShopSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public enum EditionType
    {
        Normal = 0,
        Promo = 1,
        Gold = 2
    }

    public enum AgeRestriction
    {
        Minor = 0,
        Teen = 1,
        Adult = 2
    }

    public class Book
    {
        public Book()
        {
            this.Categories = new HashSet<Category>();
            this.RelatedBooks = new HashSet<Book>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public EditionType EditionType { get; set; } 

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Copies { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Required]
        public AgeRestriction AgeRestriction { get; set; }  

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Book> RelatedBooks { get; set; }
    }
}
