using Administration.Core.Model.Base;
using Administration.Core.Repositories.Base;

namespace Administration.API.Commands.Base
{
	public interface ICommand<in TRepository, TEntity>
		where TRepository : IRepository<TEntity> 
		where TEntity : IAggregateRoot
	{
		void Execute(TRepository repository);
	}
}