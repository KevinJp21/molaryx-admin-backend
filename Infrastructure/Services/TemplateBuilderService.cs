using Domain.Contracts.IServices;
using Domain.Models;
using Infrastructure.Email;
using Infrastructure.Pdf;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.Globalization;

namespace Infrastructure.Services
{
    public class TemplateBuilderService : ITemplateBuilderService
    {
        private static readonly CultureInfo EsCo = CultureInfo.GetCultureInfo("es-CO");

        public string SetParametersToTemplate(
            string template,
            IDictionary<string, string> templateParameters)
        {
            return EmailTemplateService.SetParametersToTemplate(template, templateParameters);
        }

        public byte[] GenerateClinicalHistoryTemplate(ClinicalHistoryTemplateInformation information)
        {
            var document = new Document();
            document.Info.Title = "Historia clínica";
            document.Info.Author = information.ConsultoryName;

            var section = document.AddSection();
            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.TopMargin = Unit.FromCentimeter(2);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(2);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(2);
            section.PageSetup.RightMargin = Unit.FromCentimeter(2);

            ConfigureStyles(document);

            DrawTenantHeader(section, information);
            DrawDocumentTitle(section, information);
            DrawPatientSection(section, information);
            DrawRecordsSection(section, information);

            var renderer = new PdfDocumentRenderer
            {
                Document = document
            };
            renderer.RenderDocument();

            using var stream = new MemoryStream();
            renderer.PdfDocument.Save(stream, false);
            return stream.ToArray();
        }

        private static void ConfigureStyles(Document document)
        {
            var normal = document.Styles[StyleNames.Normal]!;
            normal.Font.Name = PdfFontBootstrap.ActiveFamily;
            normal.Font.Size = 10;

            var heading = document.Styles.AddStyle("SectionHeading", StyleNames.Normal);
            heading.Font.Size = 11;
            heading.Font.Bold = true;
            heading.ParagraphFormat.SpaceBefore = Unit.FromPoint(14);
            heading.ParagraphFormat.SpaceAfter = Unit.FromPoint(6);

            var recordHeading = document.Styles.AddStyle("RecordHeading", StyleNames.Normal);
            recordHeading.Font.Size = 10;
            recordHeading.Font.Bold = true;
            recordHeading.ParagraphFormat.SpaceBefore = Unit.FromPoint(10);
            recordHeading.ParagraphFormat.SpaceAfter = Unit.FromPoint(4);
        }

        private static void DrawTenantHeader(Section section, ClinicalHistoryTemplateInformation information)
        {
            var title = section.AddParagraph(information.ConsultoryName);
            title.Format.Font.Size = 14;
            title.Format.Font.Bold = true;
            title.Format.Alignment = ParagraphAlignment.Center;
            title.Format.SpaceAfter = Unit.FromPoint(6);

            AddCenteredDetail(section, information.TenantAddress);
            AddCenteredDetail(section, information.TenantPhoneNumber);
            AddCenteredDetail(section, information.TenantEmail);

            var tenantId = FormatIdentification(
                information.TenantIdentificationType,
                information.TenantIdentificationNumber);

            if (!string.IsNullOrWhiteSpace(tenantId))
            {
                AddCenteredDetail(section, tenantId);
            }
        }

        private static void DrawDocumentTitle(Section section, ClinicalHistoryTemplateInformation information)
        {
            var title = section.AddParagraph("HISTORIA CLÍNICA");
            title.Format.Font.Size = 13;
            title.Format.Font.Bold = true;
            title.Format.Alignment = ParagraphAlignment.Center;
            title.Format.SpaceBefore = Unit.FromPoint(16);
            title.Format.SpaceAfter = Unit.FromPoint(4);

            var generatedAt = section.AddParagraph(
                $"Generado el {FormatDateTime(information.GeneratedAt)}");
            generatedAt.Format.Alignment = ParagraphAlignment.Center;
            generatedAt.Format.Font.Size = 9;
            generatedAt.Format.Font.Color = Colors.DarkGray;
            generatedAt.Format.SpaceAfter = Unit.FromPoint(4);

            var period = section.AddParagraph(FormatPeriod(information.From, information.To));
            period.Format.Alignment = ParagraphAlignment.Center;
            period.Format.Font.Italic = true;
            period.Format.SpaceAfter = Unit.FromPoint(12);
        }

