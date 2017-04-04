using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class SheetController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public SheetController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static string ApplicationName = "LinCollect";

        public async Task<ActionResult> EditForm(int id)
        {
            var file = await _dbContext.SurveyFiles.FindAsync(id);
            if (file != null)
            {
                file.Link = $"https://docs.google.com/spreadsheets/d/{file.Link}/edit#gid=0";
                return PartialView(file);
            }
            return HttpNotFound();
        }

        public ActionResult MailingList()
        {
            var userId = User.Identity.GetUserId();
            var files = _dbContext.SurveyFiles.ToList();
            return PartialView(files);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> UserFile(string link, string name)
        {
            if (string.IsNullOrEmpty(name))
                return Json(new { success = false, Error = "Enter file name" });

            var file = _dbContext.SurveyFiles.Create();

            try
            {
                file.UserId = User.Identity.GetUserId();
                file.Link = ParseLink(link);
                file.Name = name;
                _dbContext.SurveyFiles.Add(file);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    return Json(new { success = false, Error = ex.InnerException.Message + ex.Message });
                }
                return Json(new { success = false, Error = ex.Message });
            }
            return Json(new { success = true, Data = file });
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> UpdateMailingList(int id, string link, string name)
        {
            if (id < 1 || link == null || name == null)
            {
                return Json(new { sucess = false, error = "Data is not valid" });
            }

            SurveyFile file = null;

            try
            {
                file = await _dbContext.SurveyFiles.FindAsync(id);
                file.Link = ParseLink(link);
                file.Name = name;
                _dbContext.Entry(file).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Json(new { sucess = false, Error = e.Message });
            }

            return Json(new { sucess = true, data = file });
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteMailingList(int id)
        {
            try
            {
                var file = _dbContext.SurveyFiles.Find(id);
                _dbContext.SurveyFiles.Remove(file);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Error = "You can't delete this file. Some of survey use it!" });
            }

            return Json(new { success = true, id = id });
        }

        private string CopyFile(string name)
        {
            UserCredential credential;
            //var folder = System.Web.HttpContext.Current.Server.MapPath("/Content/files");
            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets()
            {
                ClientId = "555419438916-u5dlvt62e547tkpfjjb5icc3fcq2gd6c.apps.googleusercontent.com",
                ClientSecret = "VQdxwTPZVde30WgAbFWRAn8y"
            },
            Scopes,
            Environment.UserName,
            CancellationToken.None).Result;

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var buff = service.Spreadsheets.Get("1be0tl-xqieSdw1sRmF8UxxAF49lBqzwGkE2GSDX50aU");
            buff.IncludeGridData = true;
            var copy = buff.Execute();
            copy.Properties.Title = name;

            var newFile = service.Spreadsheets.Create(new Spreadsheet()
            {
                Sheets = copy.Sheets,
                Properties = copy.Properties,
                ETag = copy.ETag,
                NamedRanges = copy.NamedRanges
            }).Execute();


            return newFile.SpreadsheetId;
        }
        #region helpers

        private string ParseLink(string link)
        {
            return link.Replace("https://docs.google.com/spreadsheets/d/", "").Split('/').FirstOrDefault() ?? "";
        }
        #endregion
    }
}