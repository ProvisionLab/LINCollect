namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QFormat : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionFormats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Code = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Questions", "QuestionFormatId", c => c.Int(nullable: false));
            CreateIndex("dbo.Questions", "QuestionFormatId");
            AddForeignKey("dbo.Questions", "QuestionFormatId", "dbo.QuestionFormats", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "QuestionFormatId", "dbo.QuestionFormats");
            DropIndex("dbo.Questions", new[] { "QuestionFormatId" });
            DropColumn("dbo.Questions", "QuestionFormatId");
            DropTable("dbo.QuestionFormats");
        }
    }
}
