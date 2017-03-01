using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using Web.Models;
using Web.Models.ViewModels;
using Web.Services.Implementations;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    [Authorize]
    public class PreviewController : Controller
    {
        private readonly IGoogleSheetsService _googleSheetsService;
        private readonly ApplicationDbContext _dbContext;
        public PreviewController(IGoogleSheetsService googleSheetsService, ApplicationDbContext dbContext)
        {
            _googleSheetsService = googleSheetsService;
            _dbContext = dbContext;
        }

        public ActionResult Index(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var survey = _dbContext.Surveys.Find(id);
            if (survey == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var model = new PreviewView();
            model.SurveyId = survey.Id;
            model.SurveyName = survey.Name;
            model.IntroductionText = survey.Introduction;
            model.ThanksText = survey.Thanks;
            model.Banner = survey.Banner;

            model.AboutYouBefore = survey.Respondents.FirstOrDefault(x => !x.IsAfterSurvey);
            model.AboutYouAfter = survey.Respondents.FirstOrDefault(x => x.IsAfterSurvey);

            model.Items = survey.RelationshipItems?.OrderBy(x => x.OrderId).ToList();
            model.Companies = new List<Companies>();

            foreach (var item in model.Items)
            {
                var fileId = int.Parse(item.NodeList);
                var file = _dbContext.SurveyFiles.Find(fileId);

                if (file == null)
                    model.Companies.Add(new Companies() { RelationshipId = item.Id, Names = new List<string>() });
                else
                {
                    var error = "";
                    var _companies = new Companies()
                    {
                        RelationshipId = item.Id,
                        RelationshipName = item.Name,
                        Names = _googleSheetsService.GetCompanies(file.Link, ref error),
                        Error = !String.IsNullOrEmpty(error) ? String.Format(error, file.Name, file.Link) : String.Empty
                    };
                    if (item.SortNodeList)
                        _companies.Names = _companies.Names.OrderBy(x => x).ToList();

                    model.Companies.Add(_companies);
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int? id, FormCollection col)
        {
            return RedirectToAction("Index");
        }
    }
}