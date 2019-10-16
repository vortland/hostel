using System;
using System.Linq;
using System.Linq.Expressions;
using Administration.API.Models;
using Administration.API.Models.Extensions;
using Administration.Core.Model.Base;
using Administration.Core.Repositories.Base;

namespace Administration.API.Queries.Base
{
	public abstract class BaseSearchQuery<TRepository, TEntity, TResult> : IQuery<TRepository, TEntity, Page<TResult>>
		where TRepository : IRepository<TEntity>
		where TEntity : IAggregateRoot
	{
		#region Fields

		private readonly bool _pagingApplied;
		private readonly int _pageIndex;
		private readonly int _pageSize;
		private readonly Sorting _sorting;
		private readonly Expression<Func<TResult, object>> _orderByExpression;

		#endregion Fields

		#region Constructors

		protected BaseSearchQuery( bool pagingApplied, int pageIndex, int pageSize)
			: this(pagingApplied, pageIndex, pageSize, null )
		{ }

		protected BaseSearchQuery(bool pagingApplied, int pageIndex, int pageSize,
			Expression<Func<TResult, object>> orderByExpression)
			: this(pagingApplied, pageIndex, pageSize, orderByExpression, Sorting.Asc)
		{
		}


		protected BaseSearchQuery(bool pagingApplied, int pageIndex, int pageSize,
			Expression<Func<TResult, object>> orderByExpression, Sorting sorting)
		{
			_pagingApplied = pagingApplied;
			_pageIndex = pageIndex;
			_pageSize = pageSize;
			_orderByExpression = orderByExpression;
			_sorting = sorting;
		}
		
		#endregion Constructors

		#region Public API

		public Page<TResult> Execute(TRepository repository)
		{
			OnExecuting(repository);
			var result = ExecuteCore(repository);
			OnExecuted(result);

			return result;
		}

		#endregion Public API

		#region Implementation

		private Page<TResult> ExecuteCore(TRepository repository)
		{
			Page<TResult> result;
			var query = GetQueryable(repository);
			if (!_pagingApplied)
			{
				result = new Page<TResult>(ApplyOrder(query).ToList());
			}
			else
			{
				var count = query.Count();
				var entries = ApplyOrder(query)
					.Page(_pageIndex, _pageSize)
					.ToList();

				result = new Page<TResult>(entries, count, _pageIndex);
			}
			return result;
		}

		#endregion Implementation

		#region Overridables

		protected abstract IQueryable<TResult> GetQueryable(TRepository repository);

		protected virtual IQueryable<TResult> ApplyOrder(IQueryable<TResult> queryable)
		{
			return _orderByExpression == null ? 
				queryable:
					_sorting == Sorting.Asc?
						queryable.OrderBy(_orderByExpression):
						queryable.OrderByDescending(_orderByExpression);
		}

		protected virtual void OnExecuting(TRepository repository)
		{ }

		protected virtual void OnExecuted(Page<TResult> result)
		{ }


		#endregion Overridables
	}
}