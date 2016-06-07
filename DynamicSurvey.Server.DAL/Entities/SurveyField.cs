using System.Collections.Generic;

namespace DynamicSurvey.Server.DAL.Entities
{
	public class SurveyField
	{
		public ulong Id { get; set; }
		public string Label { get; set; }

		public string FieldType { get; set; }

		public ulong FieldTypeId { get; set; }

	    public bool IsMandatory { get; set; }

		public IEnumerable<string> DefaultValues { get; set; }

		public ulong? GroupId { get; set; }

		public string UserAnswer { get; set; }

		public ulong? UserAnswerId { get; set; }

		public SurveyField()
		{
			DefaultValues = new string[0];
		}
	}
}
