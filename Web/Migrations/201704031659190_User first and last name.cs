namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Userfirstandlastname : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserSurvey",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        SurveyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.SurveyId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.Surveys", t => t.SurveyId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.SurveyId);
            
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSurvey", "SurveyId", "dbo.Surveys");
            DropForeignKey("dbo.UserSurvey", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserSurvey", new[] { "SurveyId" });
            DropIndex("dbo.UserSurvey", new[] { "UserId" });
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropTable("dbo.UserSurvey");
        }
    }
}
