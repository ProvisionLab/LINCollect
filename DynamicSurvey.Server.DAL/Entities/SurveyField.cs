using System.Collections.Generic;
namespace DynamicSurvey.Server.DAL.Entities
{
	public class SurveyField
	{
		public long Id { get; set; }
		public string Label { get; set; }

		public string FieldType { get; set; }

		public long FieldTypeId { get; set; }

		public IEnumerable<string> DefaultValues { get; set; }

		public long? GroupId { get; set; }

		public string UserAnswer { get; set; }

		public long? UserAnswerId { get; set; }

		public SurveyField()
		{
			DefaultValues = new string[0];
		}
	}
}
