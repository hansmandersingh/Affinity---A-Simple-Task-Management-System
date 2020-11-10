namespace Affinity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewColumnInNotificationClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "IsBugNotif", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "IsBugNotif");
        }
    }
}
