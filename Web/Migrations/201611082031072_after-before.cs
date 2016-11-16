namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class afterbefore : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "IsAfterSurvey", c => c.Boolean(nullable: false));
            AddColumn("dbo.RQuestions", "IsAfterSurvey", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RQuestions", "IsAfterSurvey");
            DropColumn("dbo.Questions", "IsAfterSurvey");
        }
    }
}
