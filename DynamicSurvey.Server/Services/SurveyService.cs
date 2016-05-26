using System;
using System.Linq;
using DynamicSurvey.Core;
using DynamicSurvey.Core.Entities;
using DynamicSurvey.Core.SessionStorage;
using DynamicSurvey.Server.ViewModels;
using DynamicSurvey.Server.ViewModels.Surveys;
using NHibernate.Linq;

namespace DynamicSurvey.Server.Services
{
    public class SurveyService
    {
        public EditSurveyViewModel GetEditSurveyViewModel(int? surveyTemplateId)
        {
            var editSurveyViewModel = new EditSurveyViewModel();

            var session = PersistenceContext.GetCurrentSession();
            using (var transaction = session.BeginTransaction())
            {
                var languages = session.Query<UserLanguage>()
                    .OrderBy(ul => ul.Name)
                    .Select(ul => new LanguageItemViewModel
                    {
                        Id = ul.Id,
                        Name = ul.Name
                    })
                    .ToList();

                editSurveyViewModel.Languages = languages;

                if (surveyTemplateId != null)
                {
                    var surveyTemplate = session.Get<SurveyTemplate>(surveyTemplateId);
                    editSurveyViewModel.Id = surveyTemplate.Id;
                    editSurveyViewModel.TemplateName = surveyTemplate.TemplateName;
                    editSurveyViewModel.IntroductionText = surveyTemplate.IntroductionText;
                    editSurveyViewModel.ThankYouText = surveyTemplate.ThankYouText;
                    editSurveyViewModel.LandingPageText = surveyTemplate.LandingPageText;
                    editSurveyViewModel.LanguageId = surveyTemplate.UserLanguage.Id;
                }

                transaction.Commit();
            }

            return editSurveyViewModel;
        }

        public SurveyTemplate CreateSurveyTemplate(EditSurveyViewModel editSurveyViewModel)
        {
            var session = PersistenceContext.GetCurrentSession();
            using (var transaction = session.BeginTransaction())
            {
                var userLanguage = session.Get<UserLanguage>(editSurveyViewModel.LanguageId);

                var surveyTemplate = new SurveyTemplate
                {
                    TemplateName = editSurveyViewModel.TemplateName,
                    IntroductionText = editSurveyViewModel.IntroductionText,
                    ThankYouText = editSurveyViewModel.ThankYouText,
                    LandingPageText = editSurveyViewModel.LandingPageText,
                    UserLanguage = userLanguage,
                    Created = DateTime.UtcNow
                };

                session.Save(surveyTemplate);

                transaction.Commit();

                return surveyTemplate;
            }
        }

        public SurveyTemplate EditSurveyTemplate(EditSurveyViewModel editSurveyViewModel)
        {
            var session = PersistenceContext.GetCurrentSession();
            using (var transaction = session.BeginTransaction())
            {
                var surveyTemplate = session.Get<SurveyTemplate>(editSurveyViewModel.Id);

                var userLanguage = session.Get<UserLanguage>(editSurveyViewModel.LanguageId);

                surveyTemplate.TemplateName = editSurveyViewModel.TemplateName;
                surveyTemplate.IntroductionText = editSurveyViewModel.IntroductionText;
                surveyTemplate.ThankYouText = editSurveyViewModel.ThankYouText;
                surveyTemplate.LandingPageText = editSurveyViewModel.LandingPageText;
                surveyTemplate.UserLanguage = userLanguage;
                surveyTemplate.LastModified = DateTime.UtcNow;

                session.Save(surveyTemplate);

                transaction.Commit();

                return surveyTemplate;
            }
        }

        public EditRespondentViewModel GetEditRespondentViewModel(QuestionAction? questionAction)
        {
            var editRespondentViewModel = new EditRespondentViewModel
            {
                QuestionAction = questionAction
            };

            var editQuestionViewModel = new EditQuestionViewModel();
            editRespondentViewModel.EditQuestionViewModel = editQuestionViewModel;

            return editRespondentViewModel;
        }

        public SurveyField CreateQuestion(EditQuestionViewModel editQuestionViewModel)
        {
            var session = PersistenceContext.GetCurrentSession();
            using (var transaction = session.BeginTransaction())
            {
                var firstPage = session.Get<SurveyPage>(1);

                var question = new SurveyField
                {
                    Label = editQuestionViewModel.Question,
                    FieldIndex = 1,
                    ParentPage = firstPage
                };

                FieldType questionFieldType;
                switch (editQuestionViewModel.Format)
                {
                    case QuestionFormat.Text:
                    {
                        questionFieldType = FieldType.TextBox;
                        break;
                    }
                    case QuestionFormat.ChoiceAcross:
                    {
                        questionFieldType = FieldType.GroupBox;
                        break;
                    }
                    case QuestionFormat.ChoiceDown:
                    {
                        questionFieldType = FieldType.GroupBox;
                        break;
                    }
                    case QuestionFormat.DropDown:
                    {
                        questionFieldType = FieldType.DropdownList;
                        break;
                    }
                    case QuestionFormat.Matrix:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    case QuestionFormat.Slider:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                    default:
                    {
                        throw new ArgumentOutOfRangeException();
                    }
                }

                var questionSurveyFieldType = session.Query<SurveyFieldType>()
                    .First(sft => sft.FieldType == questionFieldType);
                question.SurveyFieldType = questionSurveyFieldType;

                if (editQuestionViewModel.Format == QuestionFormat.ChoiceAcross ||
                    editQuestionViewModel.Format == QuestionFormat.ChoiceDown)
                {
                    var choiceFieldType = editQuestionViewModel.AllowMultipleValues
                        ? FieldType.Checkbox
                        : FieldType.RadioButton;

                    var choiceSurveyFieldType = session.Query<SurveyFieldType>()
                        .First(sft => sft.FieldType == choiceFieldType);

                    foreach (var answerChoiceItemViewModel in editQuestionViewModel.AnswerChoiceItemViewModels)
                    {
                        var choice = new SurveyField
                        {
                            Label = answerChoiceItemViewModel.Text,
                            FieldIndex = 1,
                            ParentPage = firstPage,
                            Group = question,
                            SurveyFieldType = choiceSurveyFieldType
                        };

                        question.Choices.Add(choice);
                    }
                }

                session.Save(question);

                transaction.Commit();

                return question;
            }
        }
    }
}
