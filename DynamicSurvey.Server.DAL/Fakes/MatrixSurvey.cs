
using System.Collections.Generic;
using DynamicSurvey.Server.DAL.Entities;

namespace DynamicSurvey.Server.DAL.Fakes
{
    class MatrixSurvey
    {
        private static readonly IFieldTypeRepository FieldType = new FieldTypeRepository();

        private static SurveyField[] CreateGroupRow(ulong id, ulong matrixId, string fieldType)
        {
            return new List<SurveyField>()
            {
                new SurveyField()
                {
                    Id = id,
                    GroupId = matrixId,
                    Label = "Stub question Id = " + id,
                    FieldType = FieldType.GroupBox
                },
                new SurveyField()
                {
                    Id = id+1,
                    GroupId = id,
                    Label = "Per day",
                    FieldType = fieldType
                },
                new SurveyField()
                {
                    Id = id+2,
                    GroupId = id,
                    Label = "Per week",
                    FieldType = fieldType
                },
                new SurveyField()
                {
                    Id = id+3,
                    GroupId = id,
                    Label = "Per year",
                    FieldType = fieldType
                }
            }.ToArray();
        }
        public static Survey CreateMatrixSurvey()
        {
            var page1 = new SurveyPage()
            {

                Id = 3000,
                Title = "Wake up, Neo.",
                Transitions = new List<SurveyPageTransition>()
            };
            page1.Fields.Add(
                new SurveyField()
                {
                    Id = 100,
                    FieldType = "Matrix",
                    Label = "The Radiobuttons"
                });
            page1.Fields.AddRange(CreateGroupRow(10, 100, FieldType.RadioButton));
            page1.Fields.AddRange(CreateGroupRow(20, 100, FieldType.RadioButton));
            page1.Fields.AddRange(CreateGroupRow(30, 100, FieldType.RadioButton));

            page1.Fields.Add(new SurveyField()
            {
                Id = 200,
                FieldType = "Matrix",
                Label = "The Checkboxes"
            });

            page1.Fields.AddRange(CreateGroupRow(110, 200, FieldType.Checkbox));
            page1.Fields.AddRange(CreateGroupRow(120, 200, FieldType.Checkbox));
            page1.Fields.AddRange(CreateGroupRow(130, 200, FieldType.Checkbox));

            return new Survey()
            {
                Pages = new List<SurveyPage>()
                {
                    page1
                },
                Title = "Matrix survey",
                Id = 3,
                Language = "English",
                LanguageId = 1,
            };
        }
    }
}
