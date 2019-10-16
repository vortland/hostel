namespace Administration.Core.Resources
{
	public class ValidationMessages
	{
		#region Room

		public const string Room_CannotCheckedOut_VisitorNotExists = "Visitor does not exist";
		public const string Room_VisitorNumberOutOfRange = "Maximum number of visitors exceeded";
		public const string Room_CannotCreate_NumberNotUnique = "Cannot create room. The number is not unique";
		public const string Room_NotFound_WrongId = "Room not found. Wrong room id";
		public const string User_CannotCreate_IdentityError = "Cannot create user";

		#endregion

		#region Visitor


		#endregion

		#region User

		public const string User_CannotSignIn_WrongPassOrLogin = "User cannot sign in. Wrong input credentials";

		#endregion
	}
}
