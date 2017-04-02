using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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

        public async Task<byte[]> Generate(IEnumerable<ResultModel> results)
        {
            using (var excelPackage = new ExcelPackage(new FileInfo(TemplatePath)))
            {
                foreach (var result in results)
                {
                    foreach (var section in await _resultManager.GetSections(result.Id))
                    {
                        if (section.SectionType.Name == Constants.RespondentBefore)
                        {

                            var excelSection = excelPackage.Workbook.Worksheets.FirstOrDefault(t => t.Name == "Vertices");
                            if (excelSection != null)
                            {
                                var address = excelSection.Cells["AC3"].Start;
                                FillSection(excelSection, address, (await _resultManager.GetAnswers(section.Id)).ToList());
                            }
                        }
                    }
                }
                return excelPackage.GetAsByteArray();
            }
        }

        private void FillSection(ExcelWorksheet excelWorksheet, ExcelCellAddress startAddress, List<QuestionAnswerModel> data)
        {
            var row = GetLastUsedRow(excelWorksheet) + 1;
            for (int i = 0; i < data.Count; i++)
            {
                excelWorksheet.Cells[row, startAddress.Column + i].Value = string.Join(";", data[i].Values);
            }
        }

        private void FillTitle(ExcelWorksheet excelWorksheet, StartPoint startTitlePoint, StartPoint fillPoint, string data)
        {
            excelWorksheet.Cells[startTitlePoint.StartY, startTitlePoint.StartX + fillPoint.StartX].Value = data;
            excelWorksheet.Cells[startTitlePoint.StartY, startTitlePoint.StartX + fillPoint.StartX].Style.Fill.PatternType = ExcelFillStyle.Solid;
            excelWorksheet.Cells[startTitlePoint.StartY, startTitlePoint.StartX + fillPoint.StartX].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));
            excelWorksheet.Cells[startTitlePoint.StartY, startTitlePoint.StartX + fillPoint.StartX].Style.Font.Color.SetColor(Color.White);
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