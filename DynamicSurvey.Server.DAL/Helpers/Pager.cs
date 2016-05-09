using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSurvey.Server.DAL.Helpers
{
    public interface IPager
    {
        int TotalPages { get; }

        int PageSize { get; set; }

        int CurrentPage { get; set; }

        IQueryable<TData> SelectPageQuery<TData, TKey>(IQueryable<TData> source, Func<TData, TKey> orderBy);
    }
    public class Pager : IPager
    {
        public int TotalPages  { get; private set; }
        public int PageSize { get; set; }

        public int CurrentPage { get; set; }

        public IQueryable<TData> SelectPageQuery<TData, TKey>(IQueryable<TData> source, Func<TData, TKey> orderBy)
        {
            var count = (decimal)source.Count();
            var pageSizeDecimal = (decimal)PageSize;
            TotalPages =  (int) Math.Ceiling(count / pageSizeDecimal);

            if (CurrentPage < 1)
                CurrentPage = 1;

            if (CurrentPage > TotalPages)
                CurrentPage = TotalPages;

            return source
                .OrderBy(orderBy)
                .AsQueryable()
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize);
        }
    }
}
