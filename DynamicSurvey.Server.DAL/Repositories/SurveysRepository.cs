﻿using System;
using System.Collections.Generic;
using System.Linq;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Helpers;
using MySql.Data.MySqlClient;

namespace DynamicSurvey.Server.DAL.Repositories
{
	public  interface ISurveysRepository
	{
		Survey[] GetSurveys(User admin, bool fullInfo = false);
		Survey GetSurveyById(User admin,  ulong  id);
		ulong AddSurvey(User admin, Survey survey);
		void UpdateSurvey(User admin, Survey survey);
		void RemoveSurvey(User admin,  ulong  surveyId);

		ulong AddPage(User admin,  ulong  surveyId, SurveyPage page);
		void UpdatePage(User admin, SurveyPage page);
		void RemovePage(User admin,  ulong  pageId);

		ulong AddField(User admin,  ulong  surveyPageId, SurveyField field);
		void UpdateField(User admin, SurveyField field);
		void RemoveField(User admin,  ulong  fieldId);

		ulong AddDefaultValue(User admin,  ulong  surveyFieldId,  ulong  languageId, string defaultValue);
		void UpdateDefaultValue(User admin, ulong surveyFieldId, ulong languageId, string oldValue, string newValue);
		void RemoveDefaultValue(User admin,  ulong  surveyFieldId, string value);

		//ulong AddSection(User admin,  ulong  surveyPageId, SurveyField[] fieldsStartingWithGroupBox);
		//void UpdateSection(User admin, SurveyField[] fieldsStartingWithGroupBox);
		//void RemoveSection(User admin,  ulong  groupBoxFieldId);
	}

	public class SurveysRepository : ISurveysRepository
	{
		private readonly DataEngine dataEngine = new DataEngine();
		private readonly VocabularyRepository vocabularyRepository;
		private readonly UsersRepository usersRepository;
		private readonly FieldTypeRepository fieldTypeRepository;

		private readonly string connectionString = "server=localhost;user id=CodeClient;password=In0Dd~uc!A55;persistsecurityinfo=True;database=dbsurveys;";


		public SurveysRepository()
		{
			this.vocabularyRepository = new VocabularyRepository();
			this.usersRepository = new UsersRepository();
			this.fieldTypeRepository = new FieldTypeRepository();
		}

		public Survey[] GetSurveys(User admin, bool fullInfo = false)
		{
			var surveyList = new List<Survey>();

			DataEngine.Engine.SelectFromView(DataEngine.vw_survey_template, (r) =>
			{
				//SELECT `id`, `title`, `language`, `languageId`, `date_created`, `user_created`, `date_modified`, `user_modified` FROM `dbsurveys`.`vw_survey_template`;
				surveyList.Add(new Survey()
				{
					Id = (ulong)(r["id"]),
					Language = Convert.ToString(r["language"]),
					LanguageId = (ulong)(r["languageId"]),
					Title = Convert.ToString(r["title"])
				});
			});

			if (!fullInfo)
				return surveyList.ToArray();

			foreach (var survey in surveyList)
			{
				var templateIdClause = "@TemplateId";
				DataEngine.Engine.SelectFromView(DataEngine.vw_survey_template_pages, (r) =>
				{
					// SELECT `TemplateId`, `PageId`, `PageIndex`, `PageLabel` FROM `dbsurveys`.`vw_survey_template_pages`;
					survey.Pages.Add(new SurveyPage()
					{
						PageIndex = (ulong)(r["PageIndex"]),
						Title = Convert.ToString(r["PageLabel"]),
						Id = (ulong)(r["PageId"])
					});
				},
				whereClause: "WHERE TemplateId=" + templateIdClause,
				fillCommandAction: (cmd) =>
				{
					cmd.Parameters.AddWithValue(templateIdClause, survey.Id);
				});

				foreach (var page in survey.Pages)
				{
					var pageIdClause = "@PageId";
					DataEngine.Engine.SelectFromView(DataEngine.vw_survey_template_fields, (r) =>
					{
						var f = new 
						{
							label = r["FieldLabel"],
							fieldType = r["FieldType"],
							fieldTypeId = r["FieldTypeId"],
							groupId = r["ParentGroupBoxIndex"] as ulong?,
							id = r["FieldId"]
						};
						
						page.Fields.Add(new SurveyField()
						{
							// SELECT `TemplateId`, `PageId`, `FieldLabel`, `FieldId`, `FieldIndex`, `ParentGroupBoxIndex`, `FieldType`, `FieldTypeId` FROM `dbsurveys`.`vw_survey_template_fields`;
							Id = (ulong)(f.id),
							Label = Convert.ToString(f.label),
							FieldType = Convert.ToString(f.fieldType),
							FieldTypeId = (ulong)(f.fieldTypeId),
							GroupId = f.groupId
							
						});
					},
					whereClause: "WHERE TemplateId=" + templateIdClause + " AND PageId=" + pageIdClause,
					fillCommandAction: (cmd) =>
					{
						cmd.Parameters.AddWithValue(templateIdClause, survey.Id);
						cmd.Parameters.AddWithValue(pageIdClause, page.Id);

					});

					foreach (var field in page.Fields)
					{
						var fieldIdClause = "@FieldId";
						var valuesList = new List<string>();
						DataEngine.Engine.SelectFromView(DataEngine.vw_survey_template_field_default_values, 
							(r) => 
							{
								var val = r["DefaultValue"];
								valuesList.Add(val.IsNull() ? "" : Convert.ToString(val));
							},
							whereClause: "WHERE FieldId="+fieldIdClause,
							fillCommandAction: (cmd) => 
							{
								cmd.Parameters.AddWithValue(fieldIdClause, field.Id);
							});
						field.DefaultValues = valuesList;
					}
				}
			}

			return surveyList.ToArray();
		}

