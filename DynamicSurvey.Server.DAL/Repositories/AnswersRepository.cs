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

        void AddReport(User caller, ulong respondentId, ulong companyId, DateTime timestamp, List<SurveyPage> pages);

        void UpdateAnswer(User caller, ulong answerId, string newAnswer);
        void RemoveAnswer(User caller, ulong answerId);
    }
    public class AnswersRepository : IAnswersRepository
    {
        #region view contract. TODO:
        // todo: 
        private class vw_survey_report_select_result
        {
            public ulong? EnumeratorId;
            public string Enumerator;
            public ulong? RespondentId;
            public string RespondentLogin;
            public ulong? SurveyTemplateId;
            public string SurveyTemplateName;
            public ulong? PageId;
            public string PageTitle;
            public ulong? GroupId;
            public string GroupLabel;
            public ulong? FieldId;
            public string FieldLabel;
            public ulong? UserAnswerId;
            public string UserAnswer;
            public ulong? SurveyId;
            public ulong? LanguageId;
            public string LanguageName;
            public ulong? CompanyId;
            public string CompanyName;
            public string CompanyAddress;
            public string CompanyPhoneNumber;
            public string CompanyPostalCode;
            public ulong? CityId;
            public string CityName;
            public ulong? CountryId;
            public string CountryName;

            public vw_survey_report_select_result(DataRow r)
            {
                Fill(r);
            }

            public void Fill(DataRow r)
            {
                EnumeratorId = r["EnumeratorId"] as ulong?;
                Enumerator = r["Enumerator"] as string;
                RespondentId = r["RespondentId"] as ulong?;
                RespondentLogin = r["RespondentLogin"] as string;
                SurveyTemplateId = r["SurveyTemplateId"] as ulong?;
                SurveyTemplateName = r["SurveyTemplateName"] as string;
                PageId = r["PageId"] as ulong?;
                PageTitle = r["PageTitle"] as string;
                GroupId = r["GroupId"] as ulong?;
                GroupLabel = r["GroupLabel"] as string;
                FieldId = r["FieldId"] as ulong?;
                FieldLabel = r["FieldLabel"] as string;
                UserAnswerId = r["UserAnswerId"] as ulong?;
                UserAnswer = r["UserAnswer"] as string;
                SurveyId = r["SurveyId"] as ulong?;
                LanguageId = r["LanguageId"] as ulong?;
                LanguageName = r["LanguageName"] as string;
                CompanyId = r["CompanyId"] as ulong?;
                CompanyName = r["CompanyName"] as string;
                CompanyAddress = r["CompanyAddress"] as string;
                CompanyPhoneNumber = r["CompanyPhoneNumber"] as string;
                CompanyPostalCode = r["CompanyPostalCode"] as string;
                CityId = r["CityId"] as ulong?;
                CityName = r["CityName"] as string;
                CountryId = r["CountryId"] as ulong?;
                CountryName = r["CountryName"] as string;
            }
        }
        #endregion
        public List<SurveyReport> GetReports(SurveyReportFilter filter)
        {
            List<SurveyReport> result = new List<SurveyReport>();

            // fill with common info
            DataEngine.Engine.SelectFromView(DataEngine.vw_survey_report,
                r =>
                {

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

                    res.Company = new Company()
                    {
                        Id = viewFields.CompanyId ?? 0,
                        Address = viewFields.CompanyAddress,
                        City = viewFields.CityName,
                        CityId = viewFields.CityId ?? 0,
                        Country = viewFields.CountryName,
                        CountryId = viewFields.CountryId ?? 0,
                        Name = viewFields.CompanyName,
                        PhoneNumber = viewFields.CompanyPhoneNumber,
                        PostalCode = viewFields.CompanyPostalCode
                    };

                    res.Report = new Survey()
                    {
                        Id = viewFields.SurveyId ?? 0,
                        Language = viewFields.LanguageName,
                        LanguageId = viewFields.LanguageId ?? 0,
                        Title = viewFields.PageTitle
                    };

                    result.Add(res);
                },
                whereClause: filter.ToWhereClause() + " GROUP BY SurveyId",
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
                        field.Label = !string.IsNullOrEmpty(viewFields.GroupLabel) ? viewFields.GroupLabel : viewFields.FieldLabel;
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

        public void AddReport(User caller, ulong respondentId, ulong companyId, DateTime timestamp, List<SurveyPage> pages)
        {
            var reportId = DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_add_survey, cmd =>
            {
                cmd.Parameters.AddWithValue("creator_login", caller.Username);
                cmd.Parameters.AddWithValue("creator_password", caller.Password);
                cmd.Parameters.AddWithValue("respondent_id", caller.Id);
                cmd.Parameters.AddWithValue("company_id", companyId);
                cmd.Parameters.AddWithValue("timestamp", timestamp);
            });

            foreach (var page in pages)
            {
                foreach (var field in page.Fields.Where(u => u.UserAnswer != null))
                {
                    DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_add_survey_detail, cmd =>
                    {
                        cmd.Parameters.AddWithValue("creator_login", caller.Username);
                        cmd.Parameters.AddWithValue("creator_password", caller.Password);
                        cmd.Parameters.AddWithValue("parent_survey_id", reportId);
                        cmd.Parameters.AddWithValue("field_id", field.Id);
                        cmd.Parameters.AddWithValue("answer", field.UserAnswer);
                    });
                }
            }
        }

        public void UpdateAnswer(User caller, ulong answerId, string newAnswer)
        {
            throw new NotImplementedException();
        }

        public void RemoveAnswer(User caller, ulong answerId)
        {
            throw new NotImplementedException();
        }
    }
}