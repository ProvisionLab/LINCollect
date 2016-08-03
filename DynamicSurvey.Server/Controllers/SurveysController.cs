using System.Linq;
using System.Web.Mvc;
using DynamicSurvey.Core.Entities;
using DynamicSurvey.Server.DAL.Repositories;
using DynamicSurvey.Server.Helpers;
using DynamicSurvey.Server.Models.Surveys;
using DynamicSurvey.Server.Services;
using DynamicSurvey.Server.ViewModels;
using DynamicSurvey.Server.ViewModels.Surveys;
using Survey = DynamicSurvey.Server.DAL.Entities.Survey;
using DynamicSurvey.Server.Models;

namespace DynamicSurvey.Server.Controllers
{
    public class SurveysController : Controller
    {
        private readonly SurveyValidationService _surveyValidationService;
        private readonly SurveyService _surveyService;
        private readonly ISurveysRepository _surveysRepository;

        private const string ViewModelTempDataKey = "ViewModel";
        private const string ModelStateTempDateKey = "ModelState";

        public SurveysController(ISurveysRepository surveysRepository)
        {
            _surveyValidationService = new SurveyValidationService();
            _surveyService = new SurveyService();
            _surveysRepository = surveysRepository;
        }

        public ActionResult Index()
        {
            if (AdminAuth.IsAutorizedFlag)
            {
                var res = _surveysRepository.GetSurveys(Session.GetCurrentUser());
                return View(new SurveysViewModel
                {
                    Surveys = res
                });
            }
            else { return Redirect("/Profile/Login"); }
        }

        public ActionResult CopySurvey(ulong sourceId)
        {
            if (AdminAuth.IsAutorizedFlag)
            {
                var survey = _surveysRepository.GetSurveys(Session.GetCurrentUser()).Single(s => s.Id == sourceId);
                survey.Id = 0;
                survey.Title = survey.Title + "_Copy";
                var id = _surveysRepository.AddSurvey(Session.GetCurrentUser(), survey);
                return View("AddSurvey", id);
            }
            else { return Redirect("/Profile/Login"); }
        }

        public ActionResult AddSurvey(ulong sourceId)
        {
            if (AdminAuth.IsAutorizedFlag)
            {
                var res = _surveysRepository.GetSurveyById(Session.GetCurrentUser(), sourceId);

                if (res == null)
                {
                    return RedirectToAction("Index");
                }
                return View(res);
            }
            else { return Redirect("/Profile/Login"); }
        }

        [HttpPost]
        public ActionResult AddSurvey(Survey survey)
        {
            return View();
        }

        public ActionResult Details(ulong sourceId)
        {
            if (AdminAuth.IsAutorizedFlag)
            {
                var res = _surveysRepository.GetSurveyById(Session.GetCurrentUser(), sourceId);

                if (res == null)
                {
                    return RedirectToAction("Index");
                }
                return View(res);
            }
            else { return Redirect("/Profile/Login"); }
        }

        [HttpPost]
        public ActionResult Details(Survey editedSurvey)
        {
            _surveysRepository.AddSurvey(Session.GetCurrentUser(), editedSurvey);
            return View();
        }

        public ActionResult EditSurvey(int? surveyTemplateId)
        {
            if (AdminAuth.IsAutorizedFlag)
            {
                EditSurveyViewModel editSurveyViewModel;

                if (TempData[ViewModelTempDataKey] != null)
                {
                    editSurveyViewModel = (EditSurveyViewModel)TempData[ViewModelTempDataKey];

                    // Populate ViewModel with languages again
                    editSurveyViewModel.Languages = _surveyService.GetLanguages();

                    var modelState = (ModelStateDictionary)TempData[ModelStateTempDateKey];
                    ModelState.Merge(modelState);

                    // Fill TempData with ViewModel and ModelState in case the page will be refreshed
                    TempData[ViewModelTempDataKey] = editSurveyViewModel;
                    TempData[ModelStateTempDateKey] = ModelState;
                }
                else
                {
                    editSurveyViewModel = _surveyService.GetEditSurveyViewModel(surveyTemplateId);
                }

                return View(editSurveyViewModel);
            }
            else { return Redirect("/Profile/Login"); }
        }

