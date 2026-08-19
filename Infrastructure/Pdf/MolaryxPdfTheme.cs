using MigraDoc.DocumentObjectModel;

namespace Infrastructure.Pdf
{
    internal static class MolaryxPdfTheme
    {
        public const string BrandName = "Molaryx";

        public static readonly Color Accent500 = new(124, 77, 255);
        public static readonly Color Accent400 = new(155, 120, 255);
        public static readonly Color Accent50 = new(245, 242, 255);
        public static readonly Color Coral500 = new(246, 71, 143);
        public static readonly Color Ink50 = new(14, 14, 23);
        public static readonly Color Ink100 = new(35, 35, 50);
        public static readonly Color Ink200 = new(61, 61, 77);
        public static readonly Color Ink300 = new(92, 92, 110);
        public static readonly Color Ink400 = new(119, 119, 138);
        public static readonly Color Ink700 = new(214, 214, 226);
        public static readonly Color Ink800 = new(234, 234, 241);
        public static readonly Color Ink900 = new(247, 247, 251);
        public static readonly Color White = new(255, 255, 255);

        public static readonly string LogoPath = Path.Combine(
            AppContext.BaseDirectory,
            "Infrastructure",
            "Pdf",
            "Assets",
            "molaryx-mark.png");

        public static readonly Unit ContentWidth = Unit.FromCentimeter(17);
        public static readonly Unit ContentPadding = Unit.FromPoint(14);
        public static readonly Unit HalfContentWidth = Unit.FromCentimeter(8.5);
        public static readonly Unit LogoWidth = Unit.FromCentimeter(1.15);
    }
}
