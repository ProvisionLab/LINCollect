namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationShipModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NodeSelections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        Code = c.String(maxLength: 36),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuestionLayouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        Code = c.String(maxLength: 36),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RQuestionId = c.Int(nullable: false),
                        CreateDateUtc = c.DateTime(nullable: false),
                        UpdateDateUtc = c.DateTime(nullable: false),
                        OrderId = c.Int(nullable: false),
                        IsDefault = c.Boolean(nullable: false),
                        Text = c.String(nullable: false, maxLength: 256),
                        Value = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RQuestions", t => t.RQuestionId, cascadeDelete: true)
                .Index(t => t.RQuestionId);
            
            CreateTable(
                "dbo.RelationshipItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyId = c.Int(nullable: false),
                        CreateDateUtc = c.DateTime(nullable: false),
                        UpdateDateUtc = c.DateTime(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Name = c.String(maxLength: 256),
                        NodeList = c.String(),
                        QuestionLayoutId = c.Int(nullable: false),
                        MaximumNodes = c.Int(nullable: false),
                        AddNodes = c.Boolean(nullable: false),
                        AllowSelectAllNodes = c.Boolean(nullable: false),
                        CanSkip = c.Boolean(nullable: false),
                        UseDDSearch = c.Boolean(nullable: false),
                        SuperUserViewNodes = c.Boolean(nullable: false),
                        NodeSelectionId = c.Int(nullable: false),
                        GeneratorName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NodeSelections", t => t.NodeSelectionId, cascadeDelete: true)
                .ForeignKey("dbo.QuestionLayouts", t => t.QuestionLayoutId, cascadeDelete: true)
                .ForeignKey("dbo.Surveys", t => t.SurveyId, cascadeDelete: true)
                .Index(t => t.SurveyId)
                .Index(t => t.QuestionLayoutId)
                .Index(t => t.NodeSelectionId);
            
            CreateTable(
                "dbo.RQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RelationshipItemId = c.Int(nullable: false),
                        CreateDateUtc = c.DateTime(nullable: false),
                        UpdateDateUtc = c.DateTime(nullable: false),
                        OrderId = c.Int(nullable: false),
                        IsCompulsory = c.Boolean(nullable: false),
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
                        IsShowValue = c.Boolean(),
                        Rows = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionFormats", t => t.QuestionFormatId, cascadeDelete: true)
                .ForeignKey("dbo.RelationshipItems", t => t.RelationshipItemId, cascadeDelete: true)
                .Index(t => t.RelationshipItemId)
                .Index(t => t.QuestionFormatId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RelationshipItems", "SurveyId", "dbo.Surveys");
            DropForeignKey("dbo.RQuestions", "RelationshipItemId", "dbo.RelationshipItems");
            DropForeignKey("dbo.RQuestions", "QuestionFormatId", "dbo.QuestionFormats");
            DropForeignKey("dbo.RAnswers", "RQuestionId", "dbo.RQuestions");
            DropForeignKey("dbo.RelationshipItems", "QuestionLayoutId", "dbo.QuestionLayouts");
            DropForeignKey("dbo.RelationshipItems", "NodeSelectionId", "dbo.NodeSelections");
            DropIndex("dbo.RQuestions", new[] { "QuestionFormatId" });
            DropIndex("dbo.RQuestions", new[] { "RelationshipItemId" });
            DropIndex("dbo.RelationshipItems", new[] { "NodeSelectionId" });
            DropIndex("dbo.RelationshipItems", new[] { "QuestionLayoutId" });
            DropIndex("dbo.RelationshipItems", new[] { "SurveyId" });
            DropIndex("dbo.RAnswers", new[] { "RQuestionId" });
            DropTable("dbo.RQuestions");
            DropTable("dbo.RelationshipItems");
            DropTable("dbo.RAnswers");
            DropTable("dbo.QuestionLayouts");
            DropTable("dbo.NodeSelections");
        }
    }
}
