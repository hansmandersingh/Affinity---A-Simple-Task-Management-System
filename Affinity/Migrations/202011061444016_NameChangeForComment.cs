namespace Affinity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NameChangeForComment : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Notes", newName: "Comments");
            AddColumn("dbo.Comments", "Note", c => c.String());
            DropColumn("dbo.Comments", "Comment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Comment", c => c.String());
            DropColumn("dbo.Comments", "Note");
            RenameTable(name: "dbo.Comments", newName: "Notes");
        }
    }
}
