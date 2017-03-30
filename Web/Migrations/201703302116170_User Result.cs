namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserResult : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        ResultSectionId = c.Int(nullable: false),
                        Anotation = c.String(),
                        CompanyName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ResultSections", t => t.ResultSectionId, cascadeDelete: true)
                .Index(t => t.ResultSectionId);
            
            CreateTable(
                "dbo.ResultSections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SectionId = c.Int(nullable: false),
                        SectionTypeId = c.Int(nullable: false),
                        ResultId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Results", t => t.ResultId, cascadeDelete: true)
                .ForeignKey("dbo.Sections", t => t.SectionTypeId, cascadeDelete: true)
                .Index(t => t.SectionTypeId)
                .Index(t => t.ResultId);
            
            CreateTable(
                "dbo.Results",
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
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuestionAnswerValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        QuestionAnswerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionAnswers", t => t.QuestionAnswerId, cascadeDelete: true)
                .Index(t => t.QuestionAnswerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionAnswerValues", "QuestionAnswerId", "dbo.QuestionAnswers");
            DropForeignKey("dbo.ResultSections", "SectionTypeId", "dbo.Sections");
            DropForeignKey("dbo.ResultSections", "ResultId", "dbo.Results");
            DropForeignKey("dbo.Results", "PublishSurveyId", "dbo.PublishSurveys");
            DropForeignKey("dbo.QuestionAnswers", "ResultSectionId", "dbo.ResultSections");
            DropIndex("dbo.QuestionAnswerValues", new[] { "QuestionAnswerId" });
            DropIndex("dbo.Results", new[] { "PublishSurveyId" });
            DropIndex("dbo.ResultSections", new[] { "ResultId" });
            DropIndex("dbo.ResultSections", new[] { "SectionTypeId" });
            DropIndex("dbo.QuestionAnswers", new[] { "ResultSectionId" });
            DropTable("dbo.QuestionAnswerValues");
            DropTable("dbo.Sections");
            DropTable("dbo.Results");
            DropTable("dbo.ResultSections");
            DropTable("dbo.QuestionAnswers");
        }
    }
}
