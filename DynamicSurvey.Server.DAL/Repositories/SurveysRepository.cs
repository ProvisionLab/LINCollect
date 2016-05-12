using DynamicSurvey.Server.DAL.Entities;
using System;
using DynamicSurvey.Server.DAL.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.Repositories
{
	public interface ISurveysRepository
	{
		Survey[] GetSurveys(User admin);
		Survey GetSurveyById(User admin, int id);
		int AddSurvey(User admin, Survey survey);
		void UploadReport(User enumarator, SurveyReport report);
		void UploadReportBulk(User enumerator, IEnumerable<SurveyReport> reportList);
	}

	public class SurveysRepository : ISurveysRepository
	{
		private readonly VocabularyRepository vocabularyRepository;
		private readonly LanguageRepository languageRepository;
		private readonly UsersRepository usersRepository;
		public SurveysRepository()
		{
			this.vocabularyRepository = new VocabularyRepository();
			this.languageRepository = new LanguageRepository();
			this.usersRepository = new UsersRepository();
		}

		public Survey[] GetSurveys(User admin)
		{
			using (var context = new DbSurveysContext())
			{
				context.ValidateCaller(admin);
			}
			throw new NotImplementedException();
		}

		public Survey GetSurveyById(User admin, int id)
		{
			using (var context = new DbSurveysContext())
			{
				context.ValidateCaller(admin);
			}

			throw new NotImplementedException();
		}

		public int AddSurvey(User admin, Survey survey)
		{
			using (var context = new DbSurveysContext())
			{
				context.ValidateCaller(admin);


				var dbSurveyTemplate = new survey_template();
				dbSurveyTemplate.template_name = survey.Title;
				dbSurveyTemplate.language = languageRepository.AddLanguage(survey.Language, context);
				dbSurveyTemplate.created = DateTime.UtcNow;
				dbSurveyTemplate.last_modified = DateTime.UtcNow;
				dbSurveyTemplate.user_created_id = admin.Id;
				dbSurveyTemplate.user_modified_id = admin.Id;


				for (int i = 0; i < survey.Pages.Count; i++)
				{
					var page = survey.Pages[i];
					var pageIndex = i;
					// page.Title
					// page.Fields
					foreach (var field in page.Fields)
					{
						var dbField = new survey_field();
						dbField.fk_parent_survey_id = dbSurveyTemplate.id;
					}
				}


				//dbSurveyTemplate.

				context.SaveChanges();
			}

			throw new NotImplementedException();
		}

		public void UploadReport(User enumarator, SurveyReport report)
		{
			throw new NotImplementedException();
		}

		public void UploadReportBulk(User enumerator, IEnumerable<SurveyReport> reportList)
		{
			throw new NotImplementedException();
		}
	}
}
