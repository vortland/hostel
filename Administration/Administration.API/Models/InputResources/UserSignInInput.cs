using System.ComponentModel.DataAnnotations;

namespace Administration.API.Models.InputResources
{
	public class UserSignInInput
	{
		[Required]
		public string Login { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
