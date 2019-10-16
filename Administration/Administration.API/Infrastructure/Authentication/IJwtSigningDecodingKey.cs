using Microsoft.IdentityModel.Tokens;

namespace Administration.API.Infrastructure.Authentication
{
	public interface IJwtSigningDecodingKey
	{
		SecurityKey GetKey();
	}

}
