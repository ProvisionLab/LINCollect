using System.Collections.Generic;

namespace DynamicSurvey.Server.DAL.Entities
{
	public class SurveyPage
	{
		public ulong Id { get; set; }
		public string Title { get; set; }
		public ulong PageIndex { get; set; }
		public List<SurveyField> Fields { get; set; }

		public SurveyPage()
		{
			Fields = new List<SurveyField>();
		}
	}
}
