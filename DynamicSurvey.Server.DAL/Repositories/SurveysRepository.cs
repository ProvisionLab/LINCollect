using System;
using System.Linq;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Helpers;

namespace DynamicSurvey.Server.DAL.Repositories
{
	public interface ISurveysRepository
	{
		Survey[] GetSurveys(User admin);
		Survey GetSurveyById(User admin, int id);
		decimal AddSurvey(User admin, Survey survey);
	}

	public class SurveysRepository : ISurveysRepository
	{
		private readonly VocabularyRepository vocabularyRepository;
		private readonly LanguageRepository languageRepository;
		private readonly UsersRepository usersRepository;
		private readonly FieldTypeRepository fieldTypeRepository;
		public SurveysRepository()
		{
			this.vocabularyRepository = new VocabularyRepository();
			this.languageRepository = new LanguageRepository();
			this.usersRepository = new UsersRepository();
			this.fieldTypeRepository = new FieldTypeRepository();
		}

		public Survey[] GetSurveys(User admin)
		{
			using (var context = new DbSurveysContext())
			{
				context.ValidateCaller(admin);
				return context.survey_template.Select(s => ToContract(s)).ToArray();
			}
			
		}

		public Survey GetSurveyById(User admin, int id)
		{
			using (var context = new DbSurveysContext())
			{
				context.ValidateCaller(admin);
				return ToContract(context.survey_template
					.Where(t => t.id == id).
					Single());
					
			}
		}

		public decimal AddSurvey(User admin, Survey survey)
		{
			using (var context = new DbSurveysContext())
			{
				context.ValidateCaller(admin);

				var surveyTemplate = ToData(survey, admin, context);
				// add 
				context.survey_template.Add(surveyTemplate);
				// or update
				// TODO:

				context.SaveChanges();
				return surveyTemplate.id;
			}
		}

		private Survey ToContract(survey_template template)
		{
			return new Survey()
			{
				Language = template.language.name,
				Title = template.template_name,
				Id = template.id,
				Pages = template.survey_page.Select(p => new SurveyPage()
				{
					Title = p.page_title,
					Fields = p.survey_field.Select(f => new SurveyField()
					{
						DefaultValues = f.survey_field_vocabulary_cross.Select(cr => cr.vocabulary.word).ToArray(),
						FieldType = f.survey_field_type.field_type,
						Id = f.id,
						Label = f.label,
						GroupId = f.fk_group_id
					}).ToList()
				}).ToList()
			};
		}

		private survey_template ToData(Survey survey, User admin, DbSurveysContext context)
		{
			var dbSurveyTemplate = new survey_template()
			{
				template_name = survey.Title,
				language = languageRepository.AddLanguage(survey.Language, context),
				created = DateTime.UtcNow,
				last_modified = DateTime.UtcNow,
				user_created_id = admin.Id,
				user_modified_id = admin.Id
			};

			for (int i = 0; i < survey.Pages.Count; i++)
			{
				var page = survey.Pages[i];
				var pageIndex = i;

				var dbPage = new survey_page()
				{
					page_index = i,
					page_title = page.Title,
					survey_template = dbSurveyTemplate,
					survey_field = page.Fields.Select(f =>
						{
							var dbField = new survey_field()
							{
								fk_survey_field_type_id = this.fieldTypeRepository.GetIdOf(f.FieldType),
								fk_group_id = f.GroupId,
								label = f.Label,
							};
							foreach (var value in f.DefaultValues)
							{
								var vocabularyRecord = context.vocabulary
								.Where(r => r.language.Equals(survey.Language))
								.Where(r => r.word.Equals(value))
								.SingleOrDefault();

								if (vocabularyRecord == null)
								{
									vocabularyRecord = new vocabulary()
									{
										language = languageRepository.AddLanguage(survey.Language, context),
										word = value
									};
								}

								dbField.survey_field_vocabulary_cross.Add(new survey_field_vocabulary_cross()
								{
									vocabulary = vocabularyRecord,
									survey_field = dbField
								}
								);
							}

							return dbField;
						}
					   ).ToArray()
				};

				dbSurveyTemplate.survey_page.Add(dbPage);
			}

			return dbSurveyTemplate;
		}
	}
}
