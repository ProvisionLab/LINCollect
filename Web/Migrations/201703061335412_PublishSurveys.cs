namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PublishSurveys : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PublishSurveys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyId = c.Int(nullable: false),
                        Link = c.String(),
                        UserName = c.String(),
                        UserEmail = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Surveys", t => t.SurveyId, cascadeDelete: true)
                .Index(t => t.SurveyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PublishSurveys", "SurveyId", "dbo.Surveys");
            DropIndex("dbo.PublishSurveys", new[] { "SurveyId" });
            DropTable("dbo.PublishSurveys");
        }
    }
}
