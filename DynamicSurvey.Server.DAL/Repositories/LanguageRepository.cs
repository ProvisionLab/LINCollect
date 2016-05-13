using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.Repositories
{
	internal interface ILanguageRepository
	{
		user_language AddLanguage(string language, DbSurveysContext dbContext = null);
	}
	internal class LanguageRepository : ILanguageRepository
	{
		public user_language AddLanguage(string language, DbSurveysContext dbContext = null)
		{
			var context = dbContext ?? new DbSurveysContext();
			try
			{

				
				var dbLanguage = context.user_language
						.SingleOrDefault(l => l.name == language);

				if (dbLanguage == null)
				{
					dbLanguage = new user_language()
					{
						name = language
					};

					context.Database.ExecuteSqlCommand("INSERT INTO user_language (name) VALUES ({0})", language);
					return context.user_language.Where(l => l.name == language).Single();
				}

				return dbLanguage;
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
