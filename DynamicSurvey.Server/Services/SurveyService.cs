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

        public EditRespondentViewModel GetEditRespondentViewModel(QuestionAction? questionAction, int surveyTemplateId,
            int? questionId)
        {
            var editRespondentViewModel = new EditRespondentViewModel
            {
                SurveyTemplateId = surveyTemplateId,
                QuestionAction = questionAction
            };

            var editQuestionViewModel = new EditQuestionViewModel
            {
                SurveyTemplateId = surveyTemplateId
            };

            editRespondentViewModel.EditQuestionViewModel = editQuestionViewModel;

            var session = PersistenceContext.GetCurrentSession();
            using (var transaction = session.BeginTransaction())
            {
                if (questionAction == QuestionAction.Edit)
                {
                    var question = session.Get<SurveyField>(questionId);

                    editQuestionViewModel.QuestionId = question.Id;
                    editQuestionViewModel.Question = question.Label;
                    editQuestionViewModel.Format = GetQuestionFormatFromFieldType(question.SurveyFieldType.FieldType);

                    if (question.SurveyFieldType.FieldType == FieldType.GroupBox)
                    {
                        if (question.Choices.Any(c => c.SurveyFieldType.FieldType == FieldType.Checkbox))
                        {
                            editQuestionViewModel.AllowMultipleValues = true;
                        }
                        else if (question.Choices.Any(c => c.SurveyFieldType.FieldType == FieldType.RadioButton))
                        {
                            editQuestionViewModel.AllowMultipleValues = false;
                        }

                        foreach (var choice in question.Choices)
                        {
                            var answerChoiceItemViewModel = new AnswerChoiceItemViewModel
                            {
                                Id = choice.Id,
                                Text = choice.Label
                            };

                            if (choice.SurveyFieldVocabularyCrossList.Any(sfvcl => sfvcl.SurveyField == choice))
                            {
                                answerChoiceItemViewModel.IsDefault = true;
                            }

                            editQuestionViewModel.AnswerChoiceItemViewModels.Add(answerChoiceItemViewModel);
                        }
                    }
                    else if (question.SurveyFieldType.FieldType == FieldType.DropdownList)
                    {
                        foreach (var dropDownListItem in question.SurveyFieldVocabularyCrossList)
                        {
                            var answerChoiceItemViewModel = new AnswerChoiceItemViewModel
                            {
                                Id = dropDownListItem.Id,
                                Text = dropDownListItem.VocabularyWord.Word
                            };

                            // todo: Come up with how to store default value for dropdown

                            editQuestionViewModel.AnswerChoiceItemViewModels.Add(answerChoiceItemViewModel);
                        }
                    }
                }

                transaction.Commit();
            }

            // Add empty rows to viewmodel if needed
            var numberOfEmptyChoicesToAdd = EditQuestionViewModel.DefaultAnswerChoiceNumber -
                                            editQuestionViewModel.AnswerChoiceItemViewModels.Count;
            for (var i = 0; i < numberOfEmptyChoicesToAdd; i++)
            {
                editQuestionViewModel.AnswerChoiceItemViewModels.Add(new AnswerChoiceItemViewModel());
            }

            return editRespondentViewModel;
        }

        public SurveyField CreateQuestion(EditQuestionViewModel editQuestionViewModel)
        {
            var session = PersistenceContext.GetCurrentSession();
            using (var transaction = session.BeginTransaction())
            {
                var surveyTemplate = session.Get<SurveyTemplate>(editQuestionViewModel.SurveyTemplateId);
                var firstPage = session.Get<SurveyPage>(1);

                var question = new SurveyField
                {
                    Label = editQuestionViewModel.Question,
                    FieldIndex = 1,
                    ParentPage = firstPage
                };

                var questionFieldType = GetFieldTypeFromQuestionFormat(editQuestionViewModel.Format);

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

                        if (answerChoiceItemViewModel.IsDefault)
                        {
                            var vocabularyWord = session.Query<Vocabulary>()
                                .FirstOrDefault(vw => vw.Word == answerChoiceItemViewModel.Text);

                            if (vocabularyWord == null)
                            {
                                vocabularyWord = new Vocabulary
                                {
                                    Word = answerChoiceItemViewModel.Text,
                                    UserLanguage = surveyTemplate.UserLanguage
                                };

                                session.Save(vocabularyWord);
                            }

                            var surveyFieldVocabularyCross = new SurveyFieldVocabularyCross
                            {
                                SurveyField = choice,
                                VocabularyWord = vocabularyWord
                            };

                            choice.SurveyFieldVocabularyCrossList.Add(surveyFieldVocabularyCross);
                        }
                    }
                }
                else if (editQuestionViewModel.Format == QuestionFormat.DropDown)
                {
                    foreach (var answerChoiceItemViewModel in editQuestionViewModel.AnswerChoiceItemViewModels)
                    {
                        if (!string.IsNullOrWhiteSpace(answerChoiceItemViewModel.Text))
                        {
                            var vocabularyWord = session.Query<Vocabulary>()
                                .FirstOrDefault(vw => vw.Word == answerChoiceItemViewModel.Text);

                            if (vocabularyWord == null)
                            {
                                vocabularyWord = new Vocabulary
                                {
                                    Word = answerChoiceItemViewModel.Text,
                                    UserLanguage = surveyTemplate.UserLanguage
                                };

                                session.Save(vocabularyWord);
                            }

                            var surveyFieldVocabularyCross = new SurveyFieldVocabularyCross
                            {
                                SurveyField = question,
                                VocabularyWord = vocabularyWord
                            };

                            question.SurveyFieldVocabularyCrossList.Add(surveyFieldVocabularyCross);
                        }
                    }
                }

                session.Save(question);

                transaction.Commit();

                return question;
            }
        }

        private static FieldType GetFieldTypeFromQuestionFormat(QuestionFormat questionFormat)
        {
            switch (questionFormat)
            {
                case QuestionFormat.Text:
                {
                    return FieldType.TextBox;
                }
                case QuestionFormat.ChoiceAcross:
                {
                    return FieldType.GroupBox;
                }
                case QuestionFormat.ChoiceDown:
                {
                    return FieldType.GroupBox;
                }
                case QuestionFormat.DropDown:
                {
                    return FieldType.DropdownList;
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
        }

        private static QuestionFormat GetQuestionFormatFromFieldType(FieldType fieldType)
        {
            switch (fieldType)
            {
                case FieldType.TextBox:
                {
                    return QuestionFormat.Text;
                }
                case FieldType.Email:
                {
                    throw new ArgumentOutOfRangeException();
                }
                case FieldType.Checkbox:
                {
                    throw new ArgumentOutOfRangeException();
                }
                case FieldType.Button:
                {
                    throw new ArgumentOutOfRangeException();
                }
                case FieldType.RadioButton:
                {
                    throw new ArgumentOutOfRangeException();
                }
                case FieldType.GroupBox:
                {
                    return QuestionFormat.ChoiceDown;
                }
                case FieldType.DatePicker:
                {
                    throw new ArgumentOutOfRangeException();
                }
                case FieldType.DropdownList:
                {
                    return QuestionFormat.DropDown;
                }
                default:
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
