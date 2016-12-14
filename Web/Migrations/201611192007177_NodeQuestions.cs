namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NodeQuestions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RelationshipItemId = c.Int(nullable: false),
                        CreateDateUtc = c.DateTime(nullable: false),
                        UpdateDateUtc = c.DateTime(nullable: false),
                        OrderId = c.Int(nullable: false),
                        IsCompulsory = c.Boolean(nullable: false),
                        IsAfterSurvey = c.Boolean(nullable: false),
                        Introducing = c.String(),
                        ShortName = c.String(nullable: false, maxLength: 256),
                        Text = c.String(nullable: false),
                        QuestionFormatId = c.Int(nullable: false),
                        TextRowsCount = c.Int(),
                        IsAnnotation = c.Boolean(),
                        IsMultiple = c.Boolean(),
                        TextMin = c.String(),
                        TextMax = c.String(),
                        ValueMin = c.String(),
                        ValueMax = c.String(),
                        Resolution = c.Int(nullable: false),
                        IsShowValue = c.Boolean(),
                        Rows = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionFormats", t => t.QuestionFormatId, cascadeDelete: true)
                .ForeignKey("dbo.RelationshipItems", t => t.RelationshipItemId, cascadeDelete: true)
                .Index(t => t.RelationshipItemId)
                .Index(t => t.QuestionFormatId);
            
            CreateTable(
                "dbo.NAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NQuestionId = c.Int(nullable: false),
                        CreateDateUtc = c.DateTime(nullable: false),
                        UpdateDateUtc = c.DateTime(nullable: false),
                        OrderId = c.Int(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        Text = c.String(nullable: false, maxLength: 256),
                        Value = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NQuestions", t => t.NQuestionId, cascadeDelete: true)
                .Index(t => t.NQuestionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NQuestions", "RelationshipItemId", "dbo.RelationshipItems");
            DropForeignKey("dbo.NQuestions", "QuestionFormatId", "dbo.QuestionFormats");
            DropForeignKey("dbo.NAnswers", "NQuestionId", "dbo.NQuestions");
            DropIndex("dbo.NAnswers", new[] { "NQuestionId" });
            DropIndex("dbo.NQuestions", new[] { "QuestionFormatId" });
            DropIndex("dbo.NQuestions", new[] { "RelationshipItemId" });
            DropTable("dbo.NAnswers");
            DropTable("dbo.NQuestions");
        }
    }
}
