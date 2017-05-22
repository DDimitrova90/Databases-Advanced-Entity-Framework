namespace _02_Photographers.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PhotographersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "_02_Photographers.PhotographersContext";
        }

        protected override void Seed(PhotographersContext context)
        {
            Photographer p1 = new Photographer()
            {
                Username = "Spas",
                Password = "spas123",
                Email = "spas.Lutov@abv.bg",
                RegisterDate = new DateTime(2016, 08, 15)
            };

            Photographer p2 = new Photographer()
            {
                Username = "Ignat",
                Password = "ignat123",
                Email = "ignat.ignatov@abv.bg",
                RegisterDate = new DateTime(2015, 04, 11)
            };

            Photographer p3 = new Photographer()
            {
                Username = "Viktor",
                Password = "viktor123",
                Email = "viktor.viktorov@abv.bg",
                RegisterDate = new DateTime(2014, 07, 02),
                BirthDate = new DateTime(1988, 06, 17)
            };

            context.Photographers.AddOrUpdate(p => p.Username, p1, p2, p3);

            Album a1 = new Album()
            {
                Name = "Wedding",
                BackgroundColor = "Pile pink",
                IsPublic = true
            };

            Album a2 = new Album()
            {
                Name = "First Birthday",
                BackgroundColor = "Pile blue",
                IsPublic = false
            };

            Album a3 = new Album()
            {
                Name = "Graduating",
                BackgroundColor = "White",
                IsPublic = true
            };

            context.Albums.AddOrUpdate(a => a.Name, a1, a2, a3);

            context.SaveChanges();

            // Changes for problem 10
            PhotographerAlbum pa1 = new PhotographerAlbum()
            {
                Photographer_Id = p1.Id,
                Album_Id = a1.Id,
                Role = Role.Owner
            };

            PhotographerAlbum pa2 = new PhotographerAlbum()
            {
                Photographer_Id = p1.Id,
                Album_Id = a3.Id,
                Role = Role.Viewer
            };

            PhotographerAlbum pa3 = new PhotographerAlbum()
            {
                Photographer_Id = p2.Id,
                Album_Id = a2.Id,
                Role = Role.Owner
            };

            PhotographerAlbum pa4 = new PhotographerAlbum()
            {
                Photographer_Id = p2.Id,
                Album_Id = a3.Id,
                Role = Role.Owner
            };

            PhotographerAlbum pa5 = new PhotographerAlbum()
            {
                Photographer_Id = p3.Id,
                Album_Id = a2.Id,
                Role = Role.Viewer
            };

            context.PhotographerAlbums.AddOrUpdate(pa => pa.Role, pa1, pa2, pa3, pa4, pa5);

            context.SaveChanges();

            p1.Albums.Add(pa1);
            p1.Albums.Add(pa2);
            p2.Albums.Add(pa3);
            p2.Albums.Add(pa4);
            p3.Albums.Add(pa5);

            // till here

            context.SaveChanges();

            Picture pic1 = new Picture()
            {
                Title = "Fun",
                FilePath = "D://mnhj/gjhbh"
            };

            Picture pic2 = new Picture()
            {
                Title = "Laughing",
                FilePath = "D://sssss/ccccc"
            };

            context.Pictures.AddOrUpdate(p => p.Title, pic1, pic2);

            context.SaveChanges();

            pic1.Albums.Add(a1);
            pic1.Albums.Add(a3);
            pic2.Albums.Add(a2);

            context.SaveChanges();

            Tag t1 = new Tag()
            {
                Label = "#fuun"
            };

            Tag t2 = new Tag()
            {
                Label = "#memories"
            };

            context.Tags.AddOrUpdate(t => t.Label, t1, t2);

            context.SaveChanges();

            t1.Albums.Add(a1);
            t1.Albums.Add(a2);
            t2.Albums.Add(a3);

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
