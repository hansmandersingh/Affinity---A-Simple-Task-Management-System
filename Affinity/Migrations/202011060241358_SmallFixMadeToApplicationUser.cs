namespace Affinity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmallFixMadeToApplicationUser : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProjectTasks", newName: "Tasks");
            DropForeignKey("dbo.Projects", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Projects", new[] { "UserId" });
            DropColumn("dbo.Projects", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Projects", "UserId");
            AddForeignKey("dbo.Projects", "UserId", "dbo.AspNetUsers", "Id");
            RenameTable(name: "dbo.Tasks", newName: "ProjectTasks");
        }
    }
}
