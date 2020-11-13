namespace Affinity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changesinBudgetClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Budgets", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Budgets", "UserId");
            AddForeignKey("dbo.Budgets", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Budgets", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Budgets", new[] { "UserId" });
            DropColumn("dbo.Budgets", "UserId");
        }
    }
}
