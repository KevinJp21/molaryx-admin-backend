namespace Application.Common.Pagination
{
    public static class PaginationHelper
    {
        public static int GetEffectivePage(int? page) =>
            page is > 0 ? page.Value : PaginationDefaults.DefaultPage;

        public static int GetEffectivePageSize(int? size) =>
            size is > 0
                ? Math.Min(size.Value, PaginationDefaults.MaxSize)
                : PaginationDefaults.DefaultSize;
    }
}