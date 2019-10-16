using System.Transactions;
using Administration.Core.Exceptions;
using Administration.Core.Model.Base;
using Administration.Core.Repositories.Base;

namespace Administration.API.Commands.Base
{
	public class TransactionalCommand<TRepository, TEntity, TResult> : ICommand<TRepository, TEntity, TResult>
		where TRepository : IRepository<TEntity>
		where TEntity : IAggregateRoot
	{
		private readonly ICommand<TRepository, TEntity, TResult> _inner;

		public TransactionalCommand(ICommand<TRepository, TEntity, TResult> inner)
		{
			Guard.IsNotNull(inner, nameof(inner));
			_inner = inner;
		}

		public TResult Execute(TRepository repository)
		{
			using (var tx = new TransactionScope(TransactionScopeOption.Required,
						new TransactionOptions
						{
							IsolationLevel = IsolationLevel.ReadCommitted,
							Timeout = TransactionManager.DefaultTimeout
						}))
			{
				var result = _inner.Execute(repository);
				tx.Complete();

				return result;
			}
		}
	}
}