using System.Collections.Generic;

namespace DynamicSurvey.Server.DAL.Entities
{
	public class Survey
	{
		public decimal Id { get; set; }
		public string Title { get; set; }
		public string Language { get; set; }
		public List<SurveyPage> Pages { get; set; }
	}
}
