namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sortRel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RelationshipItems", "SortNodeList", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RelationshipItems", "SortNodeList");
        }
    }
}
