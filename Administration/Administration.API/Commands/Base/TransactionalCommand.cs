using System.Transactions;
using Administration.Core.Exceptions;
using Administration.Core.Model.Base;
using Administration.Core.Repositories.Base;

namespace Administration.API.Commands.Base
{
	public class TransactionalCommand<TRepository, TEntity> : ICommand<TRepository, TEntity>
		where TRepository : IRepository<TEntity>
		where TEntity : IAggregateRoot
	{
		private readonly ICommand<TRepository, TEntity> _inner;

		public TransactionalCommand(ICommand<TRepository, TEntity> inner)
		{
			Guard.IsNotNull(inner, nameof(inner));
			_inner = inner;
		}

		public void Execute(TRepository repository)
		{
			using (var tx = new TransactionScope(TransactionScopeOption.Required,
				new TransactionOptions
				{
					IsolationLevel = IsolationLevel.ReadCommitted,
					Timeout = TransactionManager.DefaultTimeout
				}))
			{
				_inner.Execute(repository);
				tx.Complete();
			}
		}
	}
}