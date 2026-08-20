using ClosedXML.Excel;
using Domain.Contracts.IServices;
using Domain.Models;

namespace Infrastructure.Services
{
    public class PaymentReportService : IPaymentReportService
    {
        private const string AmountFormat = "\"$\"#,##0";
        private const string DateFormat = "dd/MM/yyyy HH:mm";
        private const string DayFormat = "dd/MM/yyyy";
        private const int LastColumn = 10;

        private static readonly XLColor PrimaryBlue = XLColor.FromHtml("#3E5C76");
        private static readonly XLColor AccentGold = XLColor.FromHtml("#F3E5AB");
        private static readonly XLColor SoftGray = XLColor.FromHtml("#F2F4F7");
        private static readonly XLColor BorderGray = XLColor.FromHtml("#C5CDD6");
        private static readonly XLColor TextMuted = XLColor.FromHtml("#5B6B7C");
        private static readonly XLColor TextDark = XLColor.FromHtml("#1F2A37");

        public byte[] GeneratePaymentReport(PaymentReportInformation information)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Pagos");

            ApplySheetDefaults(worksheet);

            var currentRow = WriteTitle(worksheet);
            currentRow = WriteMetaBar(worksheet, information, currentRow);
            currentRow++;
            currentRow = WriteTotalsBar(worksheet, information.Rows, currentRow);
            currentRow++;
            currentRow = WriteMethodSummary(worksheet, information.Rows, currentRow);
            currentRow += 2;
            WriteDetailTable(worksheet, information.Rows, currentRow);

            FitColumns(worksheet);

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        private static void ApplySheetDefaults(IXLWorksheet worksheet)
        {
            worksheet.Style.Font.FontName = "Calibri";
            worksheet.Style.Font.FontSize = 11;
            worksheet.Style.Font.FontColor = TextDark;
            worksheet.ShowGridLines = false;
        }

        private static int WriteTitle(IXLWorksheet worksheet)
        {
            var titleRange = worksheet.Range(1, 1, 1, LastColumn);
            titleRange.Merge();
            titleRange.Value = "INFORME DE PAGOS";
            titleRange.Style.Font.Bold = true;
            titleRange.Style.Font.FontSize = 20;
            titleRange.Style.Font.FontColor = PrimaryBlue;
            titleRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            worksheet.Row(1).Height = 32;

            var underline = worksheet.Range(2, 1, 2, LastColumn);
            underline.Style.Border.BottomBorder = XLBorderStyleValues.Medium;
            underline.Style.Border.BottomBorderColor = PrimaryBlue;

            return 3;
        }

        private static int WriteMetaBar(
            IXLWorksheet worksheet,
            PaymentReportInformation information,
            int startRow)
        {
            var headerRow = startRow;
            var valueRow = startRow + 1;

            SetHeaderCell(worksheet.Cell(headerRow, 1), "CONSULTORIO", 4);
            SetHeaderCell(worksheet.Cell(headerRow, 5), "PERIODO", 3);
            SetHeaderCell(worksheet.Cell(headerRow, 8), "GENERADO", 3);

            SetValueCell(worksheet.Cell(valueRow, 1), information.ConsultoryName, 4);
            SetValueCell(worksheet.Cell(valueRow, 5), BuildPeriodLabel(information), 3);
            SetValueCell(worksheet.Cell(valueRow, 8), information.GeneratedAt, 3);
            worksheet.Cell(valueRow, 8).Style.DateFormat.Format = DateFormat;

            ApplyBoxBorder(worksheet.Range(headerRow, 1, valueRow, LastColumn));
            worksheet.Row(headerRow).Height = 18;
            worksheet.Row(valueRow).Height = 22;

            return valueRow + 1;
        }