        [HttpPost]
        public ActionResult EditSurvey(EditSurveyViewModel editSurveyViewModel)
        {
            if (!_surveyValidationService.ValidateEditSurveyViewModel(editSurveyViewModel, ModelState))
            {
                TempData[ViewModelTempDataKey] = editSurveyViewModel;
                TempData[ModelStateTempDateKey] = ModelState;

                return RedirectToAction("EditSurvey");
            }

            SurveyTemplate surveyTemplate;

            if (editSurveyViewModel.Id == null)
            {
                surveyTemplate = _surveyService.CreateSurveyTemplate(editSurveyViewModel);
            }
            else
            {
                surveyTemplate = _surveyService.EditSurveyTemplate(editSurveyViewModel);
            }

            // Remove ViewModel and ModelState from TempData
            TempData[ViewModelTempDataKey] = null;
            TempData[ModelStateTempDateKey] = null;

            return RedirectToAction("EditSurvey", new {surveyTemplateId = surveyTemplate.Id});
        }

        public ActionResult EditRespondent(EditRespondentRequestModel request)
        {
            if (AdminAuth.IsAutorizedFlag)
            {
                if (request.SurveyTemplateId == null)
                {
                    return RedirectToAction("EditSurvey");
                }

                var editRespondentViewModel = _surveyService.GetEditRespondentViewModel(request);

                return View(editRespondentViewModel);
            }
            else { return Redirect("/Profile/Login"); }
        }

        [HttpPost]
        public ActionResult EditQuestion(EditQuestionViewModel editQuestionViewModel)
        {
            if (editQuestionViewModel.QuestionId == null)
            {
                _surveyService.CreateQuestion(editQuestionViewModel);
            }
            else
            {
                _surveyService.EditQuestion(editQuestionViewModel);
            }

            return RedirectToAction("EditRespondent", new {surveyTemplateId = editQuestionViewModel.SurveyTemplateId});
        }

        [HttpPost]
        public ActionResult MoveQuestion(int surveyTemplateId, int questionId, bool moveUp)
        {
            _surveyService.MoveQuestion(questionId, moveUp);

            return RedirectToAction("EditRespondent", new {surveyTemplateId});
        }

        [HttpPost]
        public ActionResult DeleteQuestion(int surveyTemplateId, int questionId)
        {
            _surveyService.DeleteQuestion(questionId);

            return RedirectToAction("EditRespondent", new {surveyTemplateId});
        }

        [HttpPost]
        public ActionResult DuplicateQuestion(int surveyTemplateId, int questionId)
        {
            _surveyService.DuplicateQuestion(questionId);

            return RedirectToAction("EditRespondent", new {surveyTemplateId});
        }

        public ActionResult Relationships()
        {
            if (AdminAuth.IsAutorizedFlag)
            {
                return View();
            }
            else { return Redirect("/Profile/Login"); }
        }

        public ActionResult PreviewSurvey()
        {
            if (AdminAuth.IsAutorizedFlag)
            {
                return View();
            }
            else { return Redirect("/Profile/Login"); }
        }

        public ActionResult Start()
        {
            if (AdminAuth.IsAutorizedFlag)
            {
                return View();
            }
            else { return Redirect("/Profile/Login"); }
        }

        public ActionResult AboutYou()
        {
            if (AdminAuth.IsAutorizedFlag)
            {
                return View();
            }
            else { return Redirect("/Profile/Login"); }
        }

        public ActionResult Relation()
        {
            if (AdminAuth.IsAutorizedFlag)
            {
                return View();
            }
            else { return Redirect("/Profile/Login"); }
        }

        public ActionResult Finish()
        {
            if (AdminAuth.IsAutorizedFlag)
            {
                return View();
            }
            else { return Redirect("/Profile/Login"); }
        }
    }
}
