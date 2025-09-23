namespace FinTech_ApiPanel.Domain.Shared.Pagination
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string OrderBy { get; set; } = "Id desc";

        public PaginationFilter()
        {
            PageNumber = PageNumber < 1 ? 1 : PageNumber;
            PageSize = PageSize > 100 ? 100 : PageSize; // Max limit
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 100 ? 100 : pageSize;
        }
    }

}
