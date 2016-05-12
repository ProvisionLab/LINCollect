using DynamicSurvey.Server.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicSurvey.Server.Infrastructure.Fakes
{
	public class FakeSurveysFactory
	{
		private static readonly IFieldTypeRepository FieldType = new FieldTypeRepository();
		public static Survey[] CreateSurveyList()
		{
			return new Survey[]
            {
                CreateEnglishSurvey(),
                CreateRussianSurvey(),
                CreateSurveyWithGroups()
            };
		}

		public static Survey CreateSurveyWithGroups()
		{
			#region page1
			var page1 = new SurveyPage()
				{
					Id = 1,
					Title = "Page 1",
					Fields = new List<SurveyField>()
                    {
                        new SurveyField()
                        {
                                Id = 1,
                                Label = "Group Box Label. How many times you visit church per month?",
                                FieldType = FieldType.GroupBox
                        },

                        new SurveyField()
                        {
                            Id = 2,
                            Label = "Once",
                            FieldType = FieldType.RadioButton,
                            GroupId = 1
                        },
                        new SurveyField()
                        {
                            Id = 3,
                            Label = "Twice",
                            FieldType = FieldType.RadioButton,
                            GroupId = 1
                        },
                        new SurveyField()
                        {
                            Id = 3,
                            Label = "Newer",
                            FieldType = FieldType.RadioButton,
                            GroupId = 1
                        },
                        new SurveyField()
                        {
                            Id = 11,
                            Label = "Group box label. Select capital of Japan",
                                FieldType = FieldType.GroupBox
                        },
                        new SurveyField()
                        {
                            Id = 12,
                            Label = "",
                            FieldType = FieldType.List,
                            DefaultValues = new string [] { "New York", "London", "Lima", "Cairo", "Moscow", "Kioto", "Osaka", "Okinawa", "Tokyo"},
                            GroupId = 11
                        },
                        new SurveyField()
                        {
                            Id = 100,
                            Label = "Next",
                            FieldType = FieldType.Button
                        }

                    }
				};
			#endregion
			#region page2
			var page2 = new SurveyPage()
			{
				Id = 2,
				Title = "Page 2. Color questions.",
				Fields = new List<SurveyField>()
                {
                    new SurveyField()
                    {
                        Id = 1,
                        FieldType = FieldType.List,
                        Label = "What is your favorite color?",
                        DefaultValues = new string[]{ "Red" , "Green" , "Blue", "Pink", "Violet", "White", "Brown", "Purple", "Black", "Gray", "Yellow", "Cyan" },
                    },
                    new SurveyField()
                    {
                        Id = 100,
                        Label = "Submit",
                        FieldType = FieldType.Button
                    }
                }
			};
			#endregion

			return new Survey
			{
				Id = 1,
				Language = "English",
				Title = "English sur with groups",
				Pages = new List<SurveyPage>
                {
                    page1,
                    page2
                }
			};
		}
		public static Survey CreateEnglishSurvey()
		{
			return new Survey
				{
					Id = 2,
					Language = "English",
					Title = "English Survey",
					Pages = new List<SurveyPage>
                        {
                            new SurveyPage()
                            {
								Id = 3,
                                Title = "Page 1",                                
                                Fields = new List<SurveyField>
                                {
                                    new SurveyField()
                                    {
                                        Id = 1,
                                        FieldType = FieldType.TextBox,
                                        DefaultValues = new string[] { "Enter textbox here" },
                                        Label = "Label for textbox"
                                    },
                                    new SurveyField()
                                    {
                                        Id = 2,
                                        FieldType = FieldType.Email,
                                        DefaultValues = new string[] { "Enter Email here" },
                                        Label = "Label for Email"
                                    },
                                    new SurveyField()
                                    {
                                        Id = 3,
                                        FieldType = FieldType.Button,
                                        Label = "Next"
                                    },
                                }
                            },
                            new SurveyPage()
                            {
								Id = 4,
                                Title = "Page 2",
                                Fields = new List<SurveyField>
                                {
                                    new SurveyField()
                                    {
                                        Id = 4,
                                        FieldType = FieldType.Checkbox,
                                        DefaultValues = new string[] { "checked" },
                                        Label = "cb1"
                                    },
                                    new SurveyField()
                                    {
                                        Id = 5,
                                        FieldType = FieldType.Checkbox,
                                        Label = "cb2"
                                    },
                                    new SurveyField()
                                    {
                                        Id = 6,
                                        FieldType = FieldType.Checkbox,
                                        Label = "cb3"
                                    },
                                    new SurveyField()
                                    {
                                        Id = 7,
                                        FieldType = FieldType.Checkbox,
                                        Label = "cb4"
                                    },
                                    new SurveyField()
                                    {
                                        Id = 8,
                                        FieldType = FieldType.Button,
                                        Label = "Submit"
                                    }
                                }
                            }
                        }
				};
		}

		public static Survey CreateRussianSurvey()
		{
			return new Survey
			   {
				   Id = 3,
				   Language = "Russian",
				   Title = "Отчёт на русском",
				   Pages = new List<SurveyPage>
                        {
                            new SurveyPage()
                            {
								Id = 6,
                                Title = "Страница 1",
                                
                                Fields = new List<SurveyField>
                                {
                                    new SurveyField()
                                    {
                                        Id = 11,
                                        FieldType = FieldType.TextBox,
                                        DefaultValues = new string[] { "введите текстбокс" },
                                        Label = "надпись для текстбокса"
                                    },
                                    new SurveyField()
                                    {
                                        Id = 12,
                                        FieldType = FieldType.Email,
                                        DefaultValues = new string[] { "введите почту" },
                                        Label = "надпись для почты:"
                                    },
                                    new SurveyField()
                                    {
                                        Id = 13,
                                        FieldType = FieldType.Button,
                                        Label = "Дальше"
                                    },
                                }
                            },
                            new SurveyPage()
                            {
								Id = 7,
                                Title = "Страница 2",
                                Fields = new List<SurveyField>
                                {
                                    new SurveyField()
                                    {
                                        Id = 14,
                                        FieldType = FieldType.Checkbox,
                                        DefaultValues = new string[] { "checked" },
                                        Label = "чекбокс 1"
                                    },
                                    new SurveyField()
                                    {
                                        Id = 15,
                                        FieldType = FieldType.Checkbox,
                                        Label = "чб2"
                                    },
                                    new SurveyField()
                                    {
                                        Id = 16,
                                        FieldType = FieldType.Checkbox,
                                        Label = "чб3"
                                    },
                                    new SurveyField()
                                    {
                                        Id = 17,
                                        FieldType = FieldType.Checkbox,
                                        Label = "чб4"
                                    },
                                    new SurveyField()
                                    {
                                        Id = 18,
                                         FieldType = FieldType.Button,
                                         Label = "Отправить"
                                    }
                                }
                            }
                        }
			   };
		}


	}
}