namespace BookShopSystem
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class BookShopMain
    {
        public static void Main()
        {
            BookShopContext context = new BookShopContext();

            context.Database.Initialize(true);

            List<Book> books = context.Books.Take(3).ToList();

            books[0].RelatedBooks.Add(books[1]);
            books[1].RelatedBooks.Add(books[0]);
            books[0].RelatedBooks.Add(books[2]);
            books[2].RelatedBooks.Add(books[0]);

            context.SaveChanges();

            List<Book> booksFromQuery = context.Books.Take(3).ToList();

            foreach (Book book in booksFromQuery)
            {
                Console.WriteLine($"--{book.Title}");

                foreach (Book relatedBook in book.RelatedBooks)
                {
                    Console.WriteLine(relatedBook.Title);
                }
            }
        }
    }
}
