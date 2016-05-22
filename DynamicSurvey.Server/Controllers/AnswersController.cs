using System.Web.Mvc;
using DynamicSurvey.Server.DAL.Filters;
using DynamicSurvey.Server.DAL.Repositories;

namespace DynamicSurvey.Server.Controllers
{
    public class AnswersController : Controller
    {
        private readonly IAnswersRepository _answersRepository;

        public AnswersController(IAnswersRepository answersRepository)
        {
            _answersRepository = answersRepository;
        }

        public ActionResult Index(ulong surveyId)
        {
            var reports = _answersRepository.GetReports(new SurveyReportFilter
            {
                SurveyId = surveyId
            });
            return View(reports);
        }
    }
}
