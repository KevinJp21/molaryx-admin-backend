namespace Application.Common.Pagination
{
    public class PageFilter
    {
        public int? Page { get; set; } = 1;
        public int? Size { get; set; } = 10;
    }
}