using System.Linq;

namespace System.Collections.Generic
{
	/// <summary>
	/// Basic class for paged content
	/// </summary>
	/// <typeparam name="T">Type that will be paged</typeparam>
    public class PagedList<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _members;
		/// <summary>
		/// Current active page
		/// </summary>
        public int Page { get; private set; }
		/// <summary>
		/// The total amount of pages
		/// </summary>
        public int TotalCount { get; private set; }
		/// <summary>
		/// The amount of items per page
		/// </summary>
        public int PageSize { get; private set; }

		/// <summary>
		/// Create a new paged list
		/// </summary>
		/// <param name="members">Total items</param>
		/// <param name="page">Current page</param>
		/// <param name="totalCount">Total count</param>
		/// <param name="pageSize">Page Size</param>
        public PagedList(IEnumerable<T> members, int page, int totalCount, int pageSize)
        {
            _members = members;
            Page = page;
            TotalCount = totalCount;
            PageSize = pageSize;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _members.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<T> GetItems()
        {
            return this._members;
        }

        public static PagedList<T> Empty(int pageSize = 0)
        {
            return new PagedList<T>(Enumerable.Empty<T>(), 0, 0, pageSize);
        }
    }

    public class PagedList<T, TPageIdentificator> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _members;
        public TPageIdentificator Page { get; private set; }
        public TPageIdentificator NextPage { get; private set; }
        public TPageIdentificator PrevPage { get; private set; }
        public int TotalCount { get; private set; }
        public int PageSize { get; private set; }

        public PagedList(IEnumerable<T> members, TPageIdentificator page, int totalCount, int pageSize, TPageIdentificator nextPage, TPageIdentificator prevPage)
        {
            _members = members;
            Page = page;
            NextPage = nextPage;
            PrevPage = prevPage;
            TotalCount = totalCount;
            PageSize = pageSize;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _members.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<T> GetItems()
        {
            return this._members;
        }

        public static PagedList<T, TPageIdentificator> Empty(int pageSize = 0)
        {
            return new PagedList<T, TPageIdentificator>(Enumerable.Empty<T>(), default(TPageIdentificator), 0, pageSize, default(TPageIdentificator), default(TPageIdentificator));
        }
    }
}