		public Survey GetSurveyById(User admin,  ulong  id)
		{
			// TODO: rework this approach. 
			// Basically there is no need in GetSurveys(bool) parameter - 
			// it always should return non-detailed info
			// While GetSurveyById pulls full info with cross-join.
			return GetSurveys(admin, fullInfo: true).Single(s => s.Id == (ulong)id);
		}

		#region Survey
		// TODO: extract db-related code
		public ulong AddSurvey(User admin, Survey survey)
		{
			survey.Id = DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_add_survey_template, (cmd) =>
			{
				cmd.Parameters.Add(new MySqlParameter("creator_login", admin.Username));
				cmd.Parameters.Add(new MySqlParameter("creator_password", admin.Password));
				cmd.Parameters.Add(new MySqlParameter("template_title", survey.Title));
				cmd.Parameters.Add(new MySqlParameter("language_id", survey.LanguageId));
			});

			for ( int  iPage = 0; iPage < survey.Pages.Count; iPage++)
			{
				var page = survey.Pages[iPage];
				page.Id = AddPage(admin, ( ulong )survey.Id, page);
				for ( int  iField = 0; iField < page.Fields.Count; iField++)
				{
					var field = page.Fields[iField];
					var oldGroupId = field.Id;
					field.Id = AddField(admin, ( ulong )page.Id, field);

					if (field.FieldType == fieldTypeRepository.GroupBox)
					{
						UpdateGroupId(page.Fields, oldGroupId, field.Id);
					}

					foreach (var defaultValue in field.DefaultValues)
					{
						if (!string.IsNullOrEmpty(defaultValue))
						{
							AddDefaultValue(admin, ( ulong )field.Id, ( ulong )survey.LanguageId, defaultValue);
						}
					}
				}
			}

			return survey.Id;
		}

