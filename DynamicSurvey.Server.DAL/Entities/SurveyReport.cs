namespace DynamicSurvey.Server.DAL.Entities
{
	public class SurveyReport
	{
		public User Enumerator { get; set; }
		public User Respondent { get; set; }
		public Company Company { get; set; }
		public Survey Report { get; set; }
	}

}
