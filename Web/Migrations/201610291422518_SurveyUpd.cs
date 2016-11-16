namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SurveyUpd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Surveys", "Name", c => c.String(nullable: false, maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Surveys", "Name", c => c.String(maxLength: 256));
        }
    }
}
