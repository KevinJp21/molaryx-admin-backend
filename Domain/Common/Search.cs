namespace Domain.Common
{
    public static class Search
    {
        public const int MinTokenLength = 3;

        public static string[] GetTokens(string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return [];
            }

            return [.. SearchText.Tokens(search)
                .Where(token => token.Length >= MinTokenLength)];
        }

        public static bool HasValidSearch(string? search)
            => GetTokens(search).Length > 0;

        public static SearchTermKind ClassifyToken(string token)
        {
            if (token.Contains('@'))
            {
                return SearchTermKind.Email;
            }

            if (token.All(char.IsDigit))
            {
                return SearchTermKind.Numeric;
            }

            if (token.Any(char.IsDigit) && token.Any(char.IsLetter))
            {
                return SearchTermKind.Identification;
            }

            return SearchTermKind.Name;
        }
    }
}
