namespace TeamBuilder.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeConfiguration : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TeamEvents", newName: "EventTeams");
            RenameTable(name: "dbo.TeamUsers", newName: "UserTeams");
            RenameColumn(table: "dbo.EventTeams", name: "Team_Id", newName: "TeamId");
            RenameColumn(table: "dbo.EventTeams", name: "Event_Id", newName: "EventId");
            RenameColumn(table: "dbo.UserTeams", name: "Team_Id", newName: "TeamId");
            RenameColumn(table: "dbo.UserTeams", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.UserTeams", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.UserTeams", name: "IX_Team_Id", newName: "IX_TeamId");
            RenameIndex(table: "dbo.EventTeams", name: "IX_Event_Id", newName: "IX_EventId");
            RenameIndex(table: "dbo.EventTeams", name: "IX_Team_Id", newName: "IX_TeamId");
            DropPrimaryKey("dbo.EventTeams");
            DropPrimaryKey("dbo.UserTeams");
            AddPrimaryKey("dbo.EventTeams", new[] { "EventId", "TeamId" });
            AddPrimaryKey("dbo.UserTeams", new[] { "UserId", "TeamId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserTeams");
            DropPrimaryKey("dbo.EventTeams");
            AddPrimaryKey("dbo.UserTeams", new[] { "Team_Id", "User_Id" });
            AddPrimaryKey("dbo.EventTeams", new[] { "Team_Id", "Event_Id" });
            RenameIndex(table: "dbo.EventTeams", name: "IX_TeamId", newName: "IX_Team_Id");
            RenameIndex(table: "dbo.EventTeams", name: "IX_EventId", newName: "IX_Event_Id");
            RenameIndex(table: "dbo.UserTeams", name: "IX_TeamId", newName: "IX_Team_Id");
            RenameIndex(table: "dbo.UserTeams", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.UserTeams", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.UserTeams", name: "TeamId", newName: "Team_Id");
            RenameColumn(table: "dbo.EventTeams", name: "EventId", newName: "Event_Id");
            RenameColumn(table: "dbo.EventTeams", name: "TeamId", newName: "Team_Id");
            RenameTable(name: "dbo.UserTeams", newName: "TeamUsers");
            RenameTable(name: "dbo.EventTeams", newName: "TeamEvents");
        }
    }
}
