namespace TeamBuilder.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TeamBuilderContext : DbContext
    {
        public TeamBuilderContext()
            : base("name=TeamBuilderContext")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<TeamBuilderContext>());
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Team> Teams { get; set; }

        public virtual DbSet<Invitation> Invitations { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasRequired(e => e.Creator)
                .WithMany(u => u.CreatedEvents)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CreatedEvents)
                .WithRequired(e => e.Creator)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CreatedTeams)
                .WithRequired(t => t.Creator)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Invitations)
                .WithRequired(i => i.InvitedUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ParticipateTeams)
                .WithMany(t => t.Members)
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("TeamId");
                    m.ToTable("UserTeams");
                });

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Teams)
                .WithMany(t => t.Events)
                .Map(m =>
                {
                    m.MapLeftKey("EventId");
                    m.MapRightKey("TeamId");
                    m.ToTable("EventTeams");
                });
    
            base.OnModelCreating(modelBuilder);
        }
    }
}