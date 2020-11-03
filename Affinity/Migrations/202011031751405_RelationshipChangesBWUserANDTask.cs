namespace Affinity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationshipChangesBWUserANDTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Tasks", "UserId");
            AddForeignKey("dbo.Tasks", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Tasks", new[] { "UserId" });
            DropColumn("dbo.Tasks", "UserId");
        }
    }
}
