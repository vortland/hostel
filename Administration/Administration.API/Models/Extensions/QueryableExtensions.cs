using System;
using System.Linq;
using System.Linq.Expressions;
using Administration.Core.Exceptions;

namespace Administration.API.Models.Extensions
{
	public static class QueryableExtensions
	{
		public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
		{
			Guard.IsNotNull(query, nameof(query));
			Guard.IsNotNull(predicate, nameof(predicate));

			return condition
				? query.Where(predicate)
				: query;
		}

		public static IQueryable<T> PageIf<T>(this IQueryable<T> query, bool condition, int index, int size)
		{
			Guard.IsNotNull(query, nameof(query));

			return condition
				? query.Page(index, size)
				: query;
		}

		public static IQueryable<T> Page<T>(this IQueryable<T> query, int index, int size)
		{
			Guard.IsNotNull(query, nameof(query));

			return query.Skip(index * size).Take(size);
		}
	}
}
