namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAnswer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PublishSurveyId = c.Int(nullable: false),
                        PassDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PublishSurveys", t => t.PublishSurveyId, cascadeDelete: true)
                .Index(t => t.PublishSurveyId);
            
            CreateTable(
                "dbo.UserQuestionAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SectionId = c.Int(nullable: false),
                        SectionType = c.Byte(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        UserAnswer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserAnswers", t => t.UserAnswer_Id)
                .Index(t => t.UserAnswer_Id);
            
            CreateTable(
                "dbo.UserQuestionAnswerValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        QuestionAnswerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserQuestionAnswers", t => t.QuestionAnswerId, cascadeDelete: true)
                .Index(t => t.QuestionAnswerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserQuestionAnswerValues", "QuestionAnswerId", "dbo.UserQuestionAnswers");
            DropForeignKey("dbo.UserQuestionAnswers", "UserAnswer_Id", "dbo.UserAnswers");
            DropForeignKey("dbo.UserAnswers", "PublishSurveyId", "dbo.PublishSurveys");
            DropIndex("dbo.UserQuestionAnswerValues", new[] { "QuestionAnswerId" });
            DropIndex("dbo.UserQuestionAnswers", new[] { "UserAnswer_Id" });
            DropIndex("dbo.UserAnswers", new[] { "PublishSurveyId" });
            DropTable("dbo.UserQuestionAnswerValues");
            DropTable("dbo.UserQuestionAnswers");
            DropTable("dbo.UserAnswers");
        }
    }
}
