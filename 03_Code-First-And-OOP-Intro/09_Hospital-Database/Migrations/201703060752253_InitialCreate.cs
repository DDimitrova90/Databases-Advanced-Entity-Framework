namespace _09_Hospital_Database.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Diagnoses",
                c => new
                    {
                        DiagnoseId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.DiagnoseId);
            
            CreateTable(
                "dbo.Medicaments",
                c => new
                    {
                        MedicamentId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MedicamentId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Email = c.String(),
                        BirthDate = c.String(nullable: false),
                        Picture = c.Byte(nullable: false),
                        HasInsurance = c.Boolean(nullable: false),
                        Diagnose_DiagnoseId = c.Int(),
                        Visitation_VisitationId = c.Int(),
                    })
                .PrimaryKey(t => t.PatientId)
                .ForeignKey("dbo.Diagnoses", t => t.Diagnose_DiagnoseId)
                .ForeignKey("dbo.Visitations", t => t.Visitation_VisitationId)
                .Index(t => t.Diagnose_DiagnoseId)
                .Index(t => t.Visitation_VisitationId);
            
            CreateTable(
                "dbo.Visitations",
                c => new
                    {
                        VisitationId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.VisitationId);
            
            CreateTable(
                "dbo.MedicamentDiagnoses",
                c => new
                    {
                        Medicament_MedicamentId = c.Int(nullable: false),
                        Diagnose_DiagnoseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Medicament_MedicamentId, t.Diagnose_DiagnoseId })
                .ForeignKey("dbo.Medicaments", t => t.Medicament_MedicamentId, cascadeDelete: true)
                .ForeignKey("dbo.Diagnoses", t => t.Diagnose_DiagnoseId, cascadeDelete: true)
                .Index(t => t.Medicament_MedicamentId)
                .Index(t => t.Diagnose_DiagnoseId);
            
            CreateTable(
                "dbo.PatientMedicaments",
                c => new
                    {
                        Patient_PatientId = c.Int(nullable: false),
                        Medicament_MedicamentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Patient_PatientId, t.Medicament_MedicamentId })
                .ForeignKey("dbo.Patients", t => t.Patient_PatientId, cascadeDelete: true)
                .ForeignKey("dbo.Medicaments", t => t.Medicament_MedicamentId, cascadeDelete: true)
                .Index(t => t.Patient_PatientId)
                .Index(t => t.Medicament_MedicamentId);
            
            CreateTable(
                "dbo.VisitationDiagnoses",
                c => new
                    {
                        Visitation_VisitationId = c.Int(nullable: false),
                        Diagnose_DiagnoseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Visitation_VisitationId, t.Diagnose_DiagnoseId })
                .ForeignKey("dbo.Visitations", t => t.Visitation_VisitationId, cascadeDelete: true)
                .ForeignKey("dbo.Diagnoses", t => t.Diagnose_DiagnoseId, cascadeDelete: true)
                .Index(t => t.Visitation_VisitationId)
                .Index(t => t.Diagnose_DiagnoseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "Visitation_VisitationId", "dbo.Visitations");
            DropForeignKey("dbo.VisitationDiagnoses", "Diagnose_DiagnoseId", "dbo.Diagnoses");
            DropForeignKey("dbo.VisitationDiagnoses", "Visitation_VisitationId", "dbo.Visitations");
            DropForeignKey("dbo.PatientMedicaments", "Medicament_MedicamentId", "dbo.Medicaments");
            DropForeignKey("dbo.PatientMedicaments", "Patient_PatientId", "dbo.Patients");
            DropForeignKey("dbo.Patients", "Diagnose_DiagnoseId", "dbo.Diagnoses");
            DropForeignKey("dbo.MedicamentDiagnoses", "Diagnose_DiagnoseId", "dbo.Diagnoses");
            DropForeignKey("dbo.MedicamentDiagnoses", "Medicament_MedicamentId", "dbo.Medicaments");
            DropIndex("dbo.VisitationDiagnoses", new[] { "Diagnose_DiagnoseId" });
            DropIndex("dbo.VisitationDiagnoses", new[] { "Visitation_VisitationId" });
            DropIndex("dbo.PatientMedicaments", new[] { "Medicament_MedicamentId" });
            DropIndex("dbo.PatientMedicaments", new[] { "Patient_PatientId" });
            DropIndex("dbo.MedicamentDiagnoses", new[] { "Diagnose_DiagnoseId" });
            DropIndex("dbo.MedicamentDiagnoses", new[] { "Medicament_MedicamentId" });
            DropIndex("dbo.Patients", new[] { "Visitation_VisitationId" });
            DropIndex("dbo.Patients", new[] { "Diagnose_DiagnoseId" });
            DropTable("dbo.VisitationDiagnoses");
            DropTable("dbo.PatientMedicaments");
            DropTable("dbo.MedicamentDiagnoses");
            DropTable("dbo.Visitations");
            DropTable("dbo.Patients");
            DropTable("dbo.Medicaments");
            DropTable("dbo.Diagnoses");
        }
    }
}
