﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Data;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class SheetController : Controller
    {
        static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static string ApplicationName = "LinCollect";

        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> MailingList()
        {
            var userId = User.Identity.GetUserId();
            var files = db.SurveyFiles.Where(x => x.UserId == userId).ToList();
            return PartialView(files);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> MailingList(string Name)
        {
            if (string.IsNullOrEmpty(Name))
                return Json(new { success = false, Error = "Enter file name" });

            var file = db.SurveyFiles.Create();
            try
            {
                file.UserId = User.Identity.GetUserId();
                file.Link = CopyFile(Name);
                file.Name = Name;
                db.SurveyFiles.Add(file);
                await db.SaveChangesAsync();

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
        public async Task<JsonResult> UserFile(string link, string name)
        {
            if(string.IsNullOrEmpty(name))
                return Json(new { success = false, Error = "Enter file name" });

            var file = db.SurveyFiles.Create();
            var _link = link.Replace("https://docs.google.com/spreadsheets/d/", "").Split('/').FirstOrDefault() ?? "";
            try
            {
                file.UserId = User.Identity.GetUserId();
                file.Link = _link;
                file.Name = name;
                db.SurveyFiles.Add(file);
                await db.SaveChangesAsync();

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
        public async Task<JsonResult> DeleteMailingList(int id)
        {
            try
            {
                var file = db.SurveyFiles.Find(id);
                db.SurveyFiles.Remove(file);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Error = ex.Message });
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
    }
}