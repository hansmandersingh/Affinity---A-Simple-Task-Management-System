namespace Affinity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedBudgetClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        budgetAmount = c.Double(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Salary = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Budgets", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Budgets", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Budgets", new[] { "UserId" });
            DropIndex("dbo.Budgets", new[] { "ProjectId" });
            DropTable("dbo.Budgets");
        }
    }
}
