using System.ComponentModel.DataAnnotations;

namespace Administration.API.Models.InputResources
{
	public class UserRegistrationInput
	{
		[Required]
		public string Login { get; set; }
		[Required]
		[StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
		public string Password { get; set; }
	}
}
