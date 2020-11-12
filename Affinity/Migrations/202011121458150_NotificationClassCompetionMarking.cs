namespace Affinity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationClassCompetionMarking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "IsCompletedNotif", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "IsCompletedNotif");
        }
    }
}
