namespace Affinity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeadlineFeature : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "DeadLine", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "DeadLine", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "DeadLine");
            DropColumn("dbo.Tasks", "DeadLine");
        }
    }
}
