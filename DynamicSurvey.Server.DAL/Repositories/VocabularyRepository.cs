namespace DynamicSurvey.Server.DAL.Repositories
{
	internal class VocabularyRepository
	{
		//public vocabulary AddWord(string word, string language, DbSurveysContext dbContext = null)
		//{
		//	var context = dbContext ?? new DbSurveysContext();
		//	try
		//	{
		//		var dbWord = context.vocabulary
		//			.SingleOrDefault(v => v.word.Equals(word, StringComparison.OrdinalIgnoreCase));

		//		if (dbWord != null)
		//		{
		//			return dbWord;
		//		}
		//		var dbLanguage = new LanguageRepository().AddLanguage(language, context);

		//		var res = new vocabulary()
		//		{
		//			word = word,
		//			language = dbLanguage
		//		};
		//		context.vocabulary.Add(res);
		//	}
		//	finally
		//	{
		//		if (dbContext == null)
		//		{
		//			context.SaveChanges();
		//			context.Dispose();
		//		}
		//	}
		//}
	}
}
