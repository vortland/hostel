using System.Linq;
using Administration.Core.Exceptions;
using Administration.Core.Model;
using Administration.Core.Repositories;
using Administration.Core.Resources;
using Microsoft.AspNetCore.Identity;

namespace Administration.DataAccessLayer.Repositories
{
	public class UserRepository: IUserRepository
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly AdministrationContext _context;

		public UserRepository(UserManager<IdentityUser> userManager, AdministrationContext context)
		{
			Guard.IsNotNull(userManager, nameof(userManager));
			Guard.IsNotNull(context, nameof(context));

			_userManager = userManager;
			_context = context;
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public User Create(string login, string password)
		{
			var identityUser = new IdentityUser(login);
			var identityResult = _userManager.CreateAsync(identityUser, password);

			var result = identityResult.Result;

			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description);

				throw new AdministrationDomainException(ValidationCodes.Room_CannotCreate_NumberNotUnique,
					ValidationMessages.User_CannotCreate_IdentityError + ":" + string.Join("/", errors));
			}

			var user = new User(login, password);

			return _context.Users.Add(user).Entity;
		}

		public User GetByLogin(string login)
		{
			var user = _context.Users.FirstOrDefault(u => u.Login == login);
			return user;
		}

		public bool CheckPassword(User user, string password)
		{
			var identityUser = _userManager.FindByNameAsync(user.Login).Result;

			return _userManager.CheckPasswordAsync(identityUser, password).Result;
		}
	}
}