        private static int WriteTotalsBar(
            IXLWorksheet worksheet,
            IReadOnlyList<PaymentReportRowInformation> rows,
            int startRow)
        {
            var headerRow = startRow;
            var valueRow = startRow + 1;
            var totalPaid = rows.Sum(row => row.Amount);

            SetAccentHeaderCell(worksheet.Cell(headerRow, 1), "TOTAL DE PAGOS", 5);
            SetAccentHeaderCell(worksheet.Cell(headerRow, 6), "TOTAL RECAUDADO", 5);

            SetValueCell(worksheet.Cell(valueRow, 1), rows.Count, 5);
            worksheet.Cell(valueRow, 1).Style.Font.FontSize = 14;
            worksheet.Cell(valueRow, 1).Style.Font.Bold = true;

            SetValueCell(worksheet.Cell(valueRow, 6), totalPaid, 5);
            worksheet.Cell(valueRow, 6).Style.NumberFormat.Format = AmountFormat;
            worksheet.Cell(valueRow, 6).Style.Font.FontSize = 14;
            worksheet.Cell(valueRow, 6).Style.Font.Bold = true;
            worksheet.Cell(valueRow, 6).Style.Font.FontColor = PrimaryBlue;

            ApplyBoxBorder(worksheet.Range(headerRow, 1, valueRow, LastColumn));
            worksheet.Row(headerRow).Height = 18;
            worksheet.Row(valueRow).Height = 26;

            return valueRow + 1;
        }

        private static int WriteMethodSummary(
            IXLWorksheet worksheet,
            IReadOnlyList<PaymentReportRowInformation> rows,
            int startRow)
        {
            var sectionTitle = worksheet.Range(startRow, 1, startRow, 4);
            sectionTitle.Merge();
            sectionTitle.Value = "MÉTODO DE PAGO";
            sectionTitle.Style.Font.Bold = true;
            sectionTitle.Style.Font.FontSize = 12;
            sectionTitle.Style.Font.FontColor = PrimaryBlue;
            sectionTitle.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            worksheet.Row(startRow).Height = 20;

            var headerRow = startRow + 1;
            SetPrimaryHeaderCell(worksheet.Cell(headerRow, 1), "MÉTODO", 3);
            SetPrimaryHeaderCell(worksheet.Cell(headerRow, 4), "MONTO", 1);
            worksheet.Cell(headerRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

            var methodTotals = rows
                .GroupBy(row => row.PaymentMethod)
                .Select(group => new
                {
                    Method = group.Key,
                    Total = group.Sum(row => row.Amount)
                })
                .OrderByDescending(item => item.Total)
                .ThenBy(item => item.Method)
                .ToList();

            var currentRow = headerRow + 1;
            var totalPaid = rows.Sum(row => row.Amount);

            if (methodTotals.Count == 0)
            {
                worksheet.Cell(currentRow, 1).Value = "Sin pagos en el periodo";
                worksheet.Range(currentRow, 1, currentRow, 3).Merge();
                StyleDataRow(worksheet.Range(currentRow, 1, currentRow, 4), soft: true);
                worksheet.Cell(currentRow, 4).Value = 0m;
                worksheet.Cell(currentRow, 4).Style.NumberFormat.Format = AmountFormat;
                worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                currentRow++;
            }
            else
            {
                var odd = false;
                foreach (var method in methodTotals)
                {
                    worksheet.Range(currentRow, 1, currentRow, 3).Merge();
                    worksheet.Cell(currentRow, 1).Value = method.Method;
                    worksheet.Cell(currentRow, 4).Value = method.Total;
                    worksheet.Cell(currentRow, 4).Style.NumberFormat.Format = AmountFormat;
                    worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    StyleDataRow(worksheet.Range(currentRow, 1, currentRow, 4), soft: odd);
                    odd = !odd;
                    currentRow++;
                }
            }

            worksheet.Range(currentRow, 1, currentRow, 3).Merge();
            worksheet.Cell(currentRow, 1).Value = "TOTAL";
            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 4).Value = totalPaid;
            worksheet.Cell(currentRow, 4).Style.NumberFormat.Format = AmountFormat;
            worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            StyleTotalRow(worksheet.Range(currentRow, 1, currentRow, 4));

            ApplyBoxBorder(worksheet.Range(headerRow, 1, currentRow, 4));

            return currentRow;
        }

