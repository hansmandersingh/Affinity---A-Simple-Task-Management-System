namespace Affinity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFieldAddedInTaskClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "IsCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "IsCompleted");
        }
    }
}
