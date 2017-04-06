using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Data;
using Web.Managers.Interfaces;
using Web.Models;
using Web.Models.ViewModels;
using Web.Providers;

namespace Web.Controllers
{
    //    Client ID
    //555419438916-8lg52oq5ufkfotqlqd18qekiasfnijks.apps.googleusercontent.com


    [Authorize(Roles = "Administrator")]
    public class SurveysController : Controller
    {
        //static string[] Scopes = { SheetsService.Scope.Spreadsheets };
        //static string ApplicationName = "LinCollect";

        private readonly ApplicationDbContext _dbContext;
        private readonly IResultManager _resultManager;
        private readonly ISurveyManager _surveyManager;

        public SurveysController(ApplicationDbContext dbContextContext, IResultManager resultManager, ISurveyManager surveyManager)
        {
            _dbContext = dbContextContext;
            _resultManager = resultManager;
            _surveyManager = surveyManager;
        }

        public async Task<ActionResult> Index()
        {
            var surveyViews = _dbContext.Surveys.Select(x => new SurveyView()
            {
                Id = x.Id,
                Name = x.Name,
                Language = x.Language.Name,
                Status = x.Status.Name,
                SurveyStatusId = x.SurveyStatusId,
                UpdateDateUtc = x.UpdateDateUtc
            }).OrderByDescending(x => x.UpdateDateUtc);
            return View(await surveyViews.ToListAsync());
        }

