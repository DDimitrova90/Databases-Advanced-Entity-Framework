namespace WizardDepositsEx
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WizardDepositsContext : DbContext
    {
        public WizardDepositsContext()
            : base("name=WizardDepositsContext")
        {
        }

        public virtual DbSet<WizzardDepositsOld> WizzardDepositsOlds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WizzardDepositsOld>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<WizzardDepositsOld>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<WizzardDepositsOld>()
                .Property(e => e.Notes)
                .IsUnicode(false);

            modelBuilder.Entity<WizzardDepositsOld>()
                .Property(e => e.MagicWandCreator)
                .IsUnicode(false);

            modelBuilder.Entity<WizzardDepositsOld>()
                .Property(e => e.DepositGroup)
                .IsUnicode(false);

            modelBuilder.Entity<WizzardDepositsOld>()
                .Property(e => e.DepositAmount)
                .HasPrecision(8, 2);

            modelBuilder.Entity<WizzardDepositsOld>()
                .Property(e => e.DepositInterest)
                .HasPrecision(5, 2);

            modelBuilder.Entity<WizzardDepositsOld>()
                .Property(e => e.DepositCharge)
                .HasPrecision(8, 2);
        }
    }
}
