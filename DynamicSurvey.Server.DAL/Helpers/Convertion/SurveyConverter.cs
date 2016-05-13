using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Repositories;
using System;
using System.Linq;

namespace DynamicSurvey.Server.DAL.Helpers.Convertion
{
	internal interface ISurveyConverter
	{
		Survey ToContract(survey_template template);
		survey_template ToData(Survey survey, User admin, DbSurveysContext context, IFieldTypeRepository fieldTypeRepository, ILanguageRepository languageRepository);
	}

	internal class SurveyConverter : ISurveyConverter
	{
		public Survey ToContract(survey_template template)
		{
			return new Survey()
			{
				Language = template.user_language.name,
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

		public survey_template ToData(Survey survey, 
			User admin, 
			DbSurveysContext context,  
			IFieldTypeRepository fieldTypeRepository, 
			ILanguageRepository languageRepository)
		{
			var userId = new UsersRepository().GetUserByName(admin.Username).Id;
			var dbSurveyTemplate = new survey_template()
			{
				template_name = survey.Title,
				user_language = languageRepository.AddLanguage(survey.Language, context),
				created = DateTime.UtcNow,
				last_modified = DateTime.UtcNow,
				user_created_id = userId,
				user_modified_id = userId
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
							fk_survey_field_type_id = fieldTypeRepository.GetIdOf(f.FieldType),
							fk_group_id = f.GroupId,
							fk_parent_page_id = page.Id,
							label = f.Label,
						};
						foreach (var value in f.DefaultValues)
						{
							var vocabularyRecord = context.vocabulary
								.Where(r => r.user_language.name.Equals(survey.Language))
								.Where(r => r.word.Equals(value))
								.SingleOrDefault();

							if (vocabularyRecord == null)
							{
								vocabularyRecord = new vocabulary()
								{
									user_language = languageRepository.AddLanguage(survey.Language, context),
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
