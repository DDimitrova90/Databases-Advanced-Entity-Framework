namespace PlanetHunters.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Astronomers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Discoveries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateMade = c.DateTime(nullable: false),
                        TelescopeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Telescopes", t => t.TelescopeId)
                .Index(t => t.TelescopeId);
            
            CreateTable(
                "dbo.Planets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Mass = c.Double(nullable: false),
                        HostStarSystemId = c.Int(nullable: false),
                        DiscoveryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StarSystems", t => t.HostStarSystemId)
                .ForeignKey("dbo.Discoveries", t => t.DiscoveryId)
                .Index(t => t.HostStarSystemId)
                .Index(t => t.DiscoveryId);
            
            CreateTable(
                "dbo.StarSystems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Temperature = c.Int(nullable: false),
                        HostStarSystemId = c.Int(nullable: false),
                        DiscoveryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StarSystems", t => t.HostStarSystemId)
                .ForeignKey("dbo.Discoveries", t => t.DiscoveryId)
                .Index(t => t.HostStarSystemId)
                .Index(t => t.DiscoveryId);
            
            CreateTable(
                "dbo.Telescopes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Location = c.String(nullable: false, maxLength: 255),
                        MirrorDiameter = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PioneeredDiscoveries",
                c => new
                    {
                        PioneerId = c.Int(nullable: false),
                        DiscoveryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PioneerId, t.DiscoveryId })
                .ForeignKey("dbo.Astronomers", t => t.PioneerId, cascadeDelete: true)
                .ForeignKey("dbo.Discoveries", t => t.DiscoveryId, cascadeDelete: true)
                .Index(t => t.PioneerId)
                .Index(t => t.DiscoveryId);
            
            CreateTable(
                "dbo.ObservedDiscoveries",
                c => new
                    {
                        ObserverId = c.Int(nullable: false),
                        DiscoveryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ObserverId, t.DiscoveryId })
                .ForeignKey("dbo.Astronomers", t => t.ObserverId, cascadeDelete: true)
                .ForeignKey("dbo.Discoveries", t => t.DiscoveryId, cascadeDelete: true)
                .Index(t => t.ObserverId)
                .Index(t => t.DiscoveryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ObservedDiscoveries", "DiscoveryId", "dbo.Discoveries");
            DropForeignKey("dbo.ObservedDiscoveries", "ObserverId", "dbo.Astronomers");
            DropForeignKey("dbo.PioneeredDiscoveries", "DiscoveryId", "dbo.Discoveries");
            DropForeignKey("dbo.PioneeredDiscoveries", "PioneerId", "dbo.Astronomers");
            DropForeignKey("dbo.Discoveries", "TelescopeId", "dbo.Telescopes");
            DropForeignKey("dbo.Stars", "DiscoveryId", "dbo.Discoveries");
            DropForeignKey("dbo.Planets", "DiscoveryId", "dbo.Discoveries");
            DropForeignKey("dbo.Stars", "HostStarSystemId", "dbo.StarSystems");
            DropForeignKey("dbo.Planets", "HostStarSystemId", "dbo.StarSystems");
            DropIndex("dbo.ObservedDiscoveries", new[] { "DiscoveryId" });
            DropIndex("dbo.ObservedDiscoveries", new[] { "ObserverId" });
            DropIndex("dbo.PioneeredDiscoveries", new[] { "DiscoveryId" });
            DropIndex("dbo.PioneeredDiscoveries", new[] { "PioneerId" });
            DropIndex("dbo.Stars", new[] { "DiscoveryId" });
            DropIndex("dbo.Stars", new[] { "HostStarSystemId" });
            DropIndex("dbo.Planets", new[] { "DiscoveryId" });
            DropIndex("dbo.Planets", new[] { "HostStarSystemId" });
            DropIndex("dbo.Discoveries", new[] { "TelescopeId" });
            DropTable("dbo.ObservedDiscoveries");
            DropTable("dbo.PioneeredDiscoveries");
            DropTable("dbo.Telescopes");
            DropTable("dbo.Stars");
            DropTable("dbo.StarSystems");
            DropTable("dbo.Planets");
            DropTable("dbo.Discoveries");
            DropTable("dbo.Astronomers");
        }
    }
}
