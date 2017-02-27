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

namespace Web.Controllers
{
    [Authorize]
    public class PreviewController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "LinCollect";

        public ActionResult Index(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var survey = db.Surveys.Find(id);
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
                var file = db.SurveyFiles.Find(fileId);

                if (file == null)
                    model.Companies.Add(new Companies() { RelationshipId = item.Id, Names = new List<string>() });
                else
                {
                    var error = "";
                    var _companies = new Companies()
                    {
                        RelationshipId = item.Id,
                        RelationshipName = item.Name,
                        Names = GetCompanies(file.Link, ref error),
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

        private List<string> GetCompanies(string fileId, ref string error)
        {
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = GetServiceCredentials(),
                ApplicationName = ApplicationName,
            });

            try
            {
                var buff = service.Spreadsheets.Get(fileId);
                buff.IncludeGridData = true;
                var copy = buff.Execute();

                return copy.Sheets.Skip(1).FirstOrDefault().Data.FirstOrDefault()
                    .RowData.Select(x => x.Values.FirstOrDefault().FormattedValue).Skip(1).ToList();
            }
            catch (Exception ex)
            {
                error = "Not have access to the file '{0}' or is not 'Node List' sheet. Open <a target='_blank' href='https://docs.google.com/spreadsheets/d/{1}'>THIS LINK</a> to fix problem.";
                return new List<string>();
            }
        }

        public static ServiceAccountCredential GetServiceCredentials()
        {
            ServiceAccountCredential credential;
            string credentialPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content");
            credentialPath = Path.Combine(credentialPath, "LinCollect-7a500f9e0d6a.p12");

            var certificate = new X509Certificate2(credentialPath, "notasecret", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
            credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer("linc-809@winter-sensor-149718.iam.gserviceaccount.com")
            {
                Scopes = Scopes
            }.FromCertificate(certificate));

            return credential;
        }
    }
}