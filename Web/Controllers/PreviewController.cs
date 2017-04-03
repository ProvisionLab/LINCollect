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
using AutoMapper;
using Web.Models;
using Web.Models.DTO;
using Web.Models.ViewModels;
using Web.Services.Implementations;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    [Authorize(Roles = "Administrator")]
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

            model.AboutYouBefore = Mapper.Map<RespondentModel>(survey.Respondents.FirstOrDefault(x => !x.IsAfterSurvey));
            model.AboutYouAfter = Mapper.Map<RespondentModel>(survey.Respondents.FirstOrDefault(x => x.IsAfterSurvey));

            model.Items = Mapper.Map<List<RelationshipItemModel>>(survey.RelationshipItems?.OrderBy(x => x.OrderId).ToList());

            foreach (var item in model.Items)
            {
                var fileId = int.Parse(item.NodeList);
                var file = _dbContext.SurveyFiles.Find(fileId);

                if (file == null)
                {
                    item.Companies = new Companies { RelationshipId = item.Id, Names = new List<CompanyItem>() };
                }
                else
                {
                    var companies = new List<CompanyItem>();
                    var error = "";

                    foreach (var companyName in _googleSheetsService.GetCompanies(file.Link, ref error))
                    {
                        companies.Add(new CompanyItem { Name = companyName, Checked = false });
                    }
                    var _companies = new Companies
                    {
                        RelationshipId = item.Id,
                        RelationshipName = item.Name,
                        Names = companies,
                        Error = !string.IsNullOrEmpty(error) ? string.Format(error, file.Name, file.Link) : string.Empty
                    };
                    if (item.SortNodeList)
                        _companies.Names = _companies.Names.OrderBy(x => x.Name).ToList();

                    item.Companies = _companies;
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