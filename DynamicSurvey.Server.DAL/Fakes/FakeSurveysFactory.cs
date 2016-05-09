using DynamicSurvey.Server.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicSurvey.Server.Infrastructure.Fakes
{
    public class FakeSurveysFactory
    {
        private static Survey CreateEnglishSurvey()
        {
            return new Survey
                {
                    Language = "English",
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
                                        FieldType = FieldType.TextBox,
                                        DefaultValues = new string[] { "Enter textbox here" },
                                        Label = "Label for textbox"
                                    },
                                    new SurveyField()
                                    {
                                        FieldType = FieldType.Email,
                                        DefaultValues = new string[] { "Enter Email here" },
                                        Label = "Label for Email"
                                    },
                                    new SurveyField()
                                    {
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
                                        FieldType = FieldType.Checkbox,
                                        DefaultValues = new string[] { "checked" },
                                        Label = "cb1"
                                    },
                                    new SurveyField()
                                    {
                                        FieldType = FieldType.Checkbox,
                                        Label = "cb2"
                                    },
                                    new SurveyField()
                                    {
                                        FieldType = FieldType.Checkbox,
                                        Label = "cb3"
                                    },
                                    new SurveyField()
                                    {
                                        FieldType = FieldType.Checkbox,
                                        Label = "cb4"
                                    },
                                    new SurveyField()
                                    {
                                         FieldType = FieldType.Button,
                                         Label = "Submit"
                                    }
                                }
                            }
                        }
                };
        }

        private static Survey CreateRussianSurvey()
        {
            return new Survey
               {
                   Language = "Russian",
                   Title = "Отчёт на русском",
                   Pages = new List<SurveyPage>
                        {
                            new SurveyPage()
                            {
                                Title = "Страница 1",
                                
                                Fields = new List<SurveyField>
                                {
                                    new SurveyField()
                                    {
                                        FieldType = FieldType.TextBox,
                                        DefaultValues = new string[] { "введите текстбокс" },
                                        Label = "надпись для текстбокса"
                                    },
                                    new SurveyField()
                                    {
                                        FieldType = FieldType.Email,
                                        DefaultValues = new string[] { "введите почту" },
                                        Label = "надпись для почты:"
                                    },
                                    new SurveyField()
                                    {
                                        FieldType = FieldType.Button,
                                        Label = "Дальше"
                                    },
                                }
                            },
                            new SurveyPage()
                            {
                                Title = "Страница 2",
                                Fields = new List<SurveyField>
                                {
                                    new SurveyField()
                                    {
                                        FieldType = FieldType.Checkbox,
                                        DefaultValues = new string[] { "checked" },
                                        Label = "чекбокс 1"
                                    },
                                    new SurveyField()
                                    {
                                        FieldType = FieldType.Checkbox,
                                        Label = "чб2"
                                    },
                                    new SurveyField()
                                    {
                                        FieldType = FieldType.Checkbox,
                                        Label = "чб3"
                                    },
                                    new SurveyField()
                                    {
                                        FieldType = FieldType.Checkbox,
                                        Label = "чб4"
                                    },
                                    new SurveyField()
                                    {
                                         FieldType = FieldType.Button,
                                         Label = "Отправить"
                                    }
                                }
                            }
                        }
               };
        }

        public static Survey[] CreateSurveyList()
        {
            return new Survey[]
            {
                CreateEnglishSurvey(),
                CreateRussianSurvey()
            };
        }
    }
}