using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Web.Data;
using Web.Models;
using Web.Models.ViewModels;

namespace Web.Controllers
{
//    Client ID
//219448834024-bvltvn8onr69r1mpe8n686an4hekq2a4.apps.googleusercontent.com

    [Authorize]
    public class SurveysController : Controller
    {
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "LinCollect";

        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Text()
        {
            UserCredential credential;

            using (var stream =
                new FileStream(Server.MapPath("~") + "Content\\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String spreadsheetId = "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms";
            String range = "Class Data!A2:E";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                Console.WriteLine("Name, Major");
                foreach (var row in values)
                {
                    // Print columns A and E, which correspond to indices 0 and 4.
                    Console.WriteLine("{0}, {1}", row[0], row[4]);
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
            Console.Read();


            return View();
        }

        public async Task<ActionResult> Index()
        {
            var surveyViews = db.Surveys.Select(x => new SurveyView()
            {
                Id = x.Id,
                Name = x.Name,
                Language = x.Language.Name,
                Status = x.Status.Name,
                SurveyStatusId = x.SurveyStatusId,
            });
            return View(await surveyViews.ToListAsync());
        }

        public ActionResult Create()
        {
            var model = new Survey();
            ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Name");
            ViewBag.SurveyFileId = new SelectList(db.SurveyFiles, "Id", "Name");
            model.UserId = User.Identity.GetUserId();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(Survey survey, bool isNext = false)
        {
            if (ModelState.IsValid)
            {
                survey.CreateDateUtc = survey.UpdateDateUtc = DateTime.UtcNow;
                survey.SurveyStatusId = 1;
                db.Surveys.Add(survey);
                await db.SaveChangesAsync();
                if (isNext)
                {
                    return RedirectToAction("Respondent", new { id = survey.Id });
                }
                else
                {
                    return RedirectToAction("Edit", new { id = survey.Id });
                }
            }
            ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Name", survey.LanguageId);
            ViewBag.SurveyFileId = new SelectList(db.SurveyFiles, "Id", "Name", survey.SurveyFileId);
            return View(survey);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var surveyView = await db.Surveys.FindAsync(id);
            if (surveyView == null)
            {
                return HttpNotFound();
            }
            ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Name", surveyView.LanguageId);
            ViewBag.SurveyFileId = new SelectList(db.SurveyFiles, "Id", "Name", surveyView.SurveyFileId);
            return View(surveyView);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(Survey survey, bool isNext = false)
        {
            if (ModelState.IsValid)
            {
                survey.UpdateDateUtc = DateTime.UtcNow;
                db.Entry(survey).State = EntityState.Modified;
                await db.SaveChangesAsync();
                if (isNext)
                {
                    return RedirectToAction("Respondent", new { id = survey.Id });
                }
            }
            ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Name", survey.LanguageId);
            ViewBag.SurveyFileId = new SelectList(db.SurveyFiles, "Id", "Name", survey.SurveyFileId);
            return View(survey);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var surveyView = await db.Surveys.FindAsync(id);
            if (surveyView == null)
            {
                return HttpNotFound();
            }
            return View(surveyView);
        }

        public async Task<ActionResult> Respondent(int? id, bool isAfter = true)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var survey = await db.Surveys.FindAsync(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            if (survey.Respondents.FirstOrDefault(x => x.IsAfterSurvey == isAfter) == null)
            {
                var resp = new Respondent()
                {
                    SurveyId = id.Value,
                    CreateDateUtc = DateTime.UtcNow,
                    UpdateDateUtc = DateTime.UtcNow,
                    IsAfterSurvey = isAfter
                };
                db.Respondents.Add(resp);
                await db.SaveChangesAsync();
                survey.Respondents.Add(resp);
            }
            db.Entry(survey).State = EntityState.Modified;
            ViewBag.Formats = db.QuestionFormats.ToList();
            ViewBag.IsAfter = isAfter;
            return View(survey.Respondents.FirstOrDefault(x => x.IsAfterSurvey == isAfter));
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> Respondent(Question question)
        {
            var Answers = new List<Answer>();
            var code = Request.Form["FormatCode"];
            if (code == null)
                return Json(new { success = false });
            question.CreateDateUtc = question.UpdateDateUtc = DateTime.UtcNow;
            question.QuestionFormatId = db.QuestionFormats.FirstOrDefault(x => x.Code == code)?.Id ?? 1;
            //[8]: "Answer.Rows"
            //[9]: "Answer.Text"
            //[10]: "Answer.Value"
            if ("choice_across,choice_down,drop_down,matrix".Contains(code) && Request.Form["Answer.Text"] != null)
            {
                var _text = Request.Form["Answer.Text"].Split(',');
                var _val = Request.Form["Answer.Value"].Split(',');

                if (code == "matrix" && Request.Form["Answer.Rows"] != null)
                    question.Rows = string.Join(",", Request.Form["Answer.Rows"].Split(',').Where(x => x.Length > 0).ToList());

                for (int i = 0; i < _text.Length; i++)
                {
                    if (_text[i].Length > 0)

                        Answers.Add(new Answer()
                        {
                            QuestionId = question.Id,
                            CreateDateUtc = DateTime.UtcNow,
                            UpdateDateUtc = DateTime.UtcNow,
                            Text = _text[i],
                            Value = _val[i],
                            IsDefault = false,
                            OrderId = i
                        });
                }
            }
            try
            {
                var orderId = db.Question.Where(x => x.RespondentId == question.RespondentId).Count() + 1;
                if (question.OrderId == 0)
                    question.OrderId = orderId;

                if (question.Id == 0)
                {
                    question.Answers = Answers;
                    db.Question.Add(question);
                }
                else
                {
                    var answers = db.Answers.Where(x => x.QuestionId == question.Id);
                    db.Answers.RemoveRange(answers);
                    db.SaveChanges();
                    db.Entry(question).State = EntityState.Modified;
                    db.SaveChanges();
                    db.Answers.AddRange(Answers);
                }

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<JsonResult> SetRespondentPosition(int id, bool value)
        {
            var resp = db.Respondents.SingleOrDefault(x => x.Id == id);
            if (resp == null)
            {
                return Json(new { success = false });
            }
            resp.IsAfterSurvey = value;
            await db.SaveChangesAsync();

            return Json(new { success = true });
        }

        public ActionResult EditQuestion(int id, int respId, bool isAfter = true)
        {
            var model = db.Question.Find(id);
            var _answers = new List<Answer>();
            for (int i = 0; i < 5; i++)
            {
                _answers.Add(new Answer()
                {
                    OrderId = i + 1,
                    Text = "",
                    Value = "",
                });
            }
            if (model == null)
            {
                model = new Question()
                {
                    TextRowsCount = 1,
                    IsMultiple = false,
                    IsAnnotation = false,
                    IsCompulsory = false,
                    IsShowValue = false,
                    OrderId = 0,
                    TextMin = "Strongly Agree",
                    TextMax = "Strongly Disagree",
                    ValueMin = "0",
                    ValueMax = "5",
                    Rows = ",,,,",
                    Answers = _answers,
                    RespondentId = respId,
                    IsAfterSurvey = isAfter
                };
            }
            if (string.IsNullOrEmpty(model.Rows))
                model.Rows = ",,,,";

            if (model.Answers.Count == 0)
                model.Answers = _answers;

            ViewBag.Formats = db.QuestionFormats.ToList();
            return PartialView(model);
        }

        public ActionResult EditRQuestion(int id, int relId, bool isAfter = true)
        {
            var model = db.RQuestions.Find(id);
            var _answers = new List<RAnswer>();
            for (int i = 0; i < 5; i++)
            {
                _answers.Add(new RAnswer()
                {
                    OrderId = i + 1,
                    Text = "",
                    Value = "",
                });
            }
            if (model == null)
            {
                model = new RQuestion()
                {
                    TextRowsCount = 1,
                    IsMultiple = false,
                    IsAnnotation = false,
                    IsCompulsory = false,
                    IsShowValue = false,
                    OrderId = 0,
                    TextMin = "Strongly Agree",
                    TextMax = "Strongly Disagree",
                    ValueMin = "0",
                    ValueMax = "5",
                    Rows = ",,,,",
                    Answers = _answers,
                    RelationshipItemId = relId,
                    IsAfterSurvey = isAfter
                };
            }
            if (string.IsNullOrEmpty(model.Rows))
                model.Rows = ",,,,";

            if (model.Answers.Count == 0)
                model.Answers = _answers;

            ViewBag.Formats = db.QuestionFormats.ToList();
            return PartialView(model);
        }


        public async Task<ActionResult> DeleteQuestion(int id)
        {
            try
            {
                var _Question = await db.Question.FindAsync(id);
                var _RespId = _Question.RespondentId;
                db.Question.Remove(_Question);
                await db.SaveChangesAsync();

                db.Database.ExecuteSqlCommand("exec SortRespondentQuestions {0}", _RespId);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }


        }

        public async Task<ActionResult> DeleteRQuestion(int id)
        {
            try
            {
                var _Question = await db.RQuestions.FindAsync(id);
                var _RelId = _Question.RelationshipItemId;
                db.RQuestions.Remove(_Question);
                await db.SaveChangesAsync();

                db.Database.ExecuteSqlCommand("exec SortRelationQuestions {0}", _RelId);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }


        }

        public async Task<ActionResult> ChangeQuestionPosition(int id, bool isInc)
        {
            try
            {
                var _q = await db.Question.FindAsync(id);
                var count = db.Question.Count(x => x.RespondentId == _q.RespondentId);
                if (isInc)
                {
                    if (_q.OrderId < count)
                    {
                        _q.OrderId += 1;

                        var buff = db.Question.FirstOrDefault(x => x.RespondentId == _q.RespondentId && x.OrderId == _q.OrderId);
                        if (buff != null)
                            buff.OrderId -= 1;
                    }

                }
                else
                {
                    if (_q.OrderId > 1)
                    {
                        _q.OrderId -= 1;

                        var buff = db.Question.FirstOrDefault(x => x.RespondentId == _q.RespondentId && x.OrderId == _q.OrderId);
                        if (buff != null)
                            buff.OrderId += 1;
                    }
                }

                await db.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        public async Task<ActionResult> ChangeRQuestionPosition(int id, bool isInc)
        {
            try
            {
                var _q = await db.RQuestions.FindAsync(id);
                var count = db.RQuestions.Count(x => x.RelationshipItemId == _q.RelationshipItemId);
                if (isInc)
                {
                    if (_q.OrderId < count)
                    {
                        _q.OrderId += 1;

                        var buff = db.RQuestions.FirstOrDefault(x => x.RelationshipItemId == _q.RelationshipItemId && x.OrderId == _q.OrderId);
                        if (buff != null)
                            buff.OrderId -= 1;
                    }

                }
                else
                {
                    if (_q.OrderId > 1)
                    {
                        _q.OrderId -= 1;

                        var buff = db.RQuestions.FirstOrDefault(x => x.RelationshipItemId == _q.RelationshipItemId && x.OrderId == _q.OrderId);
                        if (buff != null)
                            buff.OrderId += 1;
                    }
                }

                await db.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public async Task<ActionResult> Relationship(int? id, bool questions = false, int? relId = null)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var survey = await db.Surveys.FindAsync(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            if (survey.RelationshipItems == null)
                survey.RelationshipItems = new List<RelationshipItem>();

            ViewBag.SurveyId = id.Value;

            var model = new RelationshipView();
            model.RelationshipItems = survey.RelationshipItems.ToList();
            if (model.RelationshipItems.Count() == 0)
            {
                var rel = new RelationshipItem()
                {
                    Name = "New Relationship",
                    MaximumNodes = 0,
                    QuestionLayoutId = 1,
                    NodeSelectionId = 1,
                    SurveyId = id.Value,
                    CreateDateUtc = DateTime.UtcNow,
                    UpdateDateUtc = DateTime.UtcNow,
                    OrderId = 1
                };
                db.RelationshipItems.Add(rel);
                db.SaveChanges();
            }
            if (!relId.HasValue)
                model.SelectedItem = model.RelationshipItems.OrderBy(x => x.OrderId).FirstOrDefault();
            else
                model.SelectedItem = model.RelationshipItems.FirstOrDefault(x => x.Id == relId.Value);

            ViewBag.QuestionLayoutId = new SelectList(db.QuestionLayouts, "Id", "Name", model.SelectedItem?.QuestionLayoutId);
            ViewBag.NodeSelectionId = new SelectList(db.NodeSelections, "Id", "Name", model.SelectedItem?.NodeSelectionId);
            ViewBag.IsQuestion = questions;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddRelationsip(int id)
        {
            try
            {
                var count = db.RelationshipItems.Count(x => x.SurveyId == id);
                var rel = new RelationshipItem()
                {
                    Name = "New Relationship",
                    MaximumNodes = 0,
                    QuestionLayoutId = 1,
                    NodeSelectionId = 1,
                    SurveyId = id,
                    CreateDateUtc = DateTime.UtcNow,
                    UpdateDateUtc = DateTime.UtcNow,
                    OrderId = count + 1
                };
                db.RelationshipItems.Add(rel);
                db.SaveChanges();

                return Json(new { success = true, id = id, relId = rel.Id });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
            
        }
        [HttpPost]
        public async Task<ActionResult> DeleteRelationsip(int id)
        {
            try
            {
                var r = db.RelationshipItems.Find(id);
                var sId = r.SurveyId;
                if (db.RelationshipItems.Count(x => x.SurveyId == sId) <= 1)
                    return Json(new { success = false, error = "You cann't delete the last relationship" });

                var q = db.RQuestions.Where(x => x.RelationshipItemId == id);
                db.RQuestions.RemoveRange(q);
                db.SaveChanges();
                db.RelationshipItems.Remove(r);
                db.SaveChanges();

                db.Database.ExecuteSqlCommand("exec SortRelationship {0}", sId);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        public async Task<ActionResult> ChangeRelationshipPosition(int id, bool isInc)
        {
            try
            {
                var _q = await db.RelationshipItems.FindAsync(id);
                var count = db.RelationshipItems.Count(x => x.SurveyId == _q.SurveyId);
                if (isInc)
                {
                    if (_q.OrderId < count)
                    {
                        _q.OrderId += 1;

                        var buff = db.RelationshipItems.FirstOrDefault(x => x.SurveyId == _q.SurveyId && x.OrderId == _q.OrderId);
                        if (buff != null)
                            buff.OrderId -= 1;
                    }
                }
                else
                {
                    if (_q.OrderId > 1)
                    {
                        _q.OrderId -= 1;

                        var buff = db.RelationshipItems.FirstOrDefault(x => x.SurveyId == _q.SurveyId && x.OrderId == _q.OrderId);
                        if (buff != null)
                            buff.OrderId += 1;
                    }
                }

                await db.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> Relationship(RQuestion question)
        {
            var Answers = new List<RAnswer>();
            var code = Request.Form["FormatCode"];
            if (code == null)
                return Json(new { success = false });
            question.CreateDateUtc = question.UpdateDateUtc = DateTime.UtcNow;
            question.QuestionFormatId = db.QuestionFormats.FirstOrDefault(x => x.Code == code)?.Id ?? 1;
            //[8]: "Answer.Rows"
            //[9]: "Answer.Text"
            //[10]: "Answer.Value"
            if ("choice_across,choice_down,drop_down,matrix".Contains(code) && Request.Form["Answer.Text"] != null)
            {
                var _text = Request.Form["Answer.Text"].Split(',');
                var _val = Request.Form["Answer.Value"].Split(',');

                if (code == "matrix" && Request.Form["Answer.Rows"] != null)
                    question.Rows = string.Join(",", Request.Form["Answer.Rows"].Split(',').Where(x => x.Length > 0).ToList());

                for (int i = 0; i < _text.Length; i++)
                {
                    if (_text[i].Length > 0)
                        Answers.Add(new RAnswer()
                        {
                            RQuestionId = question.Id,
                            CreateDateUtc = DateTime.UtcNow,
                            UpdateDateUtc = DateTime.UtcNow,
                            Text = _text[i],
                            Value = _val[i],
                            IsDefault = false,
                            OrderId = i
                        });
                }
            }
            try
            {
                var orderId = db.RQuestions.Where(x => x.RelationshipItemId == question.RelationshipItemId).Count() + 1;
                if (question.OrderId == 0)
                    question.OrderId = orderId;

                if (question.Id == 0)
                {
                    question.Answers = Answers;
                    db.RQuestions.Add(question);
                }
                else
                {
                    var answers = db.RAnswers.Where(x => x.RQuestionId == question.Id);
                    db.RAnswers.RemoveRange(answers);
                    db.SaveChanges();
                    db.Entry(question).State = EntityState.Modified;
                    db.SaveChanges();
                    db.RAnswers.AddRange(Answers);
                }

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        public async Task<ActionResult> GetRelationshipItem(int id)
        {
            var item = await db.RelationshipItems.FindAsync(id);
            ViewBag.QuestionLayoutId = new SelectList(db.QuestionLayouts, "Id", "Name", item.QuestionLayoutId);
            ViewBag.NodeSelectionId = new SelectList(db.NodeSelections, "Id", "Name", item.NodeSelectionId);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> GetRelationshipItem(RelationshipItem item)
        {
            int _id = int.Parse(Request.Form["RelId"]);
            var buff = db.RelationshipItems.FirstOrDefault(x => x.Id == _id);

            buff.Name = item.Name;
            buff.UpdateDateUtc = DateTime.UtcNow;
            buff.NodeList = item.NodeList;
            buff.QuestionLayoutId = item.QuestionLayoutId;
            buff.MaximumNodes = item.MaximumNodes;
            buff.AddNodes = item.AddNodes;
            buff.HideAddedNodes = item.HideAddedNodes;
            buff.AllowSelectAllNodes = item.AllowSelectAllNodes;
            buff.CanSkip = item.CanSkip;
            buff.UseDDSearch = item.UseDDSearch;
            buff.SuperUserViewNodes = item.SuperUserViewNodes;
            buff.NodeSelectionId = item.NodeSelectionId;
            buff.GeneratorName = item.GeneratorName;
            buff.SortNodeList = item.SortNodeList;

            await db.SaveChangesAsync();

            if (bool.Parse(Request.Form["isBack"]))
                return RedirectToAction("Respondent", new { id = buff.SurveyId });

            return RedirectToAction("Relationship", new { id = buff.SurveyId, questions = false, relId = buff.Id });
        }
    }
}
