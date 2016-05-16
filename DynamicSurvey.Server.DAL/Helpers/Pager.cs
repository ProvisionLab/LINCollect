using System;
using System.Linq;

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
		public int TotalPages { get; private set; }
		public int PageSize { get; set; }

		public int CurrentPage { get; set; }

		public IQueryable<TData> SelectPageQuery<TData, TKey>(IQueryable<TData> source, Func<TData, TKey> orderBy)
		{
			var count = (long)source.Count();
			var pageSizelong = (long)PageSize;
			var d = count / pageSizelong;
			TotalPages = (int) Math.Ceiling((decimal)d);

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
