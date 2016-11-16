namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormatsOptions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "TextMin", c => c.String());
            AddColumn("dbo.Questions", "ValueMin", c => c.String());
            AddColumn("dbo.Questions", "ValueMax", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "ValueMax");
            DropColumn("dbo.Questions", "ValueMin");
            DropColumn("dbo.Questions", "TextMin");
        }
    }
}
