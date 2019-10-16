using Administration.Core.Model.Base;
using Administration.Core.Repositories.Base;

namespace Administration.API.Queries.Base
{
	public interface IQuery<in TRepository, TEntity, out TResult>
		where TRepository : IRepository<TEntity>
		where TEntity : IAggregateRoot
	{
		TResult Execute(TRepository context);
	}
}