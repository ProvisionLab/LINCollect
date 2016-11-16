namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldRelShip : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RelationshipItems", "HideAddedNodes", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RelationshipItems", "HideAddedNodes");
        }
    }
}
