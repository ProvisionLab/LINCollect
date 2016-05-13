using DynamicSurvey.Server.DAL.Entities;
using System;
using DynamicSurvey.Server.DAL.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamicSurvey.Server.DAL.Helpers.Convertion;

namespace DynamicSurvey.Server.DAL.Repositories
{
	public interface ISurveysRepository
	{
		Survey[] GetSurveys(User admin);
		Survey GetSurveyById(User admin, int id);
		decimal AddSurvey(User admin, Survey survey);
		decimal AddPage(User admin, int surveyId, SurveyPage page);
		decimal AddSecion(User admin, int surveyPageId, SurveyField[] fieldsStartingWithGroupBox);
	}

	public class SurveysRepository : ISurveysRepository
	{
		private readonly VocabularyRepository vocabularyRepository;
		private readonly LanguageRepository languageRepository;
		private readonly UsersRepository usersRepository;
		private readonly FieldTypeRepository fieldTypeRepository;
		private readonly ISurveyConverter surveyConverter;
		public SurveysRepository()
		{
			this.vocabularyRepository = new VocabularyRepository();
			this.languageRepository = new LanguageRepository();
			this.usersRepository = new UsersRepository();
			this.fieldTypeRepository = new FieldTypeRepository();
			this.surveyConverter = new SurveyConverter();
		}

		public Survey[] GetSurveys(User admin)
		{
			using (var context = new DbSurveysContext())
			{
				context.ValidateCaller(admin);
				return context.survey_template.Select(s => surveyConverter.ToContract(s)).ToArray();
			}
			
		}

		public Survey GetSurveyById(User admin, int id)
		{
			using (var context = new DbSurveysContext())
			{
				context.ValidateCaller(admin);
				return surveyConverter.ToContract(context.survey_template
					.Where(t => t.id == id).
					Single());
					
			}
		}

		public decimal AddSurvey(User admin, Survey survey)
		{
			using (var context = new DbSurveysContext())
			{

				context.ValidateCaller(admin);

				//var surveyTemplate = surveyConverter.ToData(survey, admin, context, fieldTypeRepository, languageRepository);

				var surveyTemplate = context.survey_template
					.Where(st => st.template_name.Equals(survey.Title))
					.SingleOrDefault();

				var newTemplate = surveyConverter.ToData(survey, admin, context, fieldTypeRepository, languageRepository);
				if (surveyTemplate == null)
				{
					context.survey_template.Add(newTemplate);
				}
				else
				{
					newTemplate.id = surveyTemplate.id;
					surveyTemplate = newTemplate;
				}


				context.SaveChanges();
				return surveyTemplate.id;
			}
		}


		public decimal AddPage(User admin, int surveyId, SurveyPage page)
		{
			throw new NotImplementedException();
		}

		public decimal AddSecion(User admin, int surveyPageId, SurveyField[] fieldsStartingWithGroupBox)
		{
			throw new NotImplementedException();
		}
	}
}
