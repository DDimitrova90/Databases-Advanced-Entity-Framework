namespace TeamBuilder.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeDatesNotNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Events", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "EndDate", c => c.DateTime());
            AlterColumn("dbo.Events", "StartDate", c => c.DateTime());
        }
    }
}
