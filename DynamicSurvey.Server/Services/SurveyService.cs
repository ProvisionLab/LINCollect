﻿using System;
using System.Collections.Generic;
using System.Linq;
using DynamicSurvey.Core;
using DynamicSurvey.Core.Entities;
using DynamicSurvey.Core.SessionStorage;
using DynamicSurvey.Server.Models.Surveys;
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

        public IEnumerable<LanguageItemViewModel> GetLanguages()
        {
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

                transaction.Commit();

                return languages;
            }
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

        public EditRespondentViewModel GetEditRespondentViewModel(EditRespondentRequestModel request)
        {
            if (request.SurveyTemplateId == null)
            {
                throw new NullReferenceException("SurveyTemplateId can not be null.");
            }

            var editRespondentViewModel = new EditRespondentViewModel
            {
                SurveyTemplateId = request.SurveyTemplateId.Value,
                QuestionAction = request.QuestionAction
            };

            var editQuestionViewModel = new EditQuestionViewModel
            {
                SurveyTemplateId = request.SurveyTemplateId.Value,
                InsertPosition = request.InsertPosition
            };

            editRespondentViewModel.EditQuestionViewModel = editQuestionViewModel;

            var session = PersistenceContext.GetCurrentSession();
            using (var transaction = session.BeginTransaction())
            {
                if (request.QuestionAction == QuestionAction.Edit)
                {
                    var question = session.Get<SurveyField>(request.QuestionId);

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

                        foreach (var choice in question.Choices.OrderBy(c => c.DisplayOrder))
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
                        foreach (var dropDownListItem in
                            question.SurveyFieldVocabularyCrossList.OrderBy(sfvcl => sfvcl.DisplayOrder))
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

                var questions = session.Query<SurveyField>()
                    .Where(q => q.ParentPage.SurveyTemplate.Id == request.SurveyTemplateId.Value)
                    .Where(q => q.Group == null)
                    .Fetch(q => q.Choices)
                    .Fetch(q => q.SurveyFieldVocabularyCrossList)
                    .OrderBy(q => q.DisplayOrder)
                    .ToList();

                foreach (var question in questions)
                {
                    var questionItemViewModel = new QuestionItemViewModel
                    {
                        QuestionId = question.Id,
                        Question = question.Label,
                        DisplayOrder = question.DisplayOrder,
                        Format = GetQuestionFormatFromFieldType(question.SurveyFieldType.FieldType)
                    };

                    if (question.SurveyFieldType.FieldType == FieldType.GroupBox)
                    {
                        if (question.Choices.Any(c => c.SurveyFieldType.FieldType == FieldType.Checkbox))
                        {
                            questionItemViewModel.AllowMultipleValues = true;
                        }
                        else if (question.Choices.Any(c => c.SurveyFieldType.FieldType == FieldType.RadioButton))
                        {
                            questionItemViewModel.AllowMultipleValues = false;
                        }

                        foreach (var choice in question.Choices.OrderBy(c => c.DisplayOrder))
                        {
                            var questionChoiceItemViewModel = new QuestionChoiceItemViewModel
                            {
                                Text = choice.Label
                            };

                            if (choice.SurveyFieldVocabularyCrossList.Any(sfvcl => sfvcl.SurveyField == choice))
                            {
                                questionChoiceItemViewModel.IsDefault = true;
                            }

                            questionItemViewModel.AnswerChoices.Add(questionChoiceItemViewModel);
                        }
                    }
                    else if (question.SurveyFieldType.FieldType == FieldType.DropdownList)
                    {
                        foreach (var dropDownListItem in 
                            question.SurveyFieldVocabularyCrossList.OrderBy(sfvcl => sfvcl.DisplayOrder))
                        {
                            var questionChoiceItemViewModel = new QuestionChoiceItemViewModel
                            {
                                Text = dropDownListItem.VocabularyWord.Word
                            };

                            // todo: Come up with how to store default value for dropdown

                            questionItemViewModel.AnswerChoices.Add(questionChoiceItemViewModel);
                        }
                    }

                    editRespondentViewModel.Questions.Add(questionItemViewModel);
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

                // Create survey page if not exist
                SurveyPage surveyPage;
                if (surveyTemplate.SurveyPages.Any())
                {
                    surveyPage = surveyTemplate.SurveyPages.First();
                }
                else
                {
                    surveyPage = new SurveyPage
                    {
                        PageIndex = 1,
                        PageTitle = "First Page",
                        SurveyTemplate = surveyTemplate
                    };
                    session.Save(surveyPage);
                }

                int questionDisplayOrder;

                if (editQuestionViewModel.InsertPosition == null)
                {
                    var maxQuestionDisplayOrder = session.Query<SurveyField>()
                        .Where(q => q.ParentPage.SurveyTemplate == surveyTemplate)
                        .Where(q => q.Group == null)
                        .Max(q => (int?) q.DisplayOrder);

                    if (maxQuestionDisplayOrder != null)
                    {
                        questionDisplayOrder = maxQuestionDisplayOrder.Value + 1;
                    }
                    else
                    {
                        questionDisplayOrder = 1;
                    }
                }
                else
                {
                    questionDisplayOrder = editQuestionViewModel.InsertPosition.Value;

                    var questionsWithHigherOrEqualDisplayOrder = session.Query<SurveyField>()
                        .Where(q => q.ParentPage.SurveyTemplate == surveyTemplate)
                        .Where(q => q.Group == null)
                        .Where(q => q.DisplayOrder >= questionDisplayOrder)
                        .ToList();

                    foreach (var quest in questionsWithHigherOrEqualDisplayOrder)
                    {
                        quest.DisplayOrder++;
                        session.Save(quest);
                    }
                }

                var question = new SurveyField
                {
                    Label = editQuestionViewModel.Question,
                    DisplayOrder = questionDisplayOrder,
                    FieldIndex = 1,
                    ParentPage = surveyPage
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

                    var choiceDisplayOrderCounter = 0;

                    foreach (var answerChoiceItemViewModel in editQuestionViewModel.AnswerChoiceItemViewModels)
                    {
                        if (!string.IsNullOrWhiteSpace(answerChoiceItemViewModel.Text))
                        {
                            choiceDisplayOrderCounter++;

                            var choice = new SurveyField
                            {
                                Label = answerChoiceItemViewModel.Text,
                                DisplayOrder = choiceDisplayOrderCounter,
                                FieldIndex = 1,
                                ParentPage = surveyPage,
                                Group = question,
                                SurveyFieldType = choiceSurveyFieldType
                            };

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

                            question.Choices.Add(choice);
                        }
                    }
                }
                else if (editQuestionViewModel.Format == QuestionFormat.DropDown)
                {
                    var dropDownListItemDisplayOrderCounter = 0;

                    foreach (var answerChoiceItemViewModel in editQuestionViewModel.AnswerChoiceItemViewModels)
                    {
                        if (!string.IsNullOrWhiteSpace(answerChoiceItemViewModel.Text))
                        {
                            dropDownListItemDisplayOrderCounter++;

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
                                DisplayOrder = dropDownListItemDisplayOrderCounter,
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

        public void MoveQuestion(int questionId, bool moveUp)
        {
            var session = PersistenceContext.GetCurrentSession();
            using (var transaction = session.BeginTransaction())
            {
                var question = session.Get<SurveyField>(questionId);

                if (moveUp)
                {
                    var upperQuestion = session.Query<SurveyField>()
                        .Where(q => q.ParentPage.SurveyTemplate == question.ParentPage.SurveyTemplate)
                        .Where(q => q.Group == null)
                        .First(q => q.DisplayOrder == question.DisplayOrder - 1);

                    question.DisplayOrder--;
                    upperQuestion.DisplayOrder++;

                    session.Save(question);
                    session.Save(upperQuestion);
                }
                else
                {
                    var lowerQuestion = session.Query<SurveyField>()
                        .Where(q => q.ParentPage.SurveyTemplate == question.ParentPage.SurveyTemplate)
                        .Where(q => q.Group == null)
                        .First(q => q.DisplayOrder == question.DisplayOrder + 1);

                    question.DisplayOrder++;
                    lowerQuestion.DisplayOrder--;

                    session.Save(question);
                    session.Save(lowerQuestion);
                }

                transaction.Commit();
            }
        }

        public SurveyField EditQuestion(EditQuestionViewModel editQuestionViewModel)
        {
            var session = PersistenceContext.GetCurrentSession();
            using (var transaction = session.BeginTransaction())
            {
                var question = session.Get<SurveyField>(editQuestionViewModel.QuestionId);

                question.Label = editQuestionViewModel.Question;

                var questionFieldType = GetFieldTypeFromQuestionFormat(editQuestionViewModel.Format);

                var questionSurveyFieldType = session.Query<SurveyFieldType>()
                    .First(sft => sft.FieldType == questionFieldType);
                question.SurveyFieldType = questionSurveyFieldType;

                if (editQuestionViewModel.Format == QuestionFormat.ChoiceAcross ||
                    editQuestionViewModel.Format == QuestionFormat.ChoiceDown)
                {
                    // Remove old choices
                    question.Choices.Clear();

                    var choiceFieldType = editQuestionViewModel.AllowMultipleValues
                        ? FieldType.Checkbox
                        : FieldType.RadioButton;

                    var choiceSurveyFieldType = session.Query<SurveyFieldType>()
                        .First(sft => sft.FieldType == choiceFieldType);

                    var choiceDisplayOrderCounter = 0;

                    foreach (var answerChoiceItemViewModel in editQuestionViewModel.AnswerChoiceItemViewModels)
                    {
                        if (!string.IsNullOrWhiteSpace(answerChoiceItemViewModel.Text))
                        {
                            choiceDisplayOrderCounter++;

                            var choice = new SurveyField
                            {
                                Label = answerChoiceItemViewModel.Text,
                                DisplayOrder = choiceDisplayOrderCounter,
                                FieldIndex = 1,
                                ParentPage = question.ParentPage,
                                Group = question,
                                SurveyFieldType = choiceSurveyFieldType
                            };

                            if (answerChoiceItemViewModel.IsDefault)
                            {
                                var vocabularyWord = session.Query<Vocabulary>()
                                    .FirstOrDefault(vw => vw.Word == answerChoiceItemViewModel.Text);

                                if (vocabularyWord == null)
                                {
                                    vocabularyWord = new Vocabulary
                                    {
                                        Word = answerChoiceItemViewModel.Text,
                                        UserLanguage = question.ParentPage.SurveyTemplate.UserLanguage
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

                            question.Choices.Add(choice);
                        }
                    }
                }
                else if (editQuestionViewModel.Format == QuestionFormat.DropDown)
                {
                    // Remove old dropdown list items
                    question.SurveyFieldVocabularyCrossList.Clear();

                    var dropDownListItemDisplayOrderCounter = 0;

                    foreach (var answerChoiceItemViewModel in editQuestionViewModel.AnswerChoiceItemViewModels)
                    {
                        if (!string.IsNullOrWhiteSpace(answerChoiceItemViewModel.Text))
                        {
                            dropDownListItemDisplayOrderCounter++;

                            var vocabularyWord = session.Query<Vocabulary>()
                                .FirstOrDefault(vw => vw.Word == answerChoiceItemViewModel.Text);

                            if (vocabularyWord == null)
                            {
                                vocabularyWord = new Vocabulary
                                {
                                    Word = answerChoiceItemViewModel.Text,
                                    UserLanguage = question.ParentPage.SurveyTemplate.UserLanguage
                                };

                                session.Save(vocabularyWord);
                            }

                            var surveyFieldVocabularyCross = new SurveyFieldVocabularyCross
                            {
                                DisplayOrder = dropDownListItemDisplayOrderCounter,
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

        public void DeleteQuestion(int questionId)
        {
            var session = PersistenceContext.GetCurrentSession();
            using (var transaction = session.BeginTransaction())
            {
                var question = session.Get<SurveyField>(questionId);

                var upperQuestions = session.Query<SurveyField>()
                    .Where(q => q.ParentPage.SurveyTemplate == question.ParentPage.SurveyTemplate)
                    .Where(q => q.Group == null)
                    .Where(q => q.DisplayOrder > question.DisplayOrder)
                    .ToList();

                foreach (var upperQuestion in upperQuestions)
                {
                    upperQuestion.DisplayOrder--;
                    session.Save(upperQuestion);
                }

                session.Delete(question);

                transaction.Commit();
            }
        }

        public void DuplicateQuestion(int questionId)
        {
            var session = PersistenceContext.GetCurrentSession();
            using (var transaction = session.BeginTransaction())
            {
                var question = session.Get<SurveyField>(questionId);

                var questionsWithHigherDisplayOrder = session.Query<SurveyField>()
                    .Where(q => q.ParentPage.SurveyTemplate == question.ParentPage.SurveyTemplate)
                    .Where(q => q.Group == null)
                    .Where(q => q.DisplayOrder > question.DisplayOrder)
                    .ToList();

                foreach (var quest in questionsWithHigherDisplayOrder)
                {
                    quest.DisplayOrder++;
                    session.Save(quest);
                }

                var duplicateQuestion = new SurveyField
                {
                    Label = question.Label,
                    DisplayOrder = question.DisplayOrder + 1,
                    FieldIndex = 1,
                    ParentPage = question.ParentPage,
                    SurveyFieldType = question.SurveyFieldType
                };

                foreach (var choice in question.Choices)
                {
                    var duplicateChoice = new SurveyField
                    {
                        Label = choice.Label,
                        DisplayOrder = choice.DisplayOrder,
                        FieldIndex = 1,
                        ParentPage = choice.ParentPage,
                        Group = duplicateQuestion,
                        SurveyFieldType = choice.SurveyFieldType
                    };

                    foreach (var defaultChoice in choice.SurveyFieldVocabularyCrossList)
                    {
                        var duplicateDefaultChoice = new SurveyFieldVocabularyCross
                        {
                            SurveyField = duplicateChoice,
                            VocabularyWord = defaultChoice.VocabularyWord
                        };

                        duplicateChoice.SurveyFieldVocabularyCrossList.Add(duplicateDefaultChoice);
                    }

                    duplicateQuestion.Choices.Add(duplicateChoice);
                }

                foreach (var dropDownListItem in question.SurveyFieldVocabularyCrossList)
                {
                    var duplicateDropDownListItem = new SurveyFieldVocabularyCross
                    {
                        DisplayOrder = dropDownListItem.DisplayOrder,
                        SurveyField = duplicateQuestion,
                        VocabularyWord = dropDownListItem.VocabularyWord
                    };

                    duplicateQuestion.SurveyFieldVocabularyCrossList.Add(duplicateDropDownListItem);
                }

                session.Save(duplicateQuestion);

                transaction.Commit();
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
                    throw new NotImplementedException();
                }
                case QuestionFormat.Slider:
                {
                    throw new NotImplementedException();
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
