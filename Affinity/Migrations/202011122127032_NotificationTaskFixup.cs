namespace Affinity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationTaskFixup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "IsTaskNotif", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "IsTaskNotif");
        }
    }
}
