namespace BookShopEx
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Linq;

    public class Startup
    {
        public static void Main()
        {
            BookShopContext context = new BookShopContext();

            // Problem 1. Books Titles by Age Restriction
            //BooksTitlesByAgeRestriction(context);

            // Problem 2. Golden Books
            //GoldenBooks(context);

            // Problem 3. Books by Price
            //BooksByPrice(context);

            // Problem 4. Not Released Books
            //NotReleasedBooks(context);

            // Problem 5. Book Titles by Category 
            //BookTitlesByCategory(context);

            // Problem 6. Books Released Before Date
            //BooksReleasedBeforeDate(context);

            // Problem 7. Authors Search
            //AuthorsSearch(context);

            // Problem 8. Books Search
            //BooksSearch(context);

            // Problem 9. Book Titles Search
            //BookTitlesSearch(context);

            // Problem 10. Count Books
            //CountBooks(context);

            // Problem 11. Total Book Copies
            //TotalBookCopies(context);

            // Problem 12. Find Profit
            //FindProfit(context);

            // Problem 13. Most Recent Books
            //MostRecentBooks(context);

            // Problem 14. Increase Book Copies
            //IncreaseBookCopies(context);

            // Problem 15. Remove Books
            //RemoveBooks(context);

            // Problem 16. Stored Procedure 
            //StoredProcedure(context);
        }

        public static void BooksTitlesByAgeRestriction(BookShopContext context)
        {
            string input = Console.ReadLine().ToLower();

            List<Book> books = context.Books
                .Where(b => b.AgeRestriction.ToString().ToLower() == input)
                .ToList();

            foreach (Book book in books)
            {
                Console.WriteLine(book.Title);
            }
        }

        public static void GoldenBooks(BookShopContext context)
        {
            List<Book> books = context.Books
                .Where(b => (b.EditionType.ToString() == "Gold") && (b.Copies < 5000))
                .OrderBy(b => b.Id)
                .ToList();

            foreach (Book book in books)
            {
                Console.WriteLine(book.Title);
            }
        }

        public static void BooksByPrice(BookShopContext context)
        {
            List<Book> books = context.Books
                .Where(b => b.Price < 5 || b.Price > 40)
                .OrderBy(b => b.Id)
                .ToList();

            foreach (Book book in books)
            {
                Console.WriteLine($"{book.Title} - ${book.Price:F2}");
            }
        }

        public static void NotReleasedBooks(BookShopContext context)
        {
            int year = int.Parse(Console.ReadLine());

            List<Book> books = context.Books
                .Where(b => b.ReleaseDate.Year != year)
                .OrderBy(b => b.Id)
                .ToList();

            foreach (Book book in books)
            {
                Console.WriteLine(book.Title);
            }
        }

        public static void BookTitlesByCategory(BookShopContext context)
        {
            string[] categories = Console.ReadLine()
                .ToLower()
                .Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            var categoryBooks = context.Categories
                .Where(c => categories.Contains(c.Name))
                .Select(c => new { c.Name, Books = c.Books.Select(b => b.Title) });

            foreach (var cat in categoryBooks)
            {
                if (categories.Contains(cat.Name.ToLower()))
                {
                    foreach (var book in cat.Books.ToList())
                    {
                        Console.WriteLine(book);
                    }
                }
            }

            // solution from video

            foreach (Book book in context.Books)
            {
                if (book.Categories.Any(c => categories.Contains(c.Name.ToLower())))
                {
                    Console.WriteLine(book.Title);
                }
            }
        }

        public static void BooksReleasedBeforeDate(BookShopContext context)
        {
            string input = Console.ReadLine();
            DateTime date = DateTime.ParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture);

            List<Book> books = context.Books.Where(b => b.ReleaseDate < date).ToList();

            foreach (Book book in books)
            {
                Console.WriteLine($"{book.Title} - {book.EditionType.ToString()} - {book.Price:F2}");
            }
        }

        public static void AuthorsSearch(BookShopContext context)
        {
            string checkStr = Console.ReadLine();

            List<Author> authors = context.Authors.ToList();

            foreach (Author atr in authors)
            {
                if (atr.FirstName.EndsWith(checkStr))
                {
                    Console.WriteLine($"{atr.FirstName} {atr.LastName}");
                }
            }
        }

        public static void BooksSearch(BookShopContext context)
        {
            string checkStr = Console.ReadLine().ToLower();

            List<Book> books = context.Books
                .Where(b => b.Title.ToLower().Contains(checkStr))
                .ToList();

            foreach (Book book in books)
            {
                Console.WriteLine(book.Title);
            }
        }

        public static void BookTitlesSearch(BookShopContext context)
        {
            string checkStr = Console.ReadLine().ToLower();

            List<Book> books = context.Books
                .Where(b => b.Author.LastName.ToLower().StartsWith(checkStr))
                .OrderBy(b => b.Id)
                .ToList();

            foreach (Book book in books)
            {
                Console.WriteLine($"{book.Title} ({book.Author.FirstName} {book.Author.LastName})");
            }
        }

        public static void CountBooks(BookShopContext context)
        {
            int length = int.Parse(Console.ReadLine());

            int bookCount = context.Books.Where(b => b.Title.Length > length).Count();

            Console.WriteLine(bookCount);
        }

        public static void TotalBookCopies(BookShopContext context)
        {
            var books = context.Books
                .GroupBy(b => b.Author)
                .Select(b => new { Author = b.Key, Copies = b.Sum(c => c.Copies) })
                .OrderByDescending(c => c.Copies)
                .ToList();

            foreach (var book in books)
            {
                Console.WriteLine($"{book.Author.FirstName} {book.Author.LastName} - {book.Copies}");
            }
        }

        public static void FindProfit(BookShopContext context)
        {
            var result = context.Categories
                .Select(c => new { CategoryName = c.Name, Profit = c.Books.Sum(b => (b.Copies * b.Price)) })
                .OrderByDescending(c => c.Profit)
                .ThenBy(c => c.CategoryName)
                .ToList();

            foreach (var res in result)
            {
                Console.WriteLine($"{res.CategoryName} - ${res.Profit:F2}");
            }
        }

        public static void MostRecentBooks(BookShopContext context)
        {
            var categories = context.Categories.Where(c => c.Books.Count() > 35).OrderByDescending(c => c.Books.Count());

            foreach (var cat in categories)
            {
                Console.WriteLine($"--{cat.Name}: {cat.Books.Count} books");

                var books = cat.Books.OrderByDescending(b => b.ReleaseDate).ThenBy(b => b.Title).Take(3);

                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Title} ({book.ReleaseDate.Year})");
                }
            }
        }

        public static void IncreaseBookCopies(BookShopContext context)
        {
            DateTime date = DateTime.ParseExact("2013-06-06", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            int count = 0;

            List<Book> books = context.Books
                .Where(b => b.ReleaseDate > date)
                .ToList();

            foreach (var book in books)
            {
                book.Copies += 44;
                count++;
            }

            context.SaveChanges();

            Console.WriteLine(44 * count);
        }

        public static void RemoveBooks(BookShopContext context)
        {
            List<Book> books = context.Books
                .Where(b => b.Copies < 4200)
                .ToList();

            Console.WriteLine($"{books.Count()} books were deleted");

            foreach (Book book in books)
            {
                context.Books.Remove(book);
            }

            context.SaveChanges();
        }

        public static void StoredProcedure(BookShopContext context)
        {
            string[] nameArgs = Console.ReadLine().Split();

            SqlParameter firstName = new SqlParameter("@firstName", SqlDbType.VarChar);
            firstName.Value = nameArgs[0];
            SqlParameter lastName = new SqlParameter("@lastName", SqlDbType.VarChar);
            lastName.Value = nameArgs[1];

            var result = context.Database.SqlQuery<int>("EXEC dbo.usp_GetsTotalBooksByAuthor @firstName, @lastName", firstName, lastName).First(); // without .First returns collection

            Console.WriteLine($"{nameArgs[0]} {nameArgs[1]} has written {result} books");
        }
    }
}
