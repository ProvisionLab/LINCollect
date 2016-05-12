using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.Entities
{
	public class Survey
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Language { get; set; }
		public List<SurveyPage> Pages { get; set; }
	}
}
