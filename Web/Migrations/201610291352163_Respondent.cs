namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Respondent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Respondents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyId = c.Int(nullable: false),
                        CreateDateUtc = c.DateTime(nullable: false),
                        UpdateDateUtc = c.DateTime(nullable: false),
                        IsAfterSurvey = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Surveys", t => t.SurveyId, cascadeDelete: true)
                .Index(t => t.SurveyId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RespondentId = c.Int(nullable: false),
                        CreateDateUtc = c.DateTime(nullable: false),
                        UpdateDateUtc = c.DateTime(nullable: false),
                        OrderId = c.Int(nullable: false),
                        IsCompulsory = c.Boolean(nullable: false),
                        IsAnnotation = c.Boolean(nullable: false),
                        IsMultiple = c.Boolean(nullable: false),
                        Introducing = c.String(),
                        Text = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Respondents", t => t.RespondentId, cascadeDelete: true)
                .Index(t => t.RespondentId);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        CreateDateUtc = c.DateTime(nullable: false),
                        UpdateDateUtc = c.DateTime(nullable: false),
                        OrderId = c.Int(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        Text = c.String(nullable: false, maxLength: 256),
                        Value = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Respondents", "SurveyId", "dbo.Surveys");
            DropForeignKey("dbo.Questions", "RespondentId", "dbo.Respondents");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropIndex("dbo.Questions", new[] { "RespondentId" });
            DropIndex("dbo.Respondents", new[] { "SurveyId" });
            DropTable("dbo.Answers");
            DropTable("dbo.Questions");
            DropTable("dbo.Respondents");
        }
    }
}
