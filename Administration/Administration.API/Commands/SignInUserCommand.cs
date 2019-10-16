using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Administration.API.Commands.Base;
using Administration.API.Infrastructure.Authentication;
using Administration.API.Models.InputResources;
using Administration.Core.Exceptions;
using Administration.Core.Model;
using Administration.Core.Repositories;
using Administration.Core.Resources;
using Microsoft.IdentityModel.Tokens;

namespace Administration.API.Commands
{
	public class SignInUserCommand : ICommand<IUserRepository, User, string>
	{
		private readonly UserSignInInput _signInInput;
		private readonly IJwtSigningEncodingKey _signingEncodingKey;

		public SignInUserCommand(UserSignInInput signInInput, IJwtSigningEncodingKey signingEncodingKey)
		{
			Guard.IsNotNull(signInInput, nameof(signInInput));
			Guard.IsNotNull(signingEncodingKey, nameof(signingEncodingKey));

			_signInInput = signInInput;
			_signingEncodingKey = signingEncodingKey;
		}

		public string Execute(IUserRepository repository)
		{
			var user = repository.GetByLogin(_signInInput.Login);

			if (user != null && repository.CheckPassword(user, _signInInput.Password))
			{
				return GenerateJwtToken(user);
			}

			throw new AdministrationDomainException(ValidationCodes.User_CannotSignIn_WrongPassOrLogin,
				ValidationMessages.User_CannotSignIn_WrongPassOrLogin);
		}

		private string GenerateJwtToken(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Login)
			};

			var credentials =
				new SigningCredentials(_signingEncodingKey.GetKey(), _signingEncodingKey.SigningAlgorithm);
			var expires = DateTime.Now.AddMinutes(5);

			var token = new JwtSecurityToken(
				issuer: "DemoApp",
				audience: "DemoAppClient",
				claims: claims,
				expires: expires,
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
