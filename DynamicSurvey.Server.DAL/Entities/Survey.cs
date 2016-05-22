using System.Collections.Generic;

namespace DynamicSurvey.Server.DAL.Entities
{
	public class Survey
	{
		public ulong Id { get; set; }
		public string Title { get; set; }
		public string Language { get; set; }

		public ulong? LanguageId { get; set; }
		public List<SurveyPage> Pages { get; set; }

		public Survey()
		{
			Pages = new List<SurveyPage>();
		}
	}
}
