using Administration.Core.Model;
using Administration.Core.Repositories.Base;

namespace Administration.Core.Repositories
{
	public interface IUserRepository : IRepository<User>
	{
		User Create(string login, string password);
		User GetByLogin(string login);
		bool CheckPassword(User user, string password);
	}
}
