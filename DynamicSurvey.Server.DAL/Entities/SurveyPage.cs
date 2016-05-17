using System.Collections.Generic;

namespace DynamicSurvey.Server.DAL.Entities
{
	public class SurveyPage
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public long PageIndex { get; set; }
		public List<SurveyField> Fields { get; set; }

		public SurveyPage()
		{
			Fields = new List<SurveyField>();
		}
	}
}
