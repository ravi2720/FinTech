using Microsoft.EntityFrameworkCore;

namespace FinTech_ApiPanel.Domain.Shared.Pagination
{
    public class PagedResponse<T>
    {
        public List<T> Data { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalRecords { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasPrevious => PageNumber > 1;
        public bool HasNext => PageNumber < TotalPages;

        public PagedResponse(List<T> data, int totalRecords, int pageNumber, int pageSize)
        {
            Data = data;
            TotalRecords = totalRecords;
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        }

        public static PagedResponse<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var totalRecords = source.Count();
            var data = source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResponse<T>(data, totalRecords, pageNumber, pageSize);
        }
    }
}
