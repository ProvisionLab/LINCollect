namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QOptions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "TextRowsCount", c => c.Int());
            AddColumn("dbo.Questions", "TextMax", c => c.String());
            AddColumn("dbo.Questions", "IsShowValue", c => c.Boolean());
            AlterColumn("dbo.Questions", "IsAnnotation", c => c.Boolean());
            AlterColumn("dbo.Questions", "IsMultiple", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questions", "IsMultiple", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Questions", "IsAnnotation", c => c.Boolean(nullable: false));
            DropColumn("dbo.Questions", "IsShowValue");
            DropColumn("dbo.Questions", "TextMax");
            DropColumn("dbo.Questions", "TextRowsCount");
        }
    }
}
