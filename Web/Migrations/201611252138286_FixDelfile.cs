namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDelfile : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Surveys", "SurveyFileId", "dbo.SurveyFiles");
            DropIndex("dbo.Surveys", new[] { "SurveyFileId" });
            AlterColumn("dbo.Surveys", "SurveyFileId", c => c.Int());
            CreateIndex("dbo.Surveys", "SurveyFileId");
            AddForeignKey("dbo.Surveys", "SurveyFileId", "dbo.SurveyFiles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Surveys", "SurveyFileId", "dbo.SurveyFiles");
            DropIndex("dbo.Surveys", new[] { "SurveyFileId" });
            AlterColumn("dbo.Surveys", "SurveyFileId", c => c.Int(nullable: false));
            CreateIndex("dbo.Surveys", "SurveyFileId");
            AddForeignKey("dbo.Surveys", "SurveyFileId", "dbo.SurveyFiles", "Id", cascadeDelete: true);
        }
    }
}
