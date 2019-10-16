using Administration.Core.Model.Base;

namespace Administration.Core.Repositories.Base
{
	public interface IRepository<T> where T : IAggregateRoot
	{
		void SaveChanges();
	}
}
