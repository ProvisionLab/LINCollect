using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.Repositories
{
	internal interface ILanguageRepository
	{
		language AddLanguage(string language, DbSurveysContext dbContext = null);
	}
	internal class LanguageRepository : ILanguageRepository
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
