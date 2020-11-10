namespace Affinity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCommentCheck : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "IsBugNote", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "IsBugNote");
        }
    }
}
