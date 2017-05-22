namespace _09_Hospital_Database.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedDoctorEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Stecialty = c.String(),
                    })
                .PrimaryKey(t => t.DoctorId);
            
            AddColumn("dbo.Visitations", "Doctor_DoctorId", c => c.Int());
            CreateIndex("dbo.Visitations", "Doctor_DoctorId");
            AddForeignKey("dbo.Visitations", "Doctor_DoctorId", "dbo.Doctors", "DoctorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visitations", "Doctor_DoctorId", "dbo.Doctors");
            DropIndex("dbo.Visitations", new[] { "Doctor_DoctorId" });
            DropColumn("dbo.Visitations", "Doctor_DoctorId");
            DropTable("dbo.Doctors");
        }
    }
}
