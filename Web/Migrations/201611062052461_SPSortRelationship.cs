namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SPSortRelationship : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "SortRelationship",
                p => new
                {
                    surveyId = p.Int()
                },
                @"DECLARE @orderId int, @id int, @index int
                SET @index = 1

                DECLARE @BuffTable TABLE (id int, orderId int)
                INSERT INTO @BuffTable (id, orderId)
                SELECT [Id], [OrderId] FROM [dbo].[RelationshipItems] WHERE [SurveyId] = @surveyId ORDER BY [OrderId]

                DECLARE Relationship_Cursor CURSOR FOR
                SELECT [Id] FROM @BuffTable ORDER BY [OrderId]

                OPEN Relationship_Cursor;

                FETCH NEXT FROM Relationship_Cursor INTO @id  

                WHILE @@FETCH_STATUS = 0
                   BEGIN
                      UPDATE [dbo].[RelationshipItems] SET [OrderId] = @index WHERE [Id] = @id
                      SET @index = @index + 1
	                  FETCH NEXT FROM Relationship_Cursor INTO @id
                   END
                CLOSE Relationship_Cursor
                DEALLOCATE Relationship_Cursor"
              );
        }
        
        public override void Down()
        {
            DropStoredProcedure("SortRelationship");
        }
    }
}
