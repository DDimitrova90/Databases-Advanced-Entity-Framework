namespace Projection.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Salary = c.Decimal(precision: 18, scale: 2),
                        Birthday = c.DateTime(),
                        IsOnHoliday = c.Boolean(nullable: false),
                        Address = c.String(),
                        ManagerId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.ManagerId)
                .Index(t => t.ManagerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "ManagerId", "dbo.Employees");
            DropIndex("dbo.Employees", new[] { "ManagerId" });
            DropTable("dbo.Employees");
        }
    }
}