        public ActionResult Create()
        {
            var model = new Survey();
            ViewBag.LanguageId = new SelectList(_dbContext.Languages, "Id", "Name");
            ViewBag.SurveyFileId = new SelectList(_dbContext.SurveyFiles, "Id", "Name");
            model.UserId = User.Identity.GetUserId();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(Survey survey, HttpPostedFileBase BannerFile, bool isNext = false)
        {
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                survey.CreateDateUtc = survey.UpdateDateUtc = DateTime.UtcNow;
                survey.SurveyStatusId = 1;

                if (BannerFile != null)
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/files/banners/");

                    try
                    {
                        var fileName = BannerFile.FileName.Split('\\').LastOrDefault();
                        BannerFile.SaveAs(path + fileName);
                        survey.Banner = fileName;
                    }
                    catch (Exception ex) { var t = ex.Message; }
                }

                _dbContext.Surveys.Add(survey);
                await _dbContext.SaveChangesAsync();
                if (isNext)
                {
                    return RedirectToAction("Respondent", new { id = survey.Id });
                }
                else
                {
                    return RedirectToAction("Edit", new { id = survey.Id });
                }
            }
            ViewBag.LanguageId = new SelectList(_dbContext.Languages, "Id", "Name", survey.LanguageId);
            ViewBag.SurveyFileId = new SelectList(_dbContext.SurveyFiles.Where(x => x.UserId == userId), "Id", "Name", survey.SurveyFileId);
            return View(survey);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            var userId = User.Identity.GetUserId();
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var surveyView = await _dbContext.Surveys.FindAsync(id);
            if (surveyView == null)
            {
                return HttpNotFound();
            }
            ViewBag.LanguageId = new SelectList(_dbContext.Languages, "Id", "Name", surveyView.LanguageId);
            ViewBag.SurveyFileId = new SelectList(_dbContext.SurveyFiles, "Id", "Name", surveyView.SurveyFileId);
            return View(surveyView);
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(Survey survey, HttpPostedFileBase BannerFile, bool isNext = false)
        {
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                if (BannerFile != null)
                {
                    string path = System.Web.HttpContext.Current.Server.MapPath("~/Content/files/banners/");

                    try
                    {
                        //if (System.IO.File.Exists(path + BannerFile.FileName))
                        //    System.IO.File.Delete(path + BannerFile.FileName);
                        var fileName = BannerFile.FileName.Split('\\').LastOrDefault();
                        BannerFile.SaveAs(path + fileName);
                        survey.Banner = fileName;
                    }
                    catch (Exception ex) { var t = ex.Message; }
                }
                survey.UpdateDateUtc = DateTime.UtcNow;
                _dbContext.Entry(survey).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                if (isNext)
                {
                    return RedirectToAction("Respondent", new { id = survey.Id });
                }
            }
            return RedirectToAction("Edit", new { @id = survey.Id });
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var surveyView = await _dbContext.Surveys.FindAsync(id);
            if (surveyView == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                _dbContext.Database.ExecuteSqlCommand("exec DeleteSurvay {0}", id);
            }
            catch { }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Clone(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var survey = await _dbContext.Surveys.FindAsync(id);
            if (survey == null)
            {
                return RedirectToAction("Index");
            }
            try
            {
                var newSurvey = _dbContext.Surveys.Create();

                newSurvey.Name = "Copy of " + survey.Name;
                newSurvey.Banner = survey.Banner;
                newSurvey.CreateDateUtc = DateTime.UtcNow;
                newSurvey.UpdateDateUtc = DateTime.UtcNow;
                newSurvey.LanguageId = survey.LanguageId;
                newSurvey.Status = _dbContext.SurveyStatuses.Single(t => t.Name == "Offline");
                newSurvey.SurveyFileId = survey.SurveyFileId;
                newSurvey.Introduction = survey.Introduction;
                newSurvey.Landing = survey.Landing;
                newSurvey.Thanks = survey.Thanks;
                newSurvey.UserId = survey.UserId;

                _dbContext.Surveys.Add(newSurvey);
                _dbContext.SaveChanges();
                #region // Respondents
                foreach (var item in survey.Respondents)
                {
                    var _resp = new Respondent
                    {
                        CreateDateUtc = DateTime.Now,
                        UpdateDateUtc = DateTime.UtcNow,
                        SurveyId = newSurvey.Id,
                        IsAfterSurvey = item.IsAfterSurvey,
                    };

                    _dbContext.Respondents.Add(_resp);
                    _dbContext.SaveChanges();

                    foreach (var item2 in item.Questions)
                    {
                        var _q = new Question
                        {
                            CreateDateUtc = DateTime.UtcNow,
                            UpdateDateUtc = DateTime.UtcNow,
                            Introducing = item2.Introducing,
                            IsAfterSurvey = item2.IsAfterSurvey,
                            IsAnnotation = item2.IsAnnotation,
                            IsCompulsory = item2.IsCompulsory,
                            IsMultiple = item2.IsMultiple,
                            IsShowValue = item2.IsShowValue,
                            OrderId = item2.OrderId,
                            QuestionFormatId = item2.QuestionFormatId,
                            RespondentId = _resp.Id,
                            Rows = item2.Rows,
                            ShortName = item2.ShortName,
                            Text = item2.Text,
                            TextMax = item2.TextMax,
                            TextMin = item2.TextMin,
                            TextRowsCount = item2.TextRowsCount,
                            ValueMax = item2.ValueMax,
                            ValueMin = item2.ValueMin
                        };
                        _dbContext.Question.Add(_q);
                        _dbContext.SaveChanges();

                        foreach (var item3 in item2.Answers)
                        {
                            _dbContext.Answers.Add(new Answer
                            {
                                CreateDateUtc = DateTime.UtcNow,
                                UpdateDateUtc = DateTime.UtcNow,
                                IsDefault = item3.IsDefault,
                                OrderId = item3.OrderId,
                                QuestionId = _q.Id,
                                Text = item3.Text,
                                Value = item3.Value
                            });
                            _dbContext.SaveChanges();
                        }
                    }
                }
                #endregion

                #region // RelationshipItems
                foreach (var item in survey.RelationshipItems)
                {
                    var _rel = new RelationshipItem
                    {
                        CreateDateUtc = DateTime.Now,
                        UpdateDateUtc = DateTime.UtcNow,
                        SurveyId = newSurvey.Id,
                        AddNodes = item.AddNodes,
                        AllowSelectAllNodes = item.AllowSelectAllNodes,
                        CanSkip = item.CanSkip,
                        GeneratorName = item.GeneratorName,
                        HideAddedNodes = item.HideAddedNodes,
                        MaximumNodes = item.MaximumNodes,
                        Name = item.Name,
                        NodeList = item.NodeList,
                        NodeSelectionId = item.NodeSelectionId,
                        OrderId = item.OrderId,
                        QuestionLayoutId = item.QuestionLayoutId,
                        SortNodeList = item.SortNodeList,
                        SuperUserViewNodes = item.SuperUserViewNodes,
                        UseDDSearch = item.UseDDSearch
                    };
                    _dbContext.RelationshipItems.Add(_rel);
                    _dbContext.SaveChanges();

                    foreach (var item2 in item.Questions)
                    {
                        var _rq = new RQuestion
                        {
                            CreateDateUtc = DateTime.UtcNow,
                            UpdateDateUtc = DateTime.UtcNow,
                            Introducing = item2.Introducing,
                            IsAfterSurvey = item2.IsAfterSurvey,
                            IsAnnotation = item2.IsAnnotation,
                            IsCompulsory = item2.IsCompulsory,
                            IsMultiple = item2.IsMultiple,
                            IsShowValue = item2.IsShowValue,
                            OrderId = item2.OrderId,
                            QuestionFormatId = item2.QuestionFormatId,
                            RelationshipItemId = _rel.Id,
                            Rows = item2.Rows,
                            ShortName = item2.ShortName,
                            Text = item2.Text,
                            TextMax = item2.TextMax,
                            TextMin = item2.TextMin,
                            TextRowsCount = item2.TextRowsCount,
                            ValueMax = item2.ValueMax,
                            ValueMin = item2.ValueMin,
                        };
                        _dbContext.RQuestions.Add(_rq);
                        _dbContext.SaveChanges();

                        foreach (var item3 in item2.Answers)
                        {
                            _dbContext.RAnswers.Add(new RAnswer
                            {
                                CreateDateUtc = DateTime.UtcNow,
                                UpdateDateUtc = DateTime.UtcNow,
                                IsDefault = item3.IsDefault,
                                OrderId = item3.OrderId,
                                RQuestionId = _rq.Id,
                                Text = item3.Text,
                                Value = item3.Value
                            });
                            _dbContext.SaveChanges();
                        }
                    }
                    //node
                    foreach (var item22 in item.NodeQuestions)
                    {
                        var _nq = new NQuestion
                        {
                            CreateDateUtc = DateTime.UtcNow,
                            UpdateDateUtc = DateTime.UtcNow,
                            Introducing = item22.Introducing,
                            IsAfterSurvey = item22.IsAfterSurvey,
                            IsAnnotation = item22.IsAnnotation,
                            IsCompulsory = item22.IsCompulsory,
                            IsMultiple = item22.IsMultiple,
                            IsShowValue = item22.IsShowValue,
                            OrderId = item22.OrderId,
                            QuestionFormatId = item22.QuestionFormatId,
                            RelationshipItemId = _rel.Id,
                            Rows = item22.Rows,
                            ShortName = item22.ShortName,
                            Text = item22.Text,
                            TextMax = item22.TextMax,
                            TextMin = item22.TextMin,
                            TextRowsCount = item22.TextRowsCount,
                            ValueMax = item22.ValueMax,
                            ValueMin = item22.ValueMin,
                        };
                        _dbContext.NQuestions.Add(_nq);
                        _dbContext.SaveChanges();

                        foreach (var item32 in item22.Answers)
                        {
                            _dbContext.NAnswers.Add(new NAnswer()
                            {
                                CreateDateUtc = DateTime.UtcNow,
                                UpdateDateUtc = DateTime.UtcNow,
                                IsDefault = item32.IsDefault,
                                OrderId = item32.OrderId,
                                NQuestionId = _nq.Id,
                                Text = item32.Text,
                                Value = item32.Value
                            });
                            _dbContext.SaveChanges();
                        }
                    }
                }
                #endregion
            }
            catch
            {
                // ignored
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Respondent(int? id, bool isAfter = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var survey = await _dbContext.Surveys.FindAsync(id);
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
                _dbContext.Respondents.Add(resp);
                await _dbContext.SaveChangesAsync();
                survey.Respondents.Add(resp);
            }
            _dbContext.Entry(survey).State = EntityState.Modified;
            ViewBag.Formats = _dbContext.QuestionFormats.ToList();
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
            question.QuestionFormatId = _dbContext.QuestionFormats.FirstOrDefault(x => x.Code == code)?.Id ?? 1;
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
                            IsDefault = _val[i] == Request.Form["Answer.Default"],
                            OrderId = i
                        });
                }
            }
            try
            {
                var orderId = _dbContext.Question.Where(x => x.RespondentId == question.RespondentId).Count() + 1;
                if (question.OrderId == 0)
                    question.OrderId = orderId;

                if (question.Id == 0)
                {
                    question.Answers = Answers;
                    _dbContext.Question.Add(question);
                }
                else
                {
                    var answers = _dbContext.Answers.Where(x => x.QuestionId == question.Id);
                    _dbContext.Answers.RemoveRange(answers);
                    _dbContext.SaveChanges();
                    _dbContext.Entry(question).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    _dbContext.Answers.AddRange(Answers);
                }

                await _dbContext.SaveChangesAsync();
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
            var resp = _dbContext.Respondents.SingleOrDefault(x => x.Id == id);
            if (resp == null)
            {
                return Json(new { success = false });
            }
            resp.IsAfterSurvey = value;
            await _dbContext.SaveChangesAsync();

            return Json(new { success = true });
        }

        public ActionResult EditQuestion(int id, int respId, bool isAfter = true)
        {
            var model = _dbContext.Question.Find(id);
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

            ViewBag.Formats = _dbContext.QuestionFormats.ToList();
            return PartialView(model);
        }

        public ActionResult EditRQuestion(int id, int relId, bool isAfter = true)
        {
            var model = _dbContext.RQuestions.Find(id);
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

            ViewBag.Formats = _dbContext.QuestionFormats.ToList();
            return PartialView(model);
        }

        public ActionResult EditNQuestion(int id, int relId, bool isAfter = true)
        {
            var model = _dbContext.NQuestions.Find(id);
            var _answers = new List<NAnswer>();
            for (int i = 0; i < 5; i++)
            {
                _answers.Add(new NAnswer()
                {
                    OrderId = i + 1,
                    Text = "",
                    Value = "",
                });
            }
            if (model == null)
            {
                model = new NQuestion()
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

            ViewBag.Formats = _dbContext.QuestionFormats.ToList();
            return PartialView(model);
        }


        public async Task<ActionResult> DeleteQuestion(int id)
        {
            try
            {
                var _Question = await _dbContext.Question.FindAsync(id);
                var _RespId = _Question.RespondentId;
                _dbContext.Question.Remove(_Question);
                await _dbContext.SaveChangesAsync();

                _dbContext.Database.ExecuteSqlCommand("exec SortRespondentQuestions {0}", _RespId);

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
                var _Question = await _dbContext.RQuestions.FindAsync(id);
                var _RelId = _Question.RelationshipItemId;
                _dbContext.RQuestions.Remove(_Question);
                await _dbContext.SaveChangesAsync();

                _dbContext.Database.ExecuteSqlCommand("exec SortRelationQuestions {0}", _RelId);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }


        }

        public async Task<ActionResult> DeleteNQuestion(int id)
        {
            try
            {
                var _Question = await _dbContext.NQuestions.FindAsync(id);
                var _RelId = _Question.RelationshipItemId;
                _dbContext.NQuestions.Remove(_Question);
                await _dbContext.SaveChangesAsync();

                _dbContext.Database.ExecuteSqlCommand("exec SortRelationNodeQuestions {0}", _RelId);

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
                var _q = await _dbContext.Question.FindAsync(id);
                var count = _dbContext.Question.Count(x => x.RespondentId == _q.RespondentId);
                if (isInc)
                {
                    if (_q.OrderId < count)
                    {
                        _q.OrderId += 1;

                        var buff = _dbContext.Question.FirstOrDefault(x => x.RespondentId == _q.RespondentId && x.OrderId == _q.OrderId);
                        if (buff != null)
                            buff.OrderId -= 1;
                    }

                }
                else
                {
                    if (_q.OrderId > 1)
                    {
                        _q.OrderId -= 1;

                        var buff = _dbContext.Question.FirstOrDefault(x => x.RespondentId == _q.RespondentId && x.OrderId == _q.OrderId);
                        if (buff != null)
                            buff.OrderId += 1;
                    }
                }

                await _dbContext.SaveChangesAsync();
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
                var _q = await _dbContext.RQuestions.FindAsync(id);
                var count = _dbContext.RQuestions.Count(x => x.RelationshipItemId == _q.RelationshipItemId);
                if (isInc)
                {
                    if (_q.OrderId < count)
                    {
                        _q.OrderId += 1;

                        var buff = _dbContext.RQuestions.FirstOrDefault(x => x.RelationshipItemId == _q.RelationshipItemId && x.OrderId == _q.OrderId);
                        if (buff != null)
                            buff.OrderId -= 1;
                    }

                }
                else
                {
                    if (_q.OrderId > 1)
                    {
                        _q.OrderId -= 1;

                        var buff = _dbContext.RQuestions.FirstOrDefault(x => x.RelationshipItemId == _q.RelationshipItemId && x.OrderId == _q.OrderId);
                        if (buff != null)
                            buff.OrderId += 1;
                    }
                }

                await _dbContext.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        public async Task<ActionResult> ChangeNQuestionPosition(int id, bool isInc)
        {
            try
            {
                var _q = await _dbContext.NQuestions.FindAsync(id);
                var count = _dbContext.NQuestions.Count(x => x.RelationshipItemId == _q.RelationshipItemId);
                if (isInc)
                {
                    if (_q.OrderId < count)
                    {
                        _q.OrderId += 1;

                        var buff = _dbContext.NQuestions.FirstOrDefault(x => x.RelationshipItemId == _q.RelationshipItemId && x.OrderId == _q.OrderId);
                        if (buff != null)
                            buff.OrderId -= 1;
                    }

                }
                else
                {
                    if (_q.OrderId > 1)
                    {
                        _q.OrderId -= 1;

                        var buff = _dbContext.NQuestions.FirstOrDefault(x => x.RelationshipItemId == _q.RelationshipItemId && x.OrderId == _q.OrderId);
                        if (buff != null)
                            buff.OrderId += 1;
                    }
                }

                await _dbContext.SaveChangesAsync();
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
                _dbContext.Dispose();
            }
            base.Dispose(disposing);
        }

        public async Task<ActionResult> Relationship(int? id, bool questions = false, int? relId = null, bool node = false)
        {
            var userId = User.Identity.GetUserId();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var survey = await _dbContext.Surveys.FindAsync(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            if (survey.RelationshipItems == null)
                survey.RelationshipItems = new List<RelationshipItem>();

            ViewBag.SurveyId = id.Value;

            var model = new RelationshipView();
            model.RelationshipItems = survey.RelationshipItems.ToList();
            if (model.RelationshipItems.Count == 0)
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
                    OrderId = 1,
                    NodeList = survey.SurveyFileId.ToString()
                };
                _dbContext.RelationshipItems.Add(rel);
                _dbContext.SaveChanges();

                model.RelationshipItems.Add(rel);
            }
            if (!relId.HasValue)
                model.SelectedItem = model.RelationshipItems.OrderBy(x => x.OrderId).FirstOrDefault();
            else
                model.SelectedItem = model.RelationshipItems.FirstOrDefault(x => x.Id == relId.Value);

            ViewBag.QuestionLayoutId = new SelectList(_dbContext.QuestionLayouts, "Id", "Name", model.SelectedItem?.QuestionLayoutId);
            ViewBag.NodeSelectionId = new SelectList(_dbContext.NodeSelections, "Id", "Name", model.SelectedItem?.NodeSelectionId);
            ViewBag.IsQuestion = questions;
            ViewBag.IsNode = node;
            ViewBag.NodeList = new SelectList(_dbContext.SurveyFiles.Where(x => x.UserId == userId), "Id", "Name", model.SelectedItem?.NodeList);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddRelationsip(int id)
        {
            try
            {
                var survey = await _dbContext.Surveys.FindAsync(id);
                var count = _dbContext.RelationshipItems.Count(x => x.SurveyId == id);
                var rel = new RelationshipItem()
                {
                    Name = "New Relationship",
                    MaximumNodes = 0,
                    QuestionLayoutId = 1,
                    NodeSelectionId = 1,
                    SurveyId = id,
                    CreateDateUtc = DateTime.UtcNow,
                    UpdateDateUtc = DateTime.UtcNow,
                    OrderId = count + 1,
                    NodeList = survey.SurveyFileId.ToString()
                };
                _dbContext.RelationshipItems.Add(rel);
                _dbContext.SaveChanges();

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
                var r = _dbContext.RelationshipItems.Find(id);
                var sId = r.SurveyId;
                if (_dbContext.RelationshipItems.Count(x => x.SurveyId == sId) <= 1)
                    return Json(new { success = false, error = "You cann't delete the last relationship" });

                var q = _dbContext.RQuestions.Where(x => x.RelationshipItemId == id);
                _dbContext.RQuestions.RemoveRange(q);
                _dbContext.SaveChanges();
                _dbContext.RelationshipItems.Remove(r);
                _dbContext.SaveChanges();

                _dbContext.Database.ExecuteSqlCommand("exec SortRelationship {0}", sId);

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
                var _q = await _dbContext.RelationshipItems.FindAsync(id);
                var count = _dbContext.RelationshipItems.Count(x => x.SurveyId == _q.SurveyId);
                if (isInc)
                {
                    if (_q.OrderId < count)
                    {
                        _q.OrderId += 1;

                        var buff = _dbContext.RelationshipItems.FirstOrDefault(x => x.SurveyId == _q.SurveyId && x.OrderId == _q.OrderId);
                        if (buff != null)
                            buff.OrderId -= 1;
                    }
                }
                else
                {
                    if (_q.OrderId > 1)
                    {
                        _q.OrderId -= 1;

                        var buff = _dbContext.RelationshipItems.FirstOrDefault(x => x.SurveyId == _q.SurveyId && x.OrderId == _q.OrderId);
                        if (buff != null)
                            buff.OrderId += 1;
                    }
                }

                await _dbContext.SaveChangesAsync();
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
            question.QuestionFormatId = _dbContext.QuestionFormats.FirstOrDefault(x => x.Code == code)?.Id ?? 1;
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
                            IsDefault = _val[i] == Request.Form["Answer.Default"],
                            OrderId = i
                        });
                }
            }
            try
            {
                var orderId = _dbContext.RQuestions.Where(x => x.RelationshipItemId == question.RelationshipItemId).Count() + 1;
                if (question.OrderId == 0)
                    question.OrderId = orderId;

                if (question.Id == 0)
                {
                    question.Answers = Answers;
                    _dbContext.RQuestions.Add(question);
                }
                else
                {
                    var answers = _dbContext.RAnswers.Where(x => x.RQuestionId == question.Id);
                    _dbContext.RAnswers.RemoveRange(answers);
                    _dbContext.SaveChanges();
                    _dbContext.Entry(question).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    _dbContext.RAnswers.AddRange(Answers);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<JsonResult> RelationshipN(NQuestion question)
        {
            var Answers = new List<NAnswer>();
            var code = Request.Form["FormatCode"];
            if (code == null)
                return Json(new { success = false });
            question.CreateDateUtc = question.UpdateDateUtc = DateTime.UtcNow;
            question.QuestionFormatId = _dbContext.QuestionFormats.FirstOrDefault(x => x.Code == code)?.Id ?? 1;
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
                        Answers.Add(new NAnswer()
                        {
                            NQuestionId = question.Id,
                            CreateDateUtc = DateTime.UtcNow,
                            UpdateDateUtc = DateTime.UtcNow,
                            Text = _text[i],
                            Value = _val[i],
                            IsDefault = _val[i] == Request.Form["Answer.Default"],
                            OrderId = i
                        });
                }
            }
            try
            {
                var orderId = _dbContext.NQuestions.Where(x => x.RelationshipItemId == question.RelationshipItemId).Count() + 1;
                if (question.OrderId == 0)
                    question.OrderId = orderId;

                if (question.Id == 0)
                {
                    question.Answers = Answers;
                    _dbContext.NQuestions.Add(question);
                }
                else
                {
                    var answers = _dbContext.NAnswers.Where(x => x.NQuestionId == question.Id);
                    _dbContext.NAnswers.RemoveRange(answers);
                    _dbContext.SaveChanges();
                    _dbContext.Entry(question).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    _dbContext.NAnswers.AddRange(Answers);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<ActionResult> AddNode(int id, bool addNode)
        {
            try
            {
                var item = _dbContext.RelationshipItems.Find(id);

                if (item != null)
                    item.AddNodes = addNode;

                await _dbContext.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        public async Task<ActionResult> GetRelationshipItem(int id)
        {
            var item = await _dbContext.RelationshipItems.FindAsync(id);
            ViewBag.QuestionLayoutId = new SelectList(_dbContext.QuestionLayouts, "Id", "Name", item.QuestionLayoutId);
            ViewBag.NodeSelectionId = new SelectList(_dbContext.NodeSelections, "Id", "Name", item.NodeSelectionId);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> GetRelationshipItem(RelationshipItem item)
        {
            int _id = int.Parse(Request.Form["RelId"]);
            var buff = _dbContext.RelationshipItems.FirstOrDefault(x => x.Id == _id);

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

            await _dbContext.SaveChangesAsync();

            if (bool.Parse(Request.Form["isBack"]))
                return RedirectToAction("Respondent", new { id = buff.SurveyId });

            if (bool.Parse(Request.Form["isNext"]))
                return RedirectToAction("Index", "Preview", new { id = buff.SurveyId });

            return RedirectToAction("Relationship", new { id = buff.SurveyId, questions = false, relId = buff.Id });
        }

        public async Task<FileResult> Download(int id)
        {
            var spreadsheetData = await _resultManager.GetResults(id);
            var survey = await _surveyManager.GetAsync(id);
            return File(await SpreadSheetProvider.Instance.Generate(survey, spreadsheetData), "application/ms-excel", $"{survey.Name}-{DateTime.Now:yyyy_MM_dd HH-mm}.xlsx");
        }
    }
}
