namespace BookShopSystem
{
    using Migrations;
    using Models;
    using System.Data.Entity;

    public class BookShopContext : DbContext
    {
        public BookShopContext()
            : base("name=BookShopContext")
        {
            var migrationStategy =
              new MigrateDatabaseToLatestVersion<BookShopContext, Configuration>();
            Database.SetInitializer(migrationStategy);
        }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(b => b.RelatedBooks)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("BookId");
                    m.MapRightKey("RelatedId");
                    m.ToTable("RelatedBooks");
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}