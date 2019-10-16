using Administration.Core.Model.Base;
using Administration.Core.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Administration.API.Commands.Base
{
	public static class CommandExtensions
	{
		public static ICommand<TRepository, TEntity> InTransactionScope<TRepository, TEntity>(
			this ICommand<TRepository, TEntity> command)
			where TEntity : IAggregateRoot
			where TRepository : IRepository<TEntity>
		{
			return new TransactionalCommand<TRepository, TEntity>(command);
		}

		public static ICommand<TRepository, TEntity, TResult> InTransactionScope<TRepository, TEntity, TResult>(
			this ICommand<TRepository, TEntity, TResult> command)
			where TEntity : IAggregateRoot
			where TRepository : IRepository<TEntity>
		{
			return new TransactionalCommand<TRepository, TEntity, TResult>(command);
		}
	}
}