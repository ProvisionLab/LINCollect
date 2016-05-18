using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.Repositories
{
	public interface IAnswersRepository
	{
		List<SurveyReport> GetReports(SurveyReportFilter filter);

		void AddReport(User caller, int respondentId, int companyId, DateTime timestamp);

		void UpdateAnswer(User caller, int answerId, string newAnswer);
		void RemoveAnswer(User caller, int answerId);
	}
	public class AnswersRepository : IAnswersRepository
	{
		#region view contract. TODO:
		// todo: 
		private class vw_survey_report_select_result
		{
			public long? EnumeratorId;
			public string Enumerator;
			public long? RespondentId;
			public string RespondentLogin;
			public long? SurveyTemplateId;
			public string SurveyTemplateName;
			public long? PageId;
			public string PageTitle;
			public long? GroupId;
			public string GroupLabel;
			public long? FieldId;
			public string FieldLabel;
			public long? UserAnswerId;
			public string UserAnswer;
			public long? SurveyId;
			public long? LanguageId;
			public string LanguageName;
			public long? CompanyId;
			public string CompanyName;
			public string CompanyAddress;
			public string CompanyPhoneNumber;
			public string CompanyPostalCode;
			public long? CityId;
			public string CityName;
			public long? CountryId;
			public string CountryName;

			public vw_survey_report_select_result(DataRow r)
			{
				Fill(r);
			}

			public void Fill(DataRow r)
			{
				EnumeratorId = (long?)r["EnumeratorId"];
				Enumerator = (string)r["Enumerator"];
				RespondentId = (long?)r["RespondentId"];
				RespondentLogin = (string)r["RespondentLogin"];
				SurveyTemplateId = (long?)r["SurveyTemplateId"];
				SurveyTemplateName = (string)r["SurveyTemplateName"];
				PageId = (long?)r["PageId"];
				PageTitle = (string)r["PageTitle"];
				GroupId = (long?)r["GroupId"];
				GroupLabel = (string)r["GroupLabel"];
				FieldId = (long?)r["FieldId"];
				FieldLabel = (string)r["FieldLabel"];
				UserAnswerId = (long?)r["UserAnswerId"];
				UserAnswer = (string)r["UserAnswer"];
				SurveyId = (long?)r["SurveyId"];
				LanguageId = (long?)r["LanguageId"];
				LanguageName = (string)r["LanguageName"];
				CompanyId = (long?)r["CompanyId"];
				CompanyName = (string)r["CompanyName"];
				CompanyAddress = (string)r["CompanyAddress"];
				CompanyPhoneNumber = (string)r["CompanyPhoneNumber"];
				CompanyPostalCode = (string)r["CompanyPostalCode"];
				CityId = (long?)r["CityId"];
				CityName = (string)r["CityName"];
				CountryId = (long?)r["CountryId"];
				CountryName = (string)r["CountryName"];
			}
		}
		#endregion
		public List<SurveyReport> GetReports(SurveyReportFilter filter)
		{
			List<SurveyReport> result = new List<SurveyReport>();

			// fill with common info
			DataEngine.Engine.SelectFromView(DataEngine.vw_survey_report, 
				r => {

					var res = new SurveyReport();

					var viewFields = new vw_survey_report_select_result(r);

					res.Enumerator = new User()
					{
						Id = viewFields.EnumeratorId ?? 0,
						Username = viewFields.Enumerator
					};
					res.Respondent = new User()
					{
						Id = viewFields.RespondentId ?? 0,
						Username = viewFields.RespondentLogin
					};
					res.Report = new Survey()
					{
						Id = viewFields.SurveyId ?? 0,
						Language = viewFields.LanguageName,
						LanguageId = viewFields.LanguageId ?? 0,
						Title = viewFields.PageTitle
					};
				},
				filter.ToWhereClause() + " GROUP BY SurveyId",
				fillCommandAction: filter.FillCommand);

			// TODO: HACK: low performance...
			foreach (var survey in result)
			{

				var idFilter = new SurveyReportFilter()
				{
					SurveyId = survey.Report.Id

				};
				DataEngine.Engine.SelectFromView(DataEngine.vw_survey_report, 
					r => 
					{
						var viewFields = new vw_survey_report_select_result(r);

						var page = new SurveyPage();
						page.Id = viewFields.PageId ?? 0;
						page.Title = viewFields.PageTitle;
						survey.Report.Pages.Add(page);

					},
					whereClause: idFilter.ToWhereClause(),
					fillCommandAction: idFilter.FillCommand);

				foreach (var page in survey.Report.Pages)
				{
					var pageIdFilter = page.Id;

					DataEngine.Engine.SelectFromView(DataEngine.vw_survey_report,
					r =>
					{
						var viewFields = new vw_survey_report_select_result(r);

						var field = new SurveyField();
						field.Id = viewFields.FieldId ?? 0;
						field.Label = viewFields.FieldLabel;
						field.UserAnswer = viewFields.UserAnswer;
						field.UserAnswerId = viewFields.UserAnswerId;

						page.Fields.Add(field);

					},
					whereClause: idFilter.ToWhereClause(),
					fillCommandAction: idFilter.FillCommand);
				}
			}

			return result;

		}

		public void AddReport(User caller, int respondentId, int companyId, DateTime timestamp)
		{
			throw new NotImplementedException();
		}

		public void UpdateAnswer(User caller, int answerId, string newAnswer)
		{
			throw new NotImplementedException();
		}

		public void RemoveAnswer(User caller, int answerId)
		{
			throw new NotImplementedException();
		}
	}
}
