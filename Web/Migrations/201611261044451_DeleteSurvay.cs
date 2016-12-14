namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteSurvay : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "DeleteSurvay",
                p => new
                {
                    id = p.Int()
                },
                @"/*Respondents*/
DELETE FROM dbo.Answers 
WHERE QuestionId IN (SELECT Id FROM dbo.Questions 
					  WHERE RespondentId IN (SELECT Id FROM dbo.Respondents WHERE SurveyId = @id))

DELETE FROM dbo.Questions 
WHERE RespondentId IN (SELECT Id FROM dbo.Respondents WHERE SurveyId = @id)

DELETE FROM dbo.Respondents WHERE SurveyId = @id
						
/*RelationshipItems*/
DELETE FROM dbo.RAnswers 
WHERE RQuestionId IN (SELECT Id FROM dbo.RQuestions 
					  WHERE RelationshipItemId IN (SELECT Id FROM dbo.RelationshipItems WHERE SurveyId = @id))

DELETE FROM dbo.RQuestions 
WHERE RelationshipItemId IN (SELECT Id FROM dbo.RelationshipItems WHERE SurveyId = @id)

DELETE FROM dbo.NAnswers 
WHERE NQuestionId IN (SELECT Id FROM dbo.NQuestions 
					  WHERE RelationshipItemId IN (SELECT Id FROM dbo.RelationshipItems WHERE SurveyId = @id))

DELETE FROM dbo.NQuestions 
WHERE RelationshipItemId IN (SELECT Id FROM dbo.RelationshipItems WHERE SurveyId = @id)

DELETE FROM dbo.RelationshipItems WHERE SurveyId = @id

/*Surveys*/	
DELETE FROM [dbo].[Surveys] WHERE Id = @id

/*SurveyFiles*/
DELETE FROM dbo.SurveyFiles WHERE UserId IN (SELECT UserId FROM [dbo].[Surveys] WHERE Id = @id)"

              );
        }

        public override void Down()
        {
            DropStoredProcedure("DeleteSurvay");
        }
    }
}
