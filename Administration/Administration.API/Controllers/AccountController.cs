using Administration.API.Commands;
using Administration.API.Infrastructure.Authentication;
using Administration.API.Models.InputResources;
using Administration.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Administration.API.Controllers
{
	[Route("api/[controller]")]
	[AllowAnonymous]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IJwtSigningEncodingKey _signingEncodingKey;
		private readonly IConfiguration _configuration;
		private readonly IUserRepository _userRepository;

		public AccountController(IJwtSigningEncodingKey signingEncodingKey,
			IConfiguration configuration, IUserRepository userRepository)
		{
			_signingEncodingKey = signingEncodingKey;
			_configuration = configuration;
			_userRepository = userRepository;
		}


		[Route("signin")]
		[HttpPost]
		public ActionResult SignIn([FromBody] UserSignInInput signInInput)
		{
			if (signInInput == null)
			{
				return BadRequest("Wrong input parameters");
			}

			var token = new SignInUserCommand(signInInput, _signingEncodingKey)
				.Execute(_userRepository);

			return Ok(token);
		}

		[Route("register")]
		[HttpPost]
		public ActionResult Register([FromBody]UserRegistrationInput registrationInput)
		{
			if (registrationInput == null)
			{
				return BadRequest("Wrong input parameters");
			}

			new CreateUserCommand(registrationInput)
				.Execute(_userRepository);

			return Ok();
		}
	}
}