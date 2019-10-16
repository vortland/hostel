
namespace Administration.Core.Resources
{
	public class ValidationCodes
	{
		#region 100 XX XX Room

		public const int Room_CannotCheckedOut_VisitorNotExists = 1000001;
		public const int Room_VisitorNumberOutOfRange = 1000002;
		public const int Room_CannotCreate_NumberNotUnique = 1000003;
		public const int Room_NotFound_WrongId = 1000004;

		#endregion

		#region 101 XX XX Visitor


		#endregion

		#region 102 XX XX User

		public const int User_CannotSignIn_WrongPassOrLogin = 1020001;
		public const int User_CannotCreate_IdentityError = 1020002;

		#endregion
	}
}
