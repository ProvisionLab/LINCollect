using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DynamicSurvey.Server.DAL.Repositories;
using DynamicSurvey.Server.DAL.Fakes;
using DynamicSurvey.Server.DAL;
using System.Diagnostics;
using DynamicSurvey.Server.DAL.Entities;
using System.Collections.Generic;

namespace DynamicSurvey.Server.Tests.Repositories.SurveysRepositoryTests
{
	[TestClass]
	public class UnitTest1
	{
		private static readonly IFieldTypeRepository FieldType = new FieldTypeRepository();
		
		[TestMethod]
		public void TestMethod1()
		{
			var survey = GenerateSurvey();
			var admin = CommonHelpers.CreateAdmin(encrypted: true);
			ISurveysRepository repo = new SurveysRepository();
			repo.AddSurvey(admin, survey);
		}

		[TestMethod]
		public void TestMethod2()
		{
			var admin = CommonHelpers.CreateAdmin(encrypted: true);
			ISurveysRepository repo = new SurveysRepository();
			var res = repo.GetSurveys(admin);
		}

		private Survey GenerateSurvey()
		{
			return new Survey
			{
				Language = "English",
				LanguageId = 1,
				Title = "English Survey",
				Pages = new List<SurveyPage>
                        {
                            new SurveyPage()
                            {
			                    Title = "Page 1",                                
                                Fields = new List<SurveyField>
                                {
                                    new SurveyField()
                                    {
										FieldTypeId = FieldType.GetIdOf(FieldType.TextBox),
                                        FieldType = FieldType.TextBox,
                                        DefaultValues = new string[] { "Enter textbox here" },
                                        Label = "Label for textbox"
                                    },
                                    new SurveyField()
                                    {
										FieldTypeId = FieldType.GetIdOf(FieldType.Email),
                                        FieldType = FieldType.Email,
                                        DefaultValues = new string[] { "Enter Email here" },
                                        Label = "Label for Email"
                                    },
                                    new SurveyField()
                                    {
										FieldTypeId = FieldType.GetIdOf(FieldType.Button),
                                        FieldType = FieldType.Button,
                                        Label = "Next"
                                    },
                                }
                            },
                            new SurveyPage()
                            {
			                    Title = "Page 2",
                                Fields = new List<SurveyField>
                                {
									new SurveyField()
									{
										Id = 12,
										FieldTypeId = FieldType.GetIdOf(FieldType.GroupBox),
										FieldType = FieldType.GroupBox,
										Label = "Choose watched favorite Starwars episodes"
									},
                                    new SurveyField()
                                    {
										FieldTypeId = FieldType.GetIdOf(FieldType.Checkbox),
                                        FieldType = FieldType.Checkbox,
                                        DefaultValues = new string[] { "checked" },
                                        Label = "EPISODE 1",
										GroupId = 12
                                    },
                                    new SurveyField()
                                    {
										FieldTypeId = FieldType.GetIdOf(FieldType.Checkbox),
                                        FieldType = FieldType.Checkbox,
                                        Label = "EPISODE 2",
										GroupId = 12
                                    },
                                    new SurveyField()
                                    {
										FieldTypeId = FieldType.GetIdOf(FieldType.Checkbox),
                                        FieldType = FieldType.Checkbox,
                                        Label = "EPISODE 3",
										GroupId = 12
                                    },
                                    new SurveyField()
                                    {
										GroupId = 12,
										FieldTypeId = FieldType.GetIdOf(FieldType.Checkbox),
                                        FieldType = FieldType.Checkbox,
                                        Label = "All old episodes too",
                                    },
                                    new SurveyField()
                                    {
										FieldTypeId = FieldType.GetIdOf(FieldType.Button),
                                        FieldType = FieldType.Button,
                                        Label = "Submit"
                                    }
                                }
                            }
                        }
			};
		}
	}
}
