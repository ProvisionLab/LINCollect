namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SPSortRespondentQuestions : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "SortRespondentQuestions",
                p => new
                {
                    respondentId = p.Int()
                },
                @"DECLARE @orderId int, @id int, @index int
                SET @index = 1

                DECLARE @BuffTable TABLE (id int, orderId int)
                INSERT INTO @BuffTable (id, orderId)
                SELECT [Id], [OrderId] FROM [dbo].[Questions] WHERE [RespondentId] = @respondentId ORDER BY [OrderId]

                DECLARE Question_Cursor CURSOR FOR
                SELECT [Id] FROM @BuffTable ORDER BY [OrderId]

                OPEN Question_Cursor;

                FETCH NEXT FROM Question_Cursor INTO @id  

                WHILE @@FETCH_STATUS = 0
                   BEGIN
                      UPDATE [dbo].[Questions] SET [OrderId] = @index WHERE [Id] = @id
                      SET @index = @index + 1
	                  FETCH NEXT FROM Question_Cursor INTO @id
                   END
                CLOSE Question_Cursor
                DEALLOCATE Question_Cursor"
              );
        }
        
        public override void Down()
        {
            DropStoredProcedure("SortRespondentQuestions");
        }
    }
}
