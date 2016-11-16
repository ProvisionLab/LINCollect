namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Question : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "ShortName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Questions", "Text", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questions", "Text", c => c.String(nullable: false, maxLength: 256));
            DropColumn("dbo.Questions", "ShortName");
        }
    }
}
