using System.Web.Mvc;

namespace DynamicSurvey.Server.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}
	}
}
