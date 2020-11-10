namespace Affinity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNotificationClassAndRelations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotificationDetails = c.String(),
                        ProjectId = c.Int(),
                        TaskId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .ForeignKey("dbo.Tasks", t => t.TaskId)
                .Index(t => t.ProjectId)
                .Index(t => t.TaskId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "TaskId", "dbo.Tasks");
            DropForeignKey("dbo.Notifications", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Notifications", new[] { "TaskId" });
            DropIndex("dbo.Notifications", new[] { "ProjectId" });
            DropTable("dbo.Notifications");
        }
    }
}
