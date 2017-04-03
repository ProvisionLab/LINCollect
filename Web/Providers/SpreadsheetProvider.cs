using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Web.Common;
using Web.Managers.Interfaces;
using Web.Models.DTO;

namespace Web.Providers
{
    public class SpreadSheetProvider
    {
        public SpreadSheetProvider()
        {
            _resultManager = ServiceProvider.Instance.GetService<IResultManager>();
        }
        public static string TemplatePath { get; } = HttpContext.Current.Server.MapPath(Constants.TemplatePath);

        private static SpreadSheetProvider _instance;
        private readonly IResultManager _resultManager;

        public static SpreadSheetProvider Instance => _instance ?? (_instance = new SpreadSheetProvider());

        public async Task<byte[]> Generate(SurveyModel survey, IEnumerable<ResultModel> results)
        {
            using (var excelPackage = new ExcelPackage(new FileInfo(TemplatePath)))
            {
                //Rules for filling title
                if (survey.Respondents?.Count > 0)
                {
                    foreach (var respondent in survey.Respondents)
                    {
                        if (!respondent.IsAfterSurvey)
                        {
                            var before = survey.Respondents.Single(t => !t.IsAfterSurvey);

                            var excelSection = excelPackage.Workbook.Worksheets.FirstOrDefault(t => t.Name == "Vertices");
                            if (excelSection != null && before.Questions?.Count > 0)
                            {
                                var userAddress = excelSection.Cells["AD2"].Start;

                                FillTitle(excelSection, new StartPoint {StartX = userAddress.Column, StartY = userAddress.Row}, "Respondent Name" );
                                FillTitle(excelSection, new StartPoint {StartX = userAddress.Column + 1, StartY = userAddress.Row}, "Respondent Email");

                                var address = excelSection.Cells["AF2"].Start;
                                for (int i = 0; i < before.Questions.Count; i++)
                                {
                                    FillTitle(excelSection, new StartPoint { StartX = address.Column + i, StartY = address.Row }, Regex.Replace(before.Questions[i].Text, "<.*?>", String.Empty));
                                }
                            }
                        }
                    }
                }

                //Rules for filling data

                foreach (var result in results)
                {
                    foreach (var section in await _resultManager.GetSections(result.Id))
                    {
                        if (section.SectionType.Name == Constants.RespondentBefore)
                        {


                            var excelSection = excelPackage.Workbook.Worksheets.FirstOrDefault(t => t.Name == "Vertices");
                            if (excelSection != null)
                            {
                                var userAddress = excelSection.Cells["AD3"].Start;
                                var row = GetLastUsedRow(excelSection) + 1;

                                //Fill vertex name

                                FillCell(excelSection, new StartPoint {StartX = userAddress.Column, StartY = row}, result.PublishSurvey.UserName );
                                FillCell(excelSection, new StartPoint {StartX = userAddress.Column+1, StartY = row}, result.PublishSurvey.UserEmail);

                                var address = excelSection.Cells["AF3"].Start;
                                FillSection(excelSection, new StartPoint {StartX = address.Column, StartY = row}, (await _resultManager.GetAnswers(section.Id)).ToList());
                            }
                        }
                    }
                }
                return excelPackage.GetAsByteArray();
            }
        }

        private void FillSection(ExcelWorksheet excelWorksheet, StartPoint startAddress, List<QuestionAnswerModel> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                excelWorksheet.Cells[startAddress.StartY, startAddress.StartX + i].Value = string.Join(";", data[i].Values);
            }
        }

        private void FillTitle(ExcelWorksheet excelWorksheet, StartPoint fillPoint, string data)
        {
            excelWorksheet.Cells[fillPoint.StartY, fillPoint.StartX].Value = data;
            excelWorksheet.Cells[fillPoint.StartY, fillPoint.StartX].Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelWorksheet.Cells[fillPoint.StartY, fillPoint.StartX].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));
            excelWorksheet.Cells[fillPoint.StartY, fillPoint.StartX].Style.Font.Color.SetColor(Color.White);
        }

        private void FillCell(ExcelWorksheet excelWorksheet, StartPoint fillPoint, string data)
        {
            excelWorksheet.Cells[fillPoint.StartY, fillPoint.StartX].Value = data;
        }

        int GetLastUsedRow(ExcelWorksheet sheet)
        {
            var row = sheet.Dimension.End.Row;
            while (row >= 1)
            {
                var range = sheet.Cells[row, 1, row, sheet.Dimension.End.Column];
                if (range.Any(c => !string.IsNullOrEmpty(c.Text)))
                {
                    break;
                }
                row--;
            }
            return row;
        }
    }

    public class StartPoint
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
    }
}