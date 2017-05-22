namespace _07_Gringotts_Database
{
    using System.Data.Entity;

    public class WizardDepositsContext : DbContext
    {
        public WizardDepositsContext()
            : base("name=WizardDepositsContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<WizardDeposit> WizardDeposits { get; set; }
    }
}