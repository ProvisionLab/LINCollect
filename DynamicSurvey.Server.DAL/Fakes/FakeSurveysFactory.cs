using System.Collections.Generic;
using DynamicSurvey.Server.DAL.Entities;
using DynamicSurvey.Server.DAL.Repositories;

namespace DynamicSurvey.Server.DAL.Fakes
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
                CreateSurveyWithGroups(),
				CreateSurveyWithDateAndDropDown()
            };
		}

		public static Survey CreateSurveyWithDateAndDropDown()
		{
			#region page1
			var page1 = new SurveyPage()
			{
				Id = 1,
				Title = "Page 1. Datepicker asker",
				Fields = new List<SurveyField>()
                    {
                        new SurveyField()
						{
							Id = 1,
							Label = "Enter your birthdate and postback in string in format yyyy-mm-dd hh:mm:ss",
							FieldType = FieldType.DatePicker,
							DefaultValues = new string[] { "2010-02-18 14:38:00" }
						},
                        new SurveyField()
                        {
                            Id = 2,
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
				Title = "Page 2. Color question in dropdown.",
				Fields = new List<SurveyField>()
                {
                    new SurveyField()
                    {
                        Id = 3,
                        FieldType = FieldType.DropdownList,
                        Label = "What is your favorite color?",
                        DefaultValues = new string[]{ "Red" , "Green" , "Blue", "Pink", "Violet", "White", "Brown", "Purple", "Black", "Gray", "Yellow", "Cyan" },
                    },
                    new SurveyField()
                    {
                        Id = 4,
                        Label = "Submit",
                        FieldType = FieldType.Button
                    }
                }
			};
			#endregion

			return new Survey
			{
				Id = 123,
				Language = "English",
				Title = "English sur with groups",
				Pages = new List<SurveyPage>
                {
                    page1,
                    page2
                }
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
                                Id = 5,
                                Label = "Group Box Label. How many times you visit church per month?",
                                FieldType = FieldType.GroupBox
                        },

                        new SurveyField()
                        {
                            Id = 6,
                            Label = "Once",
                            FieldType = FieldType.RadioButton,
                            GroupId = 1
                        },
                        new SurveyField()
                        {
                            Id = 7,
                            Label = "Twice",
                            FieldType = FieldType.RadioButton,
                            GroupId = 1
                        },
                        new SurveyField()
                        {
                            Id =8,
                            Label = "Newer",
                            FieldType = FieldType.RadioButton,
                            GroupId = 1
                        },
                        new SurveyField()
                        {
                            Id = 9,
                            Label = "Group box label. Select capital of Japan",
                                FieldType = FieldType.GroupBox
                        },
                        new SurveyField()
                        {
                            Id = 10,
                            Label = "",
                            FieldType = FieldType.DropdownList,
                            DefaultValues = new string [] { "New York", "London", "Lima", "Cairo", "Moscow", "Kioto", "Osaka", "Okinawa", "Tokyo"},
                            GroupId = 9
                        },
                        new SurveyField()
                        {
                            Id = 11,
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
                        Id = 12,
                        FieldType = FieldType.DropdownList,
                        Label = "What is your favorite color?",
                        DefaultValues = new string[]{ "Red" , "Green" , "Blue", "Pink", "Violet", "White", "Brown", "Purple", "Black", "Gray", "Yellow", "Cyan" },
                    },
                    new SurveyField()
                    {
                        Id = 13,
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
					LanguageId = 1,
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
                                        Id = 14,
                                        FieldType = FieldType.TextBox,
                                        DefaultValues = new string[] { "Enter textbox here" },
                                        Label = "Label for textbox"
                                    },
                                    new SurveyField()
                                    {
                                        Id = 15,
                                        FieldType = FieldType.Email,
                                        DefaultValues = new string[] { "Enter Email here" },
                                        Label = "Label for Email"
                                    },
                                    new SurveyField()
                                    {
                                        Id = 16,
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
										Id = 17,
										FieldType = FieldType.GroupBox,
										Label = "Choose watched favorite Starwars episodes"
									},
                                    new SurveyField()
                                    {
                                        Id = 18,
                                        FieldType = FieldType.Checkbox,
                                        DefaultValues = new string[] { "checked" },
                                        Label = "EPISODE 1",
										GroupId = 17
                                    },
                                    new SurveyField()
                                    {
                                        Id = 19,
                                        FieldType = FieldType.Checkbox,
                                        Label = "EPISODE 2",
										GroupId = 17
                                    },
                                    new SurveyField()
                                    {
                                        Id = 20,
                                        FieldType = FieldType.Checkbox,
                                        Label = "EPISODE 3",
										GroupId = 17
                                    },
                                    new SurveyField()
                                    {
                                        Id = 21,
                                        FieldType = FieldType.Checkbox,
                                        Label = "All old episodes too",
										GroupId = 17
                                    },
                                    new SurveyField()
                                    {
                                        Id = 22,
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
                                        Id = 23,
                                        FieldType = FieldType.TextBox,
                                        DefaultValues = new string[] { "введите текстбокс" },
                                        Label = "надпись для текстбокса"
                                    },
                                    new SurveyField()
                                    {
                                        Id = 24,
                                        FieldType = FieldType.Email,
                                        DefaultValues = new string[] { "введите почту" },
                                        Label = "надпись для почты:"
                                    },
                                    new SurveyField()
                                    {
                                        Id = 25,
                                        FieldType = FieldType.Button,
                                        Label = "Дальше"
                                    },
                                }
                            },
                             new SurveyPage()
                            {
								Id = 4,
                                Title = "Страница 2",
                                Fields = new List<SurveyField>
                                {
									new SurveyField()
									{
										Id = 26,
										FieldType = FieldType.GroupBox,
										Label = "Какие части звездных войн вы смотрели?"
									},
                                    new SurveyField()
                                    {
                                        Id = 27,
                                        FieldType = FieldType.Checkbox,
                                        DefaultValues = new string[] { "checked" },
                                        Label = "Эпизод 1",
										GroupId = 26
                                    },
                                    new SurveyField()
                                    {
                                        Id = 28,
                                        FieldType = FieldType.Checkbox,
                                        Label = "Эпизод 2",
										GroupId = 26
                                    },
                                    new SurveyField()
                                    {
                                        Id = 29,
                                        FieldType = FieldType.Checkbox,
                                        Label = "Эпизод 3",
										GroupId = 26
                                    },
                                    new SurveyField()
                                    {
                                        Id = 30,
                                        FieldType = FieldType.Checkbox,
                                        Label = "Старые эпизоды тоже смотрел",
										GroupId = 26
                                    },
                                    new SurveyField()
                                    {
                                        Id = 31,
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