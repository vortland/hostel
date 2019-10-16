using System;

namespace Administration.API.Infrastructure.Exceptions
{
	public class AdministrationApplicationNotFoundException : AdministrationApplicationException
	{
		public AdministrationApplicationNotFoundException(int errorCode) : base(errorCode)
		{
		}

		public AdministrationApplicationNotFoundException(int errorCode, string message) : base(errorCode, message)
		{
		}

		public AdministrationApplicationNotFoundException(int errorCode, string message, Exception innerException) : base(errorCode, message, innerException)
		{
		}
	}
}
