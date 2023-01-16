using Microsoft.EntityFrameworkCore;

namespace InnoGotchi.DataAccess.Models
{
    public class Page<T> : List<T> where T : class
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; }

        private Page(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            (PageNumber, PageSize, TotalCount) = (pageNumber, pageSize, totalCount);
            AddRange(items);
        }

        public int GetTotalPagesCount() => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage() => PageNumber > 1;
        public bool HasNextPage() => PageNumber < GetTotalPagesCount();

        public async static Task<Page<T>> CreateFromQueryAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                throw new ArgumentException("Incorrect page description provided!");

            if (source is IAsyncEnumerable<T>)
            {
                var count = await source.CountAsync();

                if ((int)Math.Ceiling(count / (double)pageSize) < pageNumber)
                    pageNumber = 1;

                var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                return new Page<T>(items, count, pageNumber, pageSize);
            }
            else
            {
                var count = source.Count();

                if ((int)Math.Ceiling(count / (double)pageSize) < pageNumber)
                    pageNumber = 1;

                var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                return new Page<T>(items, count, pageNumber, pageSize);
            }
        }



    }
}
