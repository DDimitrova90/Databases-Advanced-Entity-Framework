namespace BookShopEx
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

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public EditionType EditionType { get; set; }

        public decimal Price { get; set; }

        public int Copies { get; set; }

        public DateTime ReleaseDate { get; set; }

        public AgeRestriction AgeRestriction { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Book> RelatedBooks { get; set; }
    }
}