		public void UpdateSurvey(User admin, Survey survey)
		{
			DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_update_survey_template, (cmd) =>
			{
				cmd.Parameters.Add(new MySqlParameter("updator_login", admin.Username));
				cmd.Parameters.Add(new MySqlParameter("updator_password", admin.Password));
				cmd.Parameters.Add(new MySqlParameter("template_id", survey.Id));
				cmd.Parameters.Add(new MySqlParameter("template_title", survey.Title));
				cmd.Parameters.Add(new MySqlParameter("language_id", survey.LanguageId));
			});
		}

		public void RemoveSurvey(User admin,  ulong  surveyId)
		{
			DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_remove_survey_template, (cmd) =>
			{
				cmd.Parameters.Add(new MySqlParameter("updator_login", admin.Username));
				cmd.Parameters.Add(new MySqlParameter("updator_password", admin.Password));
				cmd.Parameters.Add(new MySqlParameter("template_id", surveyId));
			});
		}

		private void UpdateGroupId(List<SurveyField> fields, ulong oldGroupId, ulong newGroupId)
		{
			for ( int  i = 0; i < fields.Count; i++)
			{
				var field = fields[i];
				if (field.GroupId.HasValue && field.GroupId.Value == oldGroupId)
				{
					field.GroupId = newGroupId;
				}
			}
		}

		#endregion // survey

		#region page
		public ulong AddPage(User admin,  ulong  surveyId, SurveyPage page)
		{
			return DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_add_survey_template_page, (cmd) =>
			{
				cmd.Parameters.Add(new MySqlParameter("creator_login", admin.Username));
				cmd.Parameters.Add(new MySqlParameter("creator_password", admin.Password));
				cmd.Parameters.Add(new MySqlParameter("page_title", page.Title));
				cmd.Parameters.Add(new MySqlParameter("parent_survey_template_id", surveyId));
			});
		}


		public void UpdatePage(User admin, SurveyPage page)
		{
			DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_update_survey_template_page, (cmd) =>
			{
				cmd.Parameters.Add(new MySqlParameter("updator_login", admin.Username));
				cmd.Parameters.Add(new MySqlParameter("updator_password", admin.Password));
				cmd.Parameters.Add(new MySqlParameter("page_id", page.Id));
				cmd.Parameters.Add(new MySqlParameter("page_title", page.Title));
				cmd.Parameters.Add(new MySqlParameter("page_index", page.PageIndex));
			});
		}

		public void RemovePage(User admin,  ulong  pageId)
		{
			DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_remove_survey_template_page, (cmd) =>
			{
				cmd.Parameters.Add(new MySqlParameter("updator_login", admin.Username));
				cmd.Parameters.Add(new MySqlParameter("updator_password", admin.Password));
				cmd.Parameters.Add(new MySqlParameter("page_id", pageId));
			});
		}
		#endregion
		
		#region Field

		public ulong AddField(User admin,  ulong  surveyPageId, SurveyField field)
		{

			return DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_add_survey_template_field, (cmd) =>
			{
				cmd.Parameters.Add(new MySqlParameter("creator_login", admin.Username));
				cmd.Parameters.Add(new MySqlParameter("creator_password", admin.Password));
				cmd.Parameters.Add(new MySqlParameter("parent_page_id", surveyPageId));
				cmd.Parameters.Add(new MySqlParameter("survey_field_type_id", field.FieldTypeId));
				cmd.Parameters.Add(new MySqlParameter("group_id", field.GroupId));
				cmd.Parameters.Add(new MySqlParameter("label", field.Label));
			});
		}

		public void UpdateField(User admin, SurveyField field)
		{
			DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_update_survey_template_field, (cmd) =>
			{
				cmd.Parameters.Add(new MySqlParameter("creator_login", admin.Username));
				cmd.Parameters.Add(new MySqlParameter("creator_password", admin.Password));
				cmd.Parameters.Add(new MySqlParameter("field_id", field.Id));
				cmd.Parameters.Add(new MySqlParameter("new_label", field.Label));
			});
		}

		public void RemoveField(User admin,  ulong  fieldId)
		{
			DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_remove_survey_template_field, cmd =>
			{
				cmd.Parameters.Add(new MySqlParameter("creator_login", admin.Username));
				cmd.Parameters.Add(new MySqlParameter("creator_password", admin.Password));
				cmd.Parameters.Add(new MySqlParameter("field_id", fieldId));
			});
		}

		#endregion
		
		#region Default Value

		public ulong AddDefaultValue(User admin,  ulong  surveyFieldId,  ulong  languageId, string defaultValue)
		{
			return DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_add_field_default_value, cmd =>
			{
				cmd.Parameters.Add(new MySqlParameter("creator_login", admin.Username));
				cmd.Parameters.Add(new MySqlParameter("creator_password", admin.Password));
				cmd.Parameters.Add(new MySqlParameter("field_id", surveyFieldId));
				cmd.Parameters.Add(new MySqlParameter("language_id", languageId));
				cmd.Parameters.Add(new MySqlParameter("default_value", defaultValue));
			});
		}

		public void UpdateDefaultValue(User admin, ulong surveyFieldId, ulong languageId, string oldValue, string newValue)
		{
			DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_update_field_default_value, cmd => 
			{
				cmd.Parameters.Add(new MySqlParameter("creator_login", admin.Username));
				cmd.Parameters.Add(new MySqlParameter("creator_password", admin.Password));
				cmd.Parameters.Add(new MySqlParameter("field_id", surveyFieldId));
				cmd.Parameters.Add(new MySqlParameter("language_id", languageId));
				cmd.Parameters.Add(new MySqlParameter("old_default_value", oldValue));
				cmd.Parameters.Add(new MySqlParameter("new_default_value", newValue));
			});
		}

		public void RemoveDefaultValue(User admin,  ulong  surveyFieldId, string value)
		{
			DataEngine.Engine.ExecuteStoredProcedure(DataEngine.sp_remove_field_default_value, cmd => 
			{
				cmd.Parameters.Add(new MySqlParameter("creator_login", admin.Username));
				cmd.Parameters.Add(new MySqlParameter("creator_password", admin.Password));
				cmd.Parameters.Add(new MySqlParameter("field_id", surveyFieldId));
				cmd.Parameters.Add(new MySqlParameter("default_value_to_remove", value));
			});
		}
		#endregion // default value
	}
}
