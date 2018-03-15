using System;
using System.Collections.Generic;

namespace Birder2
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public int FirstRowOnPage
        {

            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }

    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }
}


//public class PaginatedList<T> : List<T>
//{
//    public int PageIndex { get; private set; }
//    public int TotalPages { get; private set; }

//    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
//    {
//        PageIndex = pageIndex;
//        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

//        this.AddRange(items);
//    }

//    public bool HasPreviousPage
//    {
//        get
//        {
//            return (PageIndex > 1);
//        }
//    }

//    public bool HasNextPage
//    {
//        get
//        {
//            return (PageIndex < TotalPages);
//        }
//    }

//    public static async Task<PaginatedList<T>> CreateAsync(
//        IQueryable<T> source, int pageIndex, int pageSize)
//    {
//        var count = await source.CountAsync();
//        var items = await source.Skip(
//            (pageIndex - 1) * pageSize)
//            .Take(pageSize).ToListAsync();
//        return new PaginatedList<T>(items, count, pageIndex, pageSize);
//    }
//}
