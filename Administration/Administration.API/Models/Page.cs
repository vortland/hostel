using System.Collections.Generic;
using Administration.Core.Exceptions;

namespace Administration.API.Models
{
	public class Page<T>
	{
		#region Fields

		private readonly IEnumerable<T> _items;
		private readonly int _totalCount;
		private readonly int _currentPage;

		#endregion Fields

		#region Properties

		public IEnumerable<T> Items => _items;

		public int TotalCount => _totalCount;

		public int CurrentPage => _currentPage;

		#endregion Properties

		#region Constructors

		public Page(IEnumerable<T> items, int totalCount, int currentPage)
		{
			Guard.IsNotNull(items, nameof(items));

			_items = items;
			_totalCount = totalCount;
			_currentPage = currentPage;
		}

		public Page(ICollection<T> items)
		{
			Guard.IsNotNull(items, nameof(items));

			_items = items;
			_totalCount = items.Count;
		}

		#endregion Constructors
	}
}