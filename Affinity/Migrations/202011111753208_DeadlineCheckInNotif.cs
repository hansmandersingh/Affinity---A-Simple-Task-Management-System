namespace Affinity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeadlineCheckInNotif : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "IsDeadlineNotif", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "IsDeadlineNotif");
        }
    }
}
