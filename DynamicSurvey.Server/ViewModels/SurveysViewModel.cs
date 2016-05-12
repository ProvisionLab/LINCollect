using System.Collections.Generic;
using DynamicSurvey.Server.DAL.Entities;

namespace DynamicSurvey.Server.ViewModels
{
	public class SurveysViewModel
	{
		public IEnumerable<Survey> Surveys { get; set; }
	}
}