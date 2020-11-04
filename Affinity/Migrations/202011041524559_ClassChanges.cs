namespace Affinity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClassChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "IsCompleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tasks", "TaskContent", c => c.String());
            AddColumn("dbo.Tasks", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tasks", "CompletedPercentage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "CompletedPercentage");
            DropColumn("dbo.Tasks", "Time");
            DropColumn("dbo.Tasks", "TaskContent");
            DropColumn("dbo.Projects", "IsCompleted");
            DropColumn("dbo.Projects", "Time");
        }
    }
}
