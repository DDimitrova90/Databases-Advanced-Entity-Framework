namespace PlanetHunters.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelsJournalAndPublication : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Publications",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        DiscoveryId = c.Int(nullable: false),
                        JournalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Discoveries", t => t.DiscoveryId)
                .ForeignKey("dbo.Journals", t => t.JournalId)
                .Index(t => t.Id)
                .Index(t => t.JournalId);
            
            CreateTable(
                "dbo.Journals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Publications", "JournalId", "dbo.Journals");
            DropForeignKey("dbo.Publications", "Id", "dbo.Discoveries");
            DropIndex("dbo.Publications", new[] { "JournalId" });
            DropIndex("dbo.Publications", new[] { "Id" });
            DropTable("dbo.Journals");
            DropTable("dbo.Publications");
        }
    }
}
