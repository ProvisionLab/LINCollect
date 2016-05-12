using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.Entities
{
	public class SurveyField
	{
		public decimal Id { get; set; }
		public string Label { get; set; }

		public string FieldType { get; set; }

		public string[] DefaultValues { get; set; }

		public decimal? GroupId { get; set; }

		public string UserAnswer { get; set; }
	}
}
