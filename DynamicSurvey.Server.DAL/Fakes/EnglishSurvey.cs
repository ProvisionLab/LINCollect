using System.Collections.Generic;
using DynamicSurvey.Server.DAL.Entities;

namespace DynamicSurvey.Server.DAL.Fakes
{
    class EnglishSurvey
    {
        private static readonly IFieldTypeRepository FieldType = new FieldTypeRepository();


        private static List<SurveyField> CreatePage1Fields()
        {
            return new List<SurveyField>
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
                    DefaultValues = new string[] {"checked"},
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
            };
        }

        private static List<SurveyPageTransition> CreatePage1Transitions()
        {
            var res = SurveyPageTransition.CreateSimple(10001, 5);
            res.Add(new SurveyPageTransition()
            {
                Id = 10002,
                NextPageId = 4, // page2
                Conditions = new List<ConditionBase>()
                    {
                        new UnaryCondition(20001, UnaryConditionEnum.Equals, 21, "Checked")
                    }
            });
            return res;
        }

        private static List<SurveyField> CreatePage2Fields()
        {
            return new List<SurveyField>
            {
                new SurveyField()
                {
                    Id = 117,
                    FieldType = FieldType.GroupBox,
                    Label = "Do you like to see old heroes and new foes?"
                },
                new SurveyField()
                {
                    Id = 118,
                    FieldType = FieldType.RadioButton,
                    Label = "Yes, that brings good memories!",
                    GroupId = 117
                },
                new SurveyField()
                {
                    Id = 119,
                    FieldType = FieldType.RadioButton,
                    Label = "No, actors were legends before, but now they just grannies and granpas",
                    GroupId = 117
                },
                new SurveyField()
                {
                    Id = 122,
                    FieldType = FieldType.Button,
                    Label = "Submit"
                }
            };
        }

        private static List<SurveyField> CreatePage4Fields()
        {
            return new List<SurveyField>
            {
                new SurveyField()
                {
                    Id = 417,
                    FieldType = "Slider",
                    Label = "So, how old are you?",
                    DefaultValues = new string[] {"0","100"}
                },
                new SurveyField()
                {
                    Id = 424,
                    FieldType = FieldType.Button,
                    Label = "Submit"
                }
            };
        }

        private static List<SurveyField> CreatePage3Fields()
        {
            return new List<SurveyField>
            {
                new SurveyField()
                {
                    Id = 217,
                    FieldType = FieldType.GroupBox,
                    Label = "The Force"
                },
                new SurveyField()
                {
                    Id = 218,
                    FieldType = FieldType.Checkbox,
                    Label = "Control minds",
                    GroupId = 217
                },
                new SurveyField()
                {
                    Id = 219,
                    FieldType = FieldType.Checkbox,
                    Label = "Item and people Levitation power",
                    GroupId = 217
                },
                new SurveyField()
                {
                    Id = 220,
                    FieldType = FieldType.Checkbox,
                    Label = "Hyper sense of minds and feelings all over the galaxy",
                    GroupId = 217
                },
                new SurveyField()
                {
                    Id = 221,
                    FieldType = FieldType.GroupBox,
                    Label = "The Jedi path"
                },
                new SurveyField()
                {
                    Id = 222,
                    FieldType = FieldType.Checkbox,
                    Label = "Cold minded warrior is great",
                    GroupId = 221
                },
                new SurveyField()
                {
                    Id = 223,
                    FieldType = FieldType.Checkbox,
                    Label = "Sword art with laser sparks when swords are clashing",
                    GroupId = 221
                },
                new SurveyField()
                {
                    Id = 224,
                    FieldType = FieldType.Button,
                    Label = "Submit"
                }
            };
        }

        public static Survey CreateEnglishSurvey()
        {


            var page1 = new SurveyPage()
            {
                Id = 3,
                Title = "Page 1",
                Fields = CreatePage1Fields(),
                Transitions = CreatePage1Transitions()
            };

            var page2 = new SurveyPage()
            {
                Id = 4,
                Title = "Page 1.1 for a true fan",
                Fields = CreatePage2Fields(),
                Transitions = SurveyPageTransition.CreateSimple(10006, 5)
            };

            var page3 = new SurveyPage()
            {
                Id = 5,
                Title = "Page 2. What do you like in movie?",
                Fields = CreatePage3Fields(),
                Transitions = SurveyPageTransition.CreateSimple(10007, 6)
            };

            var page4 = new SurveyPage()
            {
                Id = 6,
                Title = "Fan profile",
                Fields = CreatePage4Fields(),
                Transitions = SurveyPageTransition.CreateFinal(10010)
            };

            return new Survey
            {
                Id = 2,
                Language = "English",
                LanguageId = 1,
                Title = "Movie fan",
                Pages = new List<SurveyPage>
                {
                    page1,
                    page2,
                    page3,
                    page4
                }
            };
        }

    }
}
