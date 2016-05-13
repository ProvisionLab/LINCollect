using System.Collections.Generic;

namespace DynamicSurvey.Server.DAL.Entities
{
	public class SurveyPage
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public List<SurveyField> Fields { get; set; }
	}
}