        private static void DrawPatientSection(Section section, ClinicalHistoryTemplateInformation information)
        {
            var heading = section.AddParagraph("Datos del paciente");
            heading.Style = "SectionHeading";

            var table = section.AddTable();
            table.Borders.Width = 0;
            table.Format.Font.Size = 10;

            table.AddColumn(Unit.FromCentimeter(4.5));
            table.AddColumn(Unit.FromCentimeter(12));

            AddInfoRow(table, "Nombre", $"{information.PatientName} {information.PatientSurname}".Trim());
            AddInfoRow(
                table,
                "Identificación",
                $"{information.PatientIdentificationType} {information.PatientIdentificationNumber}".Trim());
            AddInfoRow(table, "Fecha de nacimiento", information.PatientBirthDate.ToString("dd/MM/yyyy", EsCo));
            AddInfoRow(table, "Correo", information.PatientEmail);
            AddInfoRow(table, "Teléfono", information.PatientPhoneNumber);
        }

        private static void DrawRecordsSection(Section section, ClinicalHistoryTemplateInformation information)
        {
            var heading = section.AddParagraph("Registros clínicos");
            heading.Style = "SectionHeading";

            if (information.Records.Count == 0)
            {
                var empty = section.AddParagraph("No hay registros clínicos en el periodo seleccionado.");
                empty.Format.Font.Italic = true;
                empty.Format.Font.Color = Colors.DarkGray;
                return;
            }

            for (var index = 0; index < information.Records.Count; index++)
            {
                DrawRecord(section, information.Records[index], index + 1);
            }
        }

        private static void DrawRecord(
            Section section,
            ClinicalHistoryRecordInformation record,
            int index)
        {
            if (index > 1)
            {
                var separator = section.AddParagraph();
                separator.Format.Borders.Bottom.Width = 0.5;
                separator.Format.Borders.Bottom.Color = Colors.LightGray;
                separator.Format.SpaceBefore = Unit.FromPoint(8);
                separator.Format.SpaceAfter = Unit.FromPoint(8);
            }

            var heading = section.AddParagraph($"Registro #{index} · {FormatDateTime(record.RecordedAt)}");
            heading.Style = "RecordHeading";

            var professional = section.AddParagraph(
                $"Registrado por: {record.CreatedByName} {record.CreatedBySurname}".Trim());
            professional.Format.Font.Size = 9;
            professional.Format.Font.Color = Colors.DarkGray;
            professional.Format.SpaceAfter = Unit.FromPoint(6);

            if (!string.IsNullOrWhiteSpace(record.Reference))
            {
                AddRecordField(section, "Referencia", record.Reference);
            }

            if (!string.IsNullOrWhiteSpace(record.ServiceName))
            {
                AddRecordField(section, "Servicio", record.ServiceName);
            }

            AddRecordField(section, "Motivo", record.Reason);
            AddRecordField(section, "Diagnóstico", record.Diagnosis);
            AddRecordField(section, "Evolución", record.Evolution);
            AddRecordField(section, "Notas", record.Notes);
        }

        private static void AddRecordField(Section section, string label, string? value)
        {
            var paragraph = section.AddParagraph();
            paragraph.Format.SpaceAfter = Unit.FromPoint(6);

            var labelText = paragraph.AddFormattedText($"{label}: ", TextFormat.Bold);
            labelText.Font.Size = 10;

            paragraph.AddText(string.IsNullOrWhiteSpace(value) ? "—" : value.Trim());
        }

        private static void AddInfoRow(Table table, string label, string? value)
        {
            var row = table.AddRow();
            row.VerticalAlignment = VerticalAlignment.Top;

            var labelCell = row.Cells[0];
            labelCell.AddParagraph(label).Format.Font.Bold = true;

            var valueCell = row.Cells[1];
            valueCell.AddParagraph(string.IsNullOrWhiteSpace(value) ? "—" : value.Trim());
        }

        private static void AddCenteredDetail(Section section, string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var paragraph = section.AddParagraph(value.Trim());
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.Font.Size = 9;
            paragraph.Format.Font.Color = Colors.DarkGray;
        }

        private static string? FormatIdentification(string? type, string? number)
        {
            if (string.IsNullOrWhiteSpace(type) && string.IsNullOrWhiteSpace(number))
            {
                return null;
            }

            return $"{type} {number}".Trim();
        }

        private static string FormatDateTime(DateTime value)
        {
            return value.ToString("d MMM yyyy · HH:mm", EsCo);
        }

        private static string FormatPeriod(DateTime? from, DateTime? to)
        {
            if (from is null && to is null)
            {
                return "Historial completo";
            }

            if (from is not null && to is not null)
            {
                return $"Periodo: {from.Value:dd/MM/yyyy} — {to.Value:dd/MM/yyyy}";
            }

            if (from is not null)
            {
                return $"Periodo: desde {from.Value:dd/MM/yyyy}";
            }

            return $"Periodo: hasta {to!.Value:dd/MM/yyyy}";
        }
    }
}
