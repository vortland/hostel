using Administration.Core.Exceptions;
using Administration.Core.Model.Base;

namespace Administration.Core.Model
{
	public class User : Entity, IAggregateRoot
	{
		private string _login;
		private string _passwordHash;


		public string Login
		{
			get => _login;
			protected set => _login = value;
		}

		public string PasswordHash
		{
			get => _passwordHash;
			protected set => _passwordHash = value;
		}


		public User(string login, string password)
		{
			Guard.IsNotNullOrEmpty(login, nameof(login));
			Guard.IsNotNullOrEmpty(password, nameof(password));

			_login = login;
			_passwordHash = password;
		}

		protected User()
		{
		}
	}
}
