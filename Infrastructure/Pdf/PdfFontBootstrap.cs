using MigraDoc;
using PdfSharp.Fonts;

namespace Infrastructure.Pdf
{
    public static class PdfFontBootstrap
    {
        private static bool _configured;

        public static string ActiveFamily { get; private set; } = "Arial";

        public static bool UsesEmbeddedFonts { get; private set; }

        public static void Configure()
        {
            if (_configured)
            {
                return;
            }

            var regularFontPath = Path.Combine(
                MolaryxPdfFonts.FontsDirectory,
                MolaryxPdfFonts.RegularFileName);

            UsesEmbeddedFonts = File.Exists(regularFontPath);

            if (UsesEmbeddedFonts)
            {
                GlobalFontSettings.FontResolver = new MolaryxFontResolver();
                ActiveFamily = MolaryxPdfFonts.PrimaryFamily;
            }
            else if (OperatingSystem.IsWindows())
            {
                GlobalFontSettings.UseWindowsFontsUnderWindows = true;
                ActiveFamily = "Arial";
            }
            else
            {
                throw new InvalidOperationException(
                    $"No se encontró la fuente del PDF. Agrega '{MolaryxPdfFonts.RegularFileName}' en '{MolaryxPdfFonts.FontsDirectory}'.");
            }

            PredefinedFontsAndChars.ErrorFontName = ActiveFamily;
            PredefinedFontsAndChars.Bullets.Level1FontName = ActiveFamily;
            PredefinedFontsAndChars.Bullets.Level2FontName = ActiveFamily;
            PredefinedFontsAndChars.Bullets.Level3FontName = ActiveFamily;

            _configured = true;
        }
    }
}
