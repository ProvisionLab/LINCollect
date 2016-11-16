namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SurveyFilesFix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SurveyFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 128),
                        Link = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Surveys", "SurveyFileId", c => c.Int(nullable: false));
            CreateIndex("dbo.Surveys", "SurveyFileId");
            AddForeignKey("dbo.Surveys", "SurveyFileId", "dbo.SurveyFiles", "Id", cascadeDelete: true);
            DropColumn("dbo.Surveys", "MailingFile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Surveys", "MailingFile", c => c.String());
            DropForeignKey("dbo.Surveys", "SurveyFileId", "dbo.SurveyFiles");
            DropIndex("dbo.Surveys", new[] { "SurveyFileId" });
            DropColumn("dbo.Surveys", "SurveyFileId");
            DropTable("dbo.SurveyFiles");
        }
    }
}
