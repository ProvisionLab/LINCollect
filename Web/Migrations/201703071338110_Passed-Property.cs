namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PassedProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PublishSurveys", "IsPassed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PublishSurveys", "IsPassed");
        }
    }
}
