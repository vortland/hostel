using Administration.Core.Model.Base;
using Administration.Core.Repositories.Base;

namespace Administration.API.Commands.Base
{
	public interface ICommand<in TRepository, in TEntity, out TResult> 
		where TRepository : IRepository<TEntity>
		where TEntity : IAggregateRoot
	{
		TResult Execute(TRepository context);
	}
}