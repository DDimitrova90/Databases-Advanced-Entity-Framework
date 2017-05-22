namespace PhotographyWorkshop.Data
{
    using Models;
    using System.Data.Entity;

    public class PhotographyWorkshopContext : DbContext
    {
        public PhotographyWorkshopContext()
            : base("name=PhotographyWorkshopContext")
        {
        }

        public virtual DbSet<Workshop> Workshops { get; set; }

        public virtual DbSet<Photographer> Photographers { get; set; }

        public virtual DbSet<MirrorlessCamera> MirrorlessCameras { get; set; }

        public virtual DbSet<Len> Lenses { get; set; }

        public virtual DbSet<DSLRCamera> DSLRCameras { get; set; }

        public virtual DbSet<Accessory> Accessories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Workshop>()
                .HasRequired(w => w.Trainer)
                .WithMany(p => p.WorkshopsTrainer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Workshop>()
                .HasMany(w => w.Participants)
                .WithMany(p => p.Workshops);

            modelBuilder.Entity<Photographer>()
                .HasOptional(p => p.PrimaryCameraDSLR)
                .WithMany(c => c.PrimaryForPhotographers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photographer>()
                .HasOptional(p => p.SecondaryCameraDSLR)
                .WithMany(c => c.SecondaryForPhotographers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photographer>()
                .HasOptional(p => p.PrimaryCameraMirrorless)
                .WithMany(c => c.PrimaryForPhotographers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photographer>()
                .HasOptional(p => p.SecondaryCameraMirrorless)
                .WithMany(c => c.SecondaryForPhotographers)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}