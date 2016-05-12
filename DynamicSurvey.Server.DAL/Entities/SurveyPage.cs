using System.Collections.Generic;

namespace DynamicSurvey.Server.DAL.Entities
{
	public class SurveyPage
	{
		public string Title { get; set; }
		public List<SurveyField> Fields { get; set; }
	}
}
