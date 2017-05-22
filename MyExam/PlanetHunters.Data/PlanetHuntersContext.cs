namespace PlanetHunters.Data
{
    using Models;
    using System.Data.Entity;

    public class PlanetHuntersContext : DbContext
    {
        public PlanetHuntersContext()
            : base("name=PlanetHuntersContext")
        {
        }    

        public virtual DbSet<Telescope> Telescopes { get; set; }

        public virtual DbSet<StarSystem> StarSystems { get; set; }

        public virtual DbSet<Star> Stars { get; set; }

        public virtual DbSet<Planet> Planets { get; set; }

        public virtual DbSet<Discovery> Discoveries { get; set; }

        public virtual DbSet<Astronomer> Astronomers { get; set; }

        public virtual DbSet<Publication> Publications { get; set; }

        public virtual DbSet<Journal> Journals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Astronomer>()
                .HasMany(a => a.MadeDiscoveries)
                .WithMany(d => d.Pioneers)
                .Map(m =>
                {
                    m.MapLeftKey("PioneerId");
                    m.MapRightKey("DiscoveryId");
                    m.ToTable("PioneeredDiscoveries");
                });

            modelBuilder.Entity<Astronomer>()
                .HasMany(a => a.ObservedDiscoveries)
                .WithMany(d => d.Observers)
                .Map(m =>
                {
                    m.MapLeftKey("ObserverId");
                    m.MapRightKey("DiscoveryId");
                    m.ToTable("ObservedDiscoveries");
                });

            modelBuilder.Entity<Discovery>()
                .HasRequired(d => d.Telescope)
                .WithMany(t => t.Discoveries)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Discovery>()
                .HasMany(d => d.Stars)
                .WithOptional(s => s.Discovery)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Discovery>()
                .HasMany(d => d.Planets)
                .WithOptional(p => p.Discovery)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StarSystem>()
                .HasMany(ss => ss.Stars)
                .WithRequired(s => s.HostStarSystem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StarSystem>()
                .HasMany(ss => ss.Planets)
                .WithRequired(p => p.HostStarSystem)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Publication>()
                .HasRequired(p => p.Journal)
                .WithMany(j => j.Publications)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Publication>()
                .HasRequired(p => p.Discovery)
                .WithMany(d => d.Publications)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}