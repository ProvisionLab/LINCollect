namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Resolutionslider : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Resolution", c => c.Int(nullable: false));
            AddColumn("dbo.RQuestions", "Resolution", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RQuestions", "Resolution");
            DropColumn("dbo.Questions", "Resolution");
        }
    }
}