        private static void WriteDetailTable(
            IXLWorksheet worksheet,
            IReadOnlyList<PaymentReportRowInformation> rows,
            int startRow)
        {
            var sectionTitle = worksheet.Range(startRow, 1, startRow, LastColumn);
            sectionTitle.Merge();
            sectionTitle.Value = "DETALLE DE PAGOS";
            sectionTitle.Style.Font.Bold = true;
            sectionTitle.Style.Font.FontSize = 12;
            sectionTitle.Style.Font.FontColor = PrimaryBlue;
            sectionTitle.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            worksheet.Row(startRow).Height = 20;

            var headerRowIndex = startRow + 1;
            string[] headers =
            [
                "FECHA",
                "PACIENTE",
                "TIPO ID",
                "IDENTIFICACIÓN",
                "CONCEPTO",
                "TRATAMIENTO",
                "SERVICIO",
                "MÉTODO",
                "MONTO",
                "NOTAS"
            ];

            for (var column = 1; column <= headers.Length; column++)
            {
                var cell = worksheet.Cell(headerRowIndex, column);
                cell.Value = headers[column - 1];
                cell.Style.Font.Bold = true;
                cell.Style.Font.FontColor = XLColor.White;
                cell.Style.Fill.BackgroundColor = PrimaryBlue;
                cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                cell.Style.Alignment.Horizontal = column == 9
                    ? XLAlignmentHorizontalValues.Right
                    : XLAlignmentHorizontalValues.Left;
                cell.Style.Alignment.Indent = 1;
            }

            worksheet.Row(headerRowIndex).Height = 22;

            var currentRow = headerRowIndex + 1;
            var odd = false;

            if (rows.Count == 0)
            {
                var emptyRange = worksheet.Range(currentRow, 1, currentRow, LastColumn);
                emptyRange.Merge();
                emptyRange.Value = "No hay pagos para el periodo seleccionado.";
                emptyRange.Style.Font.FontColor = TextMuted;
                emptyRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                StyleDataRow(emptyRange, soft: true);
                currentRow++;
            }
            else
            {
                foreach (var row in rows)
                {
                    worksheet.Cell(currentRow, 1).Value = row.PaidAt;
                    worksheet.Cell(currentRow, 1).Style.DateFormat.Format = DateFormat;
                    worksheet.Cell(currentRow, 2).Value = row.PatientFullName;
                    worksheet.Cell(currentRow, 3).Value = row.IdentificationTypeCode;
                    worksheet.Cell(currentRow, 4).Value = row.IdentificationNumber;
                    worksheet.Cell(currentRow, 5).Value = row.Concept;
                    worksheet.Cell(currentRow, 6).Value = row.TreatmentName;
                    worksheet.Cell(currentRow, 7).Value = row.ServiceName;
                    worksheet.Cell(currentRow, 8).Value = row.PaymentMethod;
                    worksheet.Cell(currentRow, 9).Value = row.Amount;
                    worksheet.Cell(currentRow, 9).Style.NumberFormat.Format = AmountFormat;
                    worksheet.Cell(currentRow, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                    worksheet.Cell(currentRow, 10).Value = row.Notes;

                    StyleDataRow(worksheet.Range(currentRow, 1, currentRow, LastColumn), soft: odd);
                    odd = !odd;
                    currentRow++;
                }
            }

            var totalPaid = rows.Sum(row => row.Amount);
            worksheet.Range(currentRow, 1, currentRow, 8).Merge();
            worksheet.Cell(currentRow, 1).Value = "TOTAL";
            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            worksheet.Cell(currentRow, 1).Style.Alignment.Indent = 1;
            worksheet.Cell(currentRow, 9).Value = totalPaid;
            worksheet.Cell(currentRow, 9).Style.NumberFormat.Format = AmountFormat;
            worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
            worksheet.Cell(currentRow, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            worksheet.Cell(currentRow, 10).Value = string.Empty;
            StyleTotalRow(worksheet.Range(currentRow, 1, currentRow, LastColumn));

            ApplyBoxBorder(worksheet.Range(headerRowIndex, 1, currentRow, LastColumn));
        }

        private static void SetHeaderCell(IXLCell cell, string value, int mergeColumns)
        {
            var range = cell.Worksheet.Range(cell.Address.RowNumber, cell.Address.ColumnNumber, cell.Address.RowNumber, cell.Address.ColumnNumber + mergeColumns - 1);
            range.Merge();
            range.Value = value;
            range.Style.Font.Bold = true;
            range.Style.Font.FontSize = 10;
            range.Style.Font.FontColor = XLColor.White;
            range.Style.Fill.BackgroundColor = PrimaryBlue;
            range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            range.Style.Alignment.Indent = 1;
        }

        private static void SetAccentHeaderCell(IXLCell cell, string value, int mergeColumns)
        {
            var range = cell.Worksheet.Range(cell.Address.RowNumber, cell.Address.ColumnNumber, cell.Address.RowNumber, cell.Address.ColumnNumber + mergeColumns - 1);
            range.Merge();
            range.Value = value;
            range.Style.Font.Bold = true;
            range.Style.Font.FontSize = 10;
            range.Style.Font.FontColor = TextDark;
            range.Style.Fill.BackgroundColor = AccentGold;
            range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            range.Style.Alignment.Indent = 1;
        }

        private static void SetPrimaryHeaderCell(IXLCell cell, string value, int mergeColumns)
        {
            var range = cell.Worksheet.Range(cell.Address.RowNumber, cell.Address.ColumnNumber, cell.Address.RowNumber, cell.Address.ColumnNumber + mergeColumns - 1);
            range.Merge();
            range.Value = value;
            range.Style.Font.Bold = true;
            range.Style.Font.FontSize = 10;
            range.Style.Font.FontColor = XLColor.White;
            range.Style.Fill.BackgroundColor = PrimaryBlue;
            range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            range.Style.Alignment.Indent = 1;
        }

        private static void SetValueCell(IXLCell cell, XLCellValue value, int mergeColumns)
        {
            var range = cell.Worksheet.Range(cell.Address.RowNumber, cell.Address.ColumnNumber, cell.Address.RowNumber, cell.Address.ColumnNumber + mergeColumns - 1);
            range.Merge();
            range.Value = value;
            range.Style.Fill.BackgroundColor = SoftGray;
            range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            range.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            range.Style.Alignment.Indent = 1;
        }

        private static void StyleDataRow(IXLRange range, bool soft)
        {
            range.Style.Fill.BackgroundColor = soft ? SoftGray : XLColor.White;
            range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            range.Style.Border.BottomBorder = XLBorderStyleValues.Hair;
            range.Style.Border.BottomBorderColor = BorderGray;
            range.Style.Alignment.Indent = 1;
            range.Worksheet.Row(range.FirstRow().RowNumber()).Height = 20;
        }

        private static void StyleTotalRow(IXLRange range)
        {
            range.Style.Fill.BackgroundColor = SoftGray;
            range.Style.Font.FontColor = TextDark;
            range.Style.Border.TopBorder = XLBorderStyleValues.Thin;
            range.Style.Border.TopBorderColor = PrimaryBlue;
            range.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            range.Style.Alignment.Indent = 1;
            range.Worksheet.Row(range.FirstRow().RowNumber()).Height = 22;
        }

        private static void ApplyBoxBorder(IXLRange range)
        {
            range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            range.Style.Border.OutsideBorderColor = BorderGray;
        }

        private static void FitColumns(IXLWorksheet worksheet)
        {
            worksheet.Column(1).Width = 18;
            worksheet.Column(2).Width = 28;
            worksheet.Column(3).Width = 10;
            worksheet.Column(4).Width = 16;
            worksheet.Column(5).Width = 14;
            worksheet.Column(6).Width = 18;
            worksheet.Column(7).Width = 18;
            worksheet.Column(8).Width = 14;
            worksheet.Column(9).Width = 14;
            worksheet.Column(10).Width = 28;
            worksheet.Column(10).Style.Alignment.WrapText = true;
        }

        private static string BuildPeriodLabel(PaymentReportInformation information)
        {
            if (information.PeriodFrom.HasValue && information.PeriodTo.HasValue)
            {
                return $"{information.PeriodFrom.Value.ToString(DayFormat)} - {information.PeriodTo.Value.ToString(DayFormat)}";
            }

            if (information.Rows.Count == 0)
            {
                return "Todos";
            }

            var from = information.Rows.Min(row => row.PaidAt).Date;
            var to = information.Rows.Max(row => row.PaidAt).Date;

            return $"{from.ToString(DayFormat)} - {to.ToString(DayFormat)}";
        }
    }
}
