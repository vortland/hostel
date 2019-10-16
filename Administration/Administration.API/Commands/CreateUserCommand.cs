using Administration.API.Commands.Base;
using Administration.API.Models.InputResources;
using Administration.Core.Exceptions;
using Administration.Core.Model;
using Administration.Core.Repositories;

namespace Administration.API.Commands
{
	public class CreateUserCommand : ICommand<IUserRepository, User>
	{
		private readonly UserRegistrationInput _userInput;

		public CreateUserCommand(UserRegistrationInput userInput)
		{
			Guard.IsNotNull(userInput, nameof(userInput));
			_userInput = userInput;
		}

		public void Execute(IUserRepository repository)
		{
			repository.Create(_userInput.Login, _userInput.Password);

			repository.SaveChanges();
		}
	}
}
