using DynamicSurvey.Server.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL
{
	public interface ISurveysRepository
	{
		Survey[] GetSurveys(User enumerator); // change enumerator param to "reportedBy" ?

		Survey GetSurveyById(int id);

		int AddSurvey(User admin, Survey survey);
		void UploadReport(User enumarator, SurveyReport report);
		void UploadReportBulk(User enumerator, IEnumerable<SurveyReport> reportList);
	}
}
