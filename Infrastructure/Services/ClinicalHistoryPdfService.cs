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
    public class ClinicalHistoryPdfService : IClinicalHistoryPdfService
    {
        private static readonly CultureInfo EsCo =
            CultureInfo.GetCultureInfo("es-CO");

        public byte[] GenerateClinicalHistoryTemplate(
            ClinicalHistoryTemplateInformation information)
        {
            var document = new Document();

            document.Info.Title = "Historia clínica";
            document.Info.Author = information.ConsultoryName;
            document.Info.Subject =
                $"{MolaryxPdfTheme.BrandName} · Historia clínica";

            var section = document.AddSection();

            // =========================================================
            // PAGE
            // =========================================================

            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.DifferentFirstPageHeaderFooter = true;

            section.PageSetup.TopMargin =
                Unit.FromCentimeter(2.1);

            section.PageSetup.BottomMargin =
                Unit.FromCentimeter(2.1);

            section.PageSetup.LeftMargin =
                Unit.FromCentimeter(2);

            section.PageSetup.RightMargin =
                Unit.FromCentimeter(2);

            section.PageSetup.HeaderDistance =
                Unit.FromCentimeter(0.45);

            section.PageSetup.FooterDistance =
                Unit.FromCentimeter(0.55);

            // =========================================================
            // CONFIGURATION
            // =========================================================

            ConfigureStyles(document);
            ConfigurePageHeaders(section, information);
            ConfigurePageFooter(section, information);

            // =========================================================
            // DOCUMENT
            // =========================================================

            DrawClinicHeader(section, information);
            DrawDocumentIntro(section, information);
            DrawPatientIdentity(section, information);
            DrawClinicalTimeline(section, information);

            // =========================================================
            // RENDER
            // =========================================================

            var renderer = new PdfDocumentRenderer
            {
                Document = document
            };

            renderer.RenderDocument();

            using var stream = new MemoryStream();

            renderer.PdfDocument.Save(
                stream,
                closeStream: false);

            return stream.ToArray();
        }

        // =====================================================================
        // STYLES
        // =====================================================================

        private static void ConfigureStyles(Document document)
        {
            var normal = document.Styles[StyleNames.Normal]!;

            normal.Font.Name = PdfFontBootstrap.ActiveFamily;
            normal.Font.Size = 9.2;
            normal.Font.Color = MolaryxPdfTheme.Ink50;

            var sectionLabel =
                document.Styles.AddStyle(
                    "SectionLabel",
                    StyleNames.Normal);

            sectionLabel.Font.Name =
                PdfFontBootstrap.ActiveFamily;

            sectionLabel.Font.Size = 7;
            sectionLabel.Font.Bold = true;
            sectionLabel.Font.Color =
                MolaryxPdfTheme.Accent500;

            sectionLabel.ParagraphFormat.SpaceBefore =
                Unit.FromPoint(17);

            sectionLabel.ParagraphFormat.SpaceAfter =
                Unit.FromPoint(7);
        }

        // =====================================================================
        // HEADER
        // =====================================================================

        private static void ConfigurePageHeaders(
            Section section,
            ClinicalHistoryTemplateInformation information)
        {
            ConfigureCompactPageHeader(
                section.Headers.Primary,
                information);
        }

        private static void DrawClinicHeader(
            Section section,
            ClinicalHistoryTemplateInformation information)
        {
            var table = section.AddTable();

            table.AddColumn(
                Unit.FromCentimeter(1.15));

            table.AddColumn(
                MolaryxPdfTheme.ContentWidth -
                Unit.FromCentimeter(1.15));

            table.Format.SpaceAfter =
                Unit.FromPoint(7);

            // ---------------------------------------------------------
            // Logo
            // ---------------------------------------------------------

            var row = table.AddRow();

            row.VerticalAlignment =
                VerticalAlignment.Center;

            row.BottomPadding =
                Unit.FromPoint(5);

            AddLogoCell(
                row.Cells[0],
                Unit.FromCentimeter(0.85),
                fallbackFontSize: 17);

            // ---------------------------------------------------------
            // Clinic information
            // ---------------------------------------------------------

            var cell = row.Cells[1];

            var brand = cell.AddParagraph();

            var brandText =
                brand.AddFormattedText(
                    MolaryxPdfTheme.BrandName,
                    TextFormat.Bold);

            brandText.Font.Size = 8.5;
            brandText.Font.Color =
                MolaryxPdfTheme.Accent500;

            brand.Format.SpaceAfter =
                Unit.FromPoint(1);

            var consultory =
                cell.AddParagraph(
                    information.ConsultoryName);

            consultory.Format.Font.Size = 10;
            consultory.Format.Font.Bold = true;
            consultory.Format.Font.Color =
                MolaryxPdfTheme.Ink50;

            consultory.Format.SpaceAfter =
                Unit.FromPoint(2);

            var contact = JoinMeta(
                information.TenantAddress,
                information.TenantPhoneNumber,
                information.TenantEmail);

            AddHeaderMeta(cell, contact);

            var tenantId = FormatIdentification(
                information.TenantIdentificationType,
                information.TenantIdentificationNumber);

            AddHeaderMeta(cell, tenantId);

            AddAccentLine(section);
        }

        private static void ConfigureCompactPageHeader(
            HeaderFooter header,
            ClinicalHistoryTemplateInformation information)
        {
            var table = header.AddTable();

            table.AddColumn(
                Unit.FromCentimeter(0.95));

            table.AddColumn(
                MolaryxPdfTheme.ContentWidth -
                Unit.FromCentimeter(0.95));

            var row = table.AddRow();

            row.Height =
                Unit.FromCentimeter(0.85);

            row.VerticalAlignment =
                VerticalAlignment.Center;

            AddLogoCell(
                row.Cells[0],
                Unit.FromCentimeter(0.58),
                fallbackFontSize: 11);

            var cell = row.Cells[1];

            var title =
                cell.AddParagraph();

            var brand =
                title.AddFormattedText(
                    MolaryxPdfTheme.BrandName,
                    TextFormat.Bold);

            brand.Font.Size = 8;
            brand.Font.Color =
                MolaryxPdfTheme.Accent500;

            title.AddText("  ·  ");

            var clinic =
                title.AddFormattedText(
                    information.ConsultoryName,
                    TextFormat.Bold);

            clinic.Font.Size = 8;
            clinic.Font.Color =
                MolaryxPdfTheme.Ink50;

            var subtitle =
                cell.AddParagraph("Historia clínica");

            subtitle.Format.Font.Size = 6.8;
            subtitle.Format.Font.Color =
                MolaryxPdfTheme.Ink300;

            // ---------------------------------------------------------
            // Header line
            // ---------------------------------------------------------

            var line = header.AddTable();

            line.AddColumn(
                MolaryxPdfTheme.ContentWidth);

            var lineRow = line.AddRow();

            lineRow.Height =
                Unit.FromPoint(1.2);

            lineRow.Cells[0].Shading.Color =
                MolaryxPdfTheme.Accent400;
        }

        // =====================================================================
        // FOOTER
        // =====================================================================

        private static void ConfigurePageFooter(
            Section section,
            ClinicalHistoryTemplateInformation information)
        {
            ConfigurePageFooterContent(
                section.Footers.Primary,
                information);

            ConfigurePageFooterContent(
                section.Footers.FirstPage,
                information);
        }

        private static void ConfigurePageFooterContent(
            HeaderFooter footer,
            ClinicalHistoryTemplateInformation information)
        {
            var rule = footer.AddTable();

            rule.AddColumn(
                MolaryxPdfTheme.ContentWidth);

            var ruleRow = rule.AddRow();

            ruleRow.Borders.Top.Width = 0.5;

            ruleRow.Borders.Top.Color =
                MolaryxPdfTheme.Ink800;

            ruleRow.Height =
                Unit.FromPoint(1);

            ruleRow.TopPadding =
                Unit.FromPoint(5);

            var paragraph =
                footer.AddParagraph();

            paragraph.Format.Font.Size = 7;
            paragraph.Format.Font.Color =
                MolaryxPdfTheme.Ink300;

            paragraph.Format.TabStops.AddTabStop(
                MolaryxPdfTheme.ContentWidth,
                TabAlignment.Right);

            var brand =
                paragraph.AddFormattedText(
                    MolaryxPdfTheme.BrandName,
                    TextFormat.Bold);

            brand.Font.Color =
                MolaryxPdfTheme.Ink50;

            paragraph.AddText(
                "  ·  Historia clínica  ·  ");

            paragraph.AddText(
                information.ConsultoryName);

            paragraph.AddText(
                "  ·  CONFIDENCIAL");

            paragraph.AddTab();

            paragraph.AddText("Pág. ");

            paragraph.AddPageField();

            paragraph.AddText(" / ");

            paragraph.AddNumPagesField();
        }

        // =====================================================================
        // DOCUMENT INTRO
        // =====================================================================

        private static void DrawDocumentIntro(
            Section section,
            ClinicalHistoryTemplateInformation information)
        {
            var table = section.AddTable();

            table.AddColumn(
                MolaryxPdfTheme.ContentWidth);

            table.Format.SpaceBefore =
                Unit.FromPoint(1);

            table.Format.SpaceAfter =
                Unit.FromPoint(13);

            var row = table.AddRow();

            row.BottomPadding =
                Unit.FromPoint(5);

            var cell = row.Cells[0];

            // ---------------------------------------------------------
            // Eyebrow
            // ---------------------------------------------------------

            var eyebrow =
                cell.AddParagraph("DOCUMENTO CLÍNICO");

            eyebrow.Format.Font.Size = 6.8;
            eyebrow.Format.Font.Bold = true;
            eyebrow.Format.Font.Color =
                MolaryxPdfTheme.Accent500;

            eyebrow.Format.SpaceBefore = Unit.FromPoint(5);

            eyebrow.Format.SpaceAfter =
                Unit.FromPoint(4);

            // ---------------------------------------------------------
            // Title
            // ---------------------------------------------------------

            var title =
                cell.AddParagraph("Historia clínica");

            title.Format.Font.Size = 22;
            title.Format.Font.Bold = true;
            title.Format.Font.Color =
                MolaryxPdfTheme.Ink50;

            title.Format.SpaceAfter =
                Unit.FromPoint(3);

            // ---------------------------------------------------------
            // Metadata
            // ---------------------------------------------------------

            var metadata =
                cell.AddParagraph();

            metadata.Format.Font.Size = 8;
            metadata.Format.Font.Color =
                MolaryxPdfTheme.Ink200;

            metadata.AddText(
                $"{GetRecordsLabel(information.Records.Count)}");

            metadata.AddText("  ·  ");

            metadata.AddText(
                FormatPeriod(
                    information.From,
                    information.To));

            metadata.AddText("  ·  ");

            metadata.AddText(
                $"Generado {FormatShortDate(information.GeneratedAt)}");

            // ---------------------------------------------------------
            // Coral accent
            // ---------------------------------------------------------

            var accent =
                cell.AddParagraph();

            accent.Format.Borders.Bottom.Width = 2;

            accent.Format.Borders.Bottom.Color =
                MolaryxPdfTheme.Coral500;

            accent.Format.SpaceBefore =
                Unit.FromPoint(9);

            accent.Format.SpaceAfter =
                Unit.FromPoint(1);

            accent.Format.LineSpacingRule =
                LineSpacingRule.Exactly;

            accent.Format.LineSpacing =
                Unit.FromPoint(2);
        }

        // =====================================================================
        // PATIENT
        // =====================================================================

        private static void DrawPatientIdentity(
            Section section,
            ClinicalHistoryTemplateInformation information)
        {
            var label =
                section.AddParagraph("PACIENTE");

            label.Style = "SectionLabel";

            var table = section.AddTable();

            table.AddColumn(
                Unit.FromCentimeter(9.5));

            table.AddColumn(
                MolaryxPdfTheme.ContentWidth -
                Unit.FromCentimeter(9.5));

            table.Format.SpaceAfter =
                Unit.FromPoint(6);

            // ---------------------------------------------------------
            // Main identity
            // ---------------------------------------------------------

            var identityRow = table.AddRow();

            identityRow.BottomPadding =
                Unit.FromPoint(8);

            var identity =
                identityRow.Cells[0];

            var fullName =
                $"{information.PatientName} " +
                $"{information.PatientSurname}".Trim();

            var name =
                identity.AddParagraph(
                    fullName.ToUpperInvariant());

            name.Format.Font.Size = 15;
            name.Format.Font.Bold = true;
            name.Format.Font.Color =
                MolaryxPdfTheme.Ink50;

            name.Format.SpaceAfter =
                Unit.FromPoint(3);

            var identification =
                identity.AddParagraph();

            identification.Format.Font.Size = 8;
            identification.Format.Font.Color =
                MolaryxPdfTheme.Ink100;

            var identificationText =
                FormatIdentification(
                    information.PatientIdentificationType,
                    information.PatientIdentificationNumber);

            identification.AddText(
                identificationText ?? "Identificación no registrada");

            // ---------------------------------------------------------
            // Right side
            // ---------------------------------------------------------

            var side =
                identityRow.Cells[1];

            AddPatientHighlight(
                side,
                "Fecha de nacimiento",
                information.PatientBirthDate.ToString(
                    "dd MMM yyyy",
                    EsCo));

            // ---------------------------------------------------------
            // Secondary information
            // ---------------------------------------------------------

            var detailsRow = table.AddRow();

            detailsRow.TopPadding =
                Unit.FromPoint(7);

            detailsRow.BottomPadding =
                Unit.FromPoint(7);

            detailsRow.Borders.Top.Width =
                0.5;

            detailsRow.Borders.Top.Color =
                MolaryxPdfTheme.Ink800;

            var phone =
                detailsRow.Cells[0];

            AddPatientMeta(
                phone,
                "TELÉFONO",
                information.PatientPhoneNumber);

            var email =
                detailsRow.Cells[1];

            AddPatientMeta(
                email,
                "CORREO ELECTRÓNICO",
                information.PatientEmail);
        }

        // =====================================================================
        // TIMELINE
        // =====================================================================

        private static readonly Unit TimelineDotWidth = Unit.FromPoint(14);
        private static readonly Unit TimelineDateWidth = Unit.FromPoint(58);

        private static void DrawClinicalTimeline(
            Section section,
            ClinicalHistoryTemplateInformation information)
        {
            var label =
                section.AddParagraph("EVOLUCIÓN CLÍNICA");

            label.Style = "SectionLabel";

            if (information.Records.Count == 0)
            {
                DrawEmptyState(section);
                return;
            }

            var timeline = section.AddTable();
            timeline.AddColumn(TimelineDotWidth);
            timeline.AddColumn(TimelineDateWidth);
            timeline.AddColumn(
                MolaryxPdfTheme.ContentWidth -
                TimelineDotWidth -
                TimelineDateWidth);

            timeline.Format.SpaceAfter = Unit.FromPoint(2);

            for (var index = 0;
                 index < information.Records.Count;
                 index++)
            {
                DrawTimelineRecord(
                    timeline,
                    information.Records[index],
                    index + 1,
                    information.Records.Count);
            }
        }

        private static void DrawTimelineRecord(
            Table timeline,
            ClinicalHistoryRecordInformation record,
            int index,
            int totalRecords)
        {
            var row = timeline.AddRow();

            row.TopPadding =
                Unit.FromPoint(index == 1 ? 3 : 0);

            row.BottomPadding = Unit.FromPoint(0);

            row.VerticalAlignment =
                VerticalAlignment.Top;

            DrawTimelineDot(row.Cells[0]);
            DrawTimelineDateColumn(row.Cells[1], record.RecordedAt);
            DrawTimelineRecordBody(row.Cells[2], record, index, totalRecords);
        }

        private static void DrawTimelineDot(Cell dotCell)
        {
            dotCell.Format.Alignment = ParagraphAlignment.Center;
            dotCell.VerticalAlignment = VerticalAlignment.Top;

            var dot = dotCell.AddParagraph("●");
            dot.Format.Font.Size = 8;
            dot.Format.Font.Color = MolaryxPdfTheme.Accent500;
            dot.Format.Alignment = ParagraphAlignment.Center;
            dot.Format.SpaceBefore = Unit.FromPoint(0);
            dot.Format.SpaceAfter = Unit.FromPoint(0);
        }

        private static void DrawTimelineDateColumn(
            Cell dateCell,
            DateTime recordedAt)
        {
            dateCell.Format.Alignment = ParagraphAlignment.Left;
            dateCell.VerticalAlignment = VerticalAlignment.Top;

            var dayMonth =
                dateCell.AddParagraph(
                    FormatTimelineDayMonth(recordedAt));

            dayMonth.Format.Font.Size = 7.5;
            dayMonth.Format.Font.Bold = true;
            dayMonth.Format.Font.Color = MolaryxPdfTheme.Ink50;
            dayMonth.Format.SpaceAfter = Unit.FromPoint(1);

            var year =
                dateCell.AddParagraph(
                    recordedAt.ToString("yyyy", EsCo));

            year.Format.Font.Size = 7;
            year.Format.Font.Bold = true;
            year.Format.Font.Color = MolaryxPdfTheme.Ink300;
            year.Format.SpaceAfter = Unit.FromPoint(2);

            var time =
                dateCell.AddParagraph(
                    recordedAt.ToString("HH:mm", EsCo));

            time.Format.Font.Size = 7.5;
            time.Format.Font.Color = MolaryxPdfTheme.Ink300;
        }

        private static void DrawTimelineRecordBody(
            Cell body,
            ClinicalHistoryRecordInformation record,
            int index,
            int totalRecords)
        {
            body.Format.LeftIndent = Unit.FromPoint(4);
            body.Format.RightIndent = Unit.FromPoint(1);
            body.VerticalAlignment = VerticalAlignment.Top;

            var heading =
                body.AddParagraph(
                    GetRecordTitle(record));

            heading.Format.Font.Size = 10.5;
            heading.Format.Font.Bold = true;
            heading.Format.Font.Color =
                MolaryxPdfTheme.Ink50;

            if (index > 1)
            {
                heading.Format.SpaceBefore =
                    Unit.FromPoint(10);
            }

            heading.Format.SpaceAfter =
                Unit.FromPoint(2);

            var professionalName =
                $"{record.CreatedByName} " +
                $"{record.CreatedBySurname}".Trim();

            var professional =
                body.AddParagraph(
                    $"Profesional · {professionalName}");

            professional.Format.Font.Size = 7.5;
            professional.Format.Font.Color =
                MolaryxPdfTheme.Ink300;

            professional.Format.SpaceAfter =
                Unit.FromPoint(5);

            if (!string.IsNullOrWhiteSpace(record.Reference))
            {
                var reference =
                    body.AddParagraph();

                reference.Format.Font.Size = 7;

                var referenceText =
                    reference.AddFormattedText(
                        record.Reference.Trim(),
                        TextFormat.Bold);

                referenceText.Font.Size = 7;
                referenceText.Font.Color =
                    MolaryxPdfTheme.Accent500;

                reference.Format.SpaceAfter =
                    Unit.FromPoint(5);
            }

            DrawTimelineClinicalField(
                body,
                "Motivo de consulta",
                record.Reason,
                emphasized: true);

            DrawTimelineClinicalField(
                body,
                "Diagnóstico",
                record.Diagnosis);

            DrawTimelineClinicalField(
                body,
                "Evolución",
                record.Evolution);

            DrawTimelineClinicalField(
                body,
                "Notas adicionales",
                record.Notes);

            if (index < totalRecords)
            {
                var separator =
                    body.AddParagraph();

                separator.Format.Borders.Bottom.Width =
                    0.5;

                separator.Format.Borders.Bottom.Color =
                    MolaryxPdfTheme.Ink800;

                separator.Format.SpaceBefore =
                    Unit.FromPoint(9);

                separator.Format.SpaceAfter =
                    Unit.FromPoint(1);
            }
        }

        // =====================================================================
        // CLINICAL FIELD
        // =====================================================================

        private static void DrawTimelineClinicalField(
            Cell cell,
            string label,
            string? value,
            bool emphasized = false)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var labelParagraph =
                cell.AddParagraph(
                    label.ToUpperInvariant());

            labelParagraph.Format.Font.Size = 6.8;
            labelParagraph.Format.Font.Bold = true;

            labelParagraph.Format.Font.Color =
                emphasized
                    ? MolaryxPdfTheme.Accent500
                    : MolaryxPdfTheme.Ink50;

            labelParagraph.Format.SpaceBefore =
                Unit.FromPoint(7);

            labelParagraph.Format.SpaceAfter =
                Unit.FromPoint(2);

            if (emphasized)
            {
                labelParagraph.Format.LeftIndent =
                    Unit.FromPoint(6);

                labelParagraph.Format.Borders.Left.Width =
                    2;

                labelParagraph.Format.Borders.Left.Color =
                    MolaryxPdfTheme.Accent500;

                labelParagraph.Format.Borders.DistanceFromLeft =
                    Unit.FromPoint(5);
            }

            var valueParagraph =
                cell.AddParagraph(value.Trim());

            valueParagraph.Format.Font.Size =
                emphasized ? 9.2 : 8.8;

            valueParagraph.Format.Font.Color =
                emphasized ? MolaryxPdfTheme.Ink100 : MolaryxPdfTheme.Ink200;

            valueParagraph.Format.SpaceAfter =
                Unit.FromPoint(2);
        }

        // =====================================================================
        // EMPTY STATE
        // =====================================================================

        private static void DrawEmptyState(
            Section section)
        {
            var table = section.AddTable();

            table.AddColumn(
                MolaryxPdfTheme.ContentWidth);

            table.Format.SpaceBefore =
                Unit.FromPoint(3);

            var row =
                table.AddRow();

            row.TopPadding =
                Unit.FromPoint(18);

            row.BottomPadding =
                Unit.FromPoint(18);

            row.Borders.Top.Width =
                0.5;

            row.Borders.Bottom.Width =
                0.5;

            row.Borders.Top.Color =
                MolaryxPdfTheme.Ink800;

            row.Borders.Bottom.Color =
                MolaryxPdfTheme.Ink800;

            var cell =
                row.Cells[0];

            var title =
                cell.AddParagraph(
                    "Sin registros en este periodo");

            title.Format.Font.Size = 10.5;
            title.Format.Font.Bold = true;
            title.Format.Font.Color =
                MolaryxPdfTheme.Ink50;

            title.Format.Alignment =
                ParagraphAlignment.Center;

            title.Format.SpaceAfter =
                Unit.FromPoint(4);

            var subtitle =
                cell.AddParagraph(
                    "No se encontraron evoluciones clínicas " +
                    "para el paciente en el rango de fechas seleccionado.");

            subtitle.Format.Font.Size = 8;
            subtitle.Format.Font.Color =
                MolaryxPdfTheme.Ink300;

            subtitle.Format.Alignment =
                ParagraphAlignment.Center;
        }

        // =====================================================================
        // SMALL COMPONENTS
        // =====================================================================

        private static void AddLogoCell(
            Cell cell,
            Unit logoWidth,
            double fallbackFontSize)
        {
            if (File.Exists(MolaryxPdfTheme.LogoPath))
            {
                var logo =
                    cell.AddImage(
                        MolaryxPdfTheme.LogoPath);

                logo.Width = logoWidth;
                logo.LockAspectRatio = true;

                return;
            }

            var brandMark =
                cell.AddParagraph("M");

            brandMark.Format.Font.Name =
                PdfFontBootstrap.ActiveFamily;

            brandMark.Format.Font.Size =
                fallbackFontSize;

            brandMark.Format.Font.Bold = true;

            brandMark.Format.Font.Color =
                MolaryxPdfTheme.Accent500;

            brandMark.Format.Alignment =
                ParagraphAlignment.Center;
        }

        private static void AddAccentLine(
            Section section)
        {
            var line =
                section.AddTable();

            line.AddColumn(
                MolaryxPdfTheme.ContentWidth);

            line.Format.SpaceAfter =
                Unit.FromPoint(4);

            var row =
                line.AddRow();

            row.Height =
                Unit.FromPoint(1.5);

            row.Cells[0].Shading.Color =
                MolaryxPdfTheme.Accent400;
        }

        private static void AddHeaderMeta(
            Cell cell,
            string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            var paragraph =
                cell.AddParagraph(
                    value.Trim());

            paragraph.Format.Font.Size = 6.8;
            paragraph.Format.Font.Color =
                MolaryxPdfTheme.Ink300;

            paragraph.Format.SpaceAfter =
                Unit.FromPoint(1);
        }

        private static void AddPatientHighlight(
            Cell cell,
            string label,
            string? value)
        {
            var labelParagraph =
                cell.AddParagraph(
                    label.ToUpperInvariant());

            labelParagraph.Format.Font.Size = 6.5;
            labelParagraph.Format.Font.Bold = true;
            labelParagraph.Format.Font.Color =
                MolaryxPdfTheme.Ink300;

            labelParagraph.Format.SpaceAfter =
                Unit.FromPoint(2);

            var valueParagraph =
                cell.AddParagraph(
                    string.IsNullOrWhiteSpace(value)
                        ? "—"
                        : value.Trim());

            valueParagraph.Format.Font.Size = 8.5;
            valueParagraph.Format.Font.Bold = true;
            valueParagraph.Format.Font.Color =
                MolaryxPdfTheme.Ink50;
        }

        private static void AddPatientMeta(
            Cell cell,
            string label,
            string? value)
        {
            var labelParagraph =
                cell.AddParagraph(label);

            labelParagraph.Format.Font.Size = 6.5;
            labelParagraph.Format.Font.Bold = true;
            labelParagraph.Format.Font.Color =
                MolaryxPdfTheme.Ink300;

            labelParagraph.Format.SpaceAfter =
                Unit.FromPoint(2);

            var valueParagraph =
                cell.AddParagraph(
                    string.IsNullOrWhiteSpace(value)
                        ? "—"
                        : value.Trim());

            valueParagraph.Format.Font.Size = 8.5;
            valueParagraph.Format.Font.Color =
                MolaryxPdfTheme.Ink50;
        }

        // =====================================================================
        // FORMATTERS
        // =====================================================================

        private static string GetRecordsLabel(
            int count)
        {
            return $"{count} registro{(count == 1 ? "" : "s")}";
        }

        private static string GetRecordTitle(
            ClinicalHistoryRecordInformation record)
        {
            if (!string.IsNullOrWhiteSpace(record.ServiceName))
            {
                return record.ServiceName.Trim();
            }

            if (!string.IsNullOrWhiteSpace(record.Reference))
            {
                return record.Reference.Trim();
            }

            return "Evolución clínica";
        }

        private static string FormatTimelineDayMonth(
            DateTime value)
        {
            return value
                .ToString("dd MMM", EsCo)
                .ToUpperInvariant();
        }

        private static string FormatShortDate(
            DateTime value)
        {
            return value.ToString(
                "dd MMM yyyy",
                EsCo);
        }

        private static string JoinMeta(
            params string?[] values)
        {
            return string.Join(
                " · ",
                values
                    .Where(v =>
                        !string.IsNullOrWhiteSpace(v))
                    .Select(v =>
                        v!.Trim()));
        }

        private static string? FormatIdentification(
            string? type,
            string? number)
        {
            if (string.IsNullOrWhiteSpace(type) &&
                string.IsNullOrWhiteSpace(number))
            {
                return null;
            }

            return $"{type}: {number}".Trim();
        }

        private static string FormatDateTime(
            DateTime value)
        {
            return value.ToString(
                "d MMM yyyy · HH:mm",
                EsCo);
        }

        private static string FormatPeriod(
            DateTime? from,
            DateTime? to)
        {
            if (from is null && to is null)
            {
                return "Historial completo";
            }

            if (from is not null &&
                to is not null)
            {
                return
                    $"{from.Value:dd/MM/yyyy} — " +
                    $"{to.Value:dd/MM/yyyy}";
            }

            if (from is not null)
            {
                return $"Desde {from.Value:dd/MM/yyyy}";
            }

            return $"Hasta {to!.Value:dd/MM/yyyy}";
        }
    }
}