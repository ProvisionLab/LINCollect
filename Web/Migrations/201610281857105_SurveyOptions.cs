namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SurveyOptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        Code = c.String(maxLength: 6),
                        ShortCode = c.String(maxLength: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Surveys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        CreateDateUtc = c.DateTime(nullable: false),
                        UpdateDateUtc = c.DateTime(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        SurveyStatusId = c.Int(nullable: false),
                        Name = c.String(maxLength: 256),
                        Banner = c.String(),
                        Introduction = c.String(),
                        Thanks = c.String(),
                        Landing = c.String(),
                        MailingFile = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .ForeignKey("dbo.SurveyStatus", t => t.SurveyStatusId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.LanguageId)
                .Index(t => t.SurveyStatusId);
            
            CreateTable(
                "dbo.SurveyStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Surveys", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Surveys", "SurveyStatusId", "dbo.SurveyStatus");
            DropForeignKey("dbo.Surveys", "LanguageId", "dbo.Languages");
            DropIndex("dbo.Surveys", new[] { "SurveyStatusId" });
            DropIndex("dbo.Surveys", new[] { "LanguageId" });
            DropIndex("dbo.Surveys", new[] { "UserId" });
            DropTable("dbo.SurveyStatus");
            DropTable("dbo.Surveys");
            DropTable("dbo.Languages");
        }
    }
}
