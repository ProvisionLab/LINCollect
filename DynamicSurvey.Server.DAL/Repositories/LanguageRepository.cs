using System;
using System.Linq;

namespace DynamicSurvey.Server.DAL.Repositories
{
	internal class LanguageRepository
	{
		public language AddLanguage(string language, DbSurveysContext dbContext = null)
		{
			var context = dbContext ?? new DbSurveysContext();

			try
			{

				var dbLanguage = context.language
						.SingleOrDefault(l => l.name == language);

				if (dbLanguage == null)
				{
					dbLanguage = new language()
					{
						name = language
					};
					dbLanguage = context.language.Add(dbLanguage);
				}

				return context.language
					.Where(l => l.name.Equals(language, StringComparison.OrdinalIgnoreCase))
					.Single();
			}
			finally
			{
				if (dbContext == null)
				{
					context.SaveChanges();
					context.Dispose();
				}
			}

		}
	}
}
