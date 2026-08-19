namespace Infrastructure.Pdf
{
    public static class MolaryxPdfFonts
    {
        public const string PrimaryFamily = "Molaryx";

        public static readonly string FontsDirectory = Path.Combine(
            AppContext.BaseDirectory,
            "Infrastructure",
            "Pdf",
            "Fonts");

        public const string RegularFileName = "molaryx-regular.ttf";
        public const string BoldFileName = "molaryx-bold.ttf";
    }
}
