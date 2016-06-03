using System.Web.Mvc;
using DynamicSurvey.Server.ViewModels.Surveys;

namespace DynamicSurvey.Server.Services
{
    public class SurveyValidationService
    {
        public bool ValidateEditSurveyViewModel(EditSurveyViewModel editSurveyViewModel, ModelStateDictionary modelState)
        {
            if (string.IsNullOrWhiteSpace(editSurveyViewModel.TemplateName))
            {
                modelState.AddModelError("TemplateName", "Please enter text");
            }

            return modelState.IsValid;
        }
    }
}
