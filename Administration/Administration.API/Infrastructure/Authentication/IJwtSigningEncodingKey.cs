using Microsoft.IdentityModel.Tokens;

namespace Administration.API.Infrastructure.Authentication
{
	public interface IJwtSigningEncodingKey
	{
		string SigningAlgorithm { get; }

		SecurityKey GetKey();
	}
}
