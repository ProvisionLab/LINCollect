using System;
using System.Linq;
using DynamicSurvey.Core.Entities;
using DynamicSurvey.Core.SessionStorage;
using DynamicSurvey.Server.ViewModels;
using DynamicSurvey.Server.ViewModels.Surveys;
using NHibernate.Linq;

namespace DynamicSurvey.Server.Services
{
    public class SurveyService
    {
        public const int DefaultRows = 1;

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

            if (questionAction == QuestionAction.Add)
            {
                editRespondentViewModel.EditQuestionViewModel.Rows = DefaultRows;
            }

            /*
            if (questionAction != null)
            {
                editRespondentViewModel.EditQuestionViewModel = new EditQuestionViewModel();
                if (questionAction.Value == QuestionAction.Add)
                {
                    editRespondentViewModel.EditQuestionViewModel.Rows = DefaultRows;
                }
            }
             */

            var session = PersistenceContext.GetCurrentSession();
            using (var transaction = session.BeginTransaction())
            {

            }

            return editRespondentViewModel;
        }
    }
}
