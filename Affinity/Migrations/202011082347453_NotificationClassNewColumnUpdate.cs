namespace Affinity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationClassNewColumnUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "IsWatched", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "IsWatched");
        }
    }
}
