namespace TeamBuilder.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                        Description = c.String(maxLength: 250),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        CreatorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 25),
                        Password = c.String(nullable: false),
                        FirstName = c.String(maxLength: 25),
                        LastName = c.String(maxLength: 25),
                        Age = c.Int(),
                        Gender = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                        Description = c.String(maxLength: 32),
                        Acronym = c.String(nullable: false, maxLength: 3),
                        CreatorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.Invitations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvitedUserId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.InvitedUserId)
                .Index(t => t.InvitedUserId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.TeamEvents",
                c => new
                    {
                        Team_Id = c.Int(nullable: false),
                        Event_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Team_Id, t.Event_Id })
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_Id, cascadeDelete: true)
                .Index(t => t.Team_Id)
                .Index(t => t.Event_Id);
            
            CreateTable(
                "dbo.TeamUsers",
                c => new
                    {
                        Team_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Team_Id, t.User_Id })
                .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Team_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.Invitations", "InvitedUserId", "dbo.Users");
            DropForeignKey("dbo.Teams", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.TeamUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.TeamUsers", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Invitations", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.TeamEvents", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.TeamEvents", "Team_Id", "dbo.Teams");
            DropIndex("dbo.TeamUsers", new[] { "User_Id" });
            DropIndex("dbo.TeamUsers", new[] { "Team_Id" });
            DropIndex("dbo.TeamEvents", new[] { "Event_Id" });
            DropIndex("dbo.TeamEvents", new[] { "Team_Id" });
            DropIndex("dbo.Invitations", new[] { "TeamId" });
            DropIndex("dbo.Invitations", new[] { "InvitedUserId" });
            DropIndex("dbo.Teams", new[] { "CreatorId" });
            DropIndex("dbo.Teams", new[] { "Name" });
            DropIndex("dbo.Users", new[] { "Username" });
            DropIndex("dbo.Events", new[] { "CreatorId" });
            DropTable("dbo.TeamUsers");
            DropTable("dbo.TeamEvents");
            DropTable("dbo.Invitations");
            DropTable("dbo.Teams");
            DropTable("dbo.Users");
            DropTable("dbo.Events");
        }
    }
}
