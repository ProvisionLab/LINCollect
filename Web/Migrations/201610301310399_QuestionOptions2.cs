namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionOptions2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "Rows", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "Rows");
        }
    }
}
