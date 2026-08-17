namespace Domain.Common
{
    public static class SearchText
    {
        public static string[] Tokens(string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return [];
            }

            return [.. search
                .Trim()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(token => token.ToLowerInvariant())
                .Where(token => token.Length > 0)
                .Distinct()];
        }
    }
}
