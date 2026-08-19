using PdfSharp.Fonts;

namespace Infrastructure.Pdf
{
    public sealed class MolaryxFontResolver : IFontResolver
    {
        private const string RegularFace = "molaryx-regular";
        private const string BoldFace = "molaryx-bold";

        private static string RegularFontPath =>
            Path.Combine(MolaryxPdfFonts.FontsDirectory, MolaryxPdfFonts.RegularFileName);

        private static string BoldFontPath =>
            Path.Combine(MolaryxPdfFonts.FontsDirectory, MolaryxPdfFonts.BoldFileName);

        public FontResolverInfo? ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (!HasEmbeddedFonts() || !IsSupportedFamily(familyName))
            {
                return null;
            }

            if (isBold && File.Exists(BoldFontPath))
            {
                return new FontResolverInfo(BoldFace, false, isItalic);
            }

            return new FontResolverInfo(RegularFace, isBold, isItalic);
        }

        public byte[]? GetFont(string faceName)
        {
            var fontPath = faceName switch
            {
                RegularFace => RegularFontPath,
                BoldFace => BoldFontPath,
                _ => null
            };

            if (fontPath is null || !File.Exists(fontPath))
            {
                return null;
            }

            return File.ReadAllBytes(fontPath);
        }

        private static bool HasEmbeddedFonts() => File.Exists(RegularFontPath);

        private static bool IsSupportedFamily(string familyName) =>
            familyName.Equals(MolaryxPdfFonts.PrimaryFamily, StringComparison.OrdinalIgnoreCase)
            || familyName.Equals("Arial", StringComparison.OrdinalIgnoreCase)
            || familyName.Equals("Courier New", StringComparison.OrdinalIgnoreCase);
    }
}
