using System;

namespace Administration.API.Infrastructure.Exceptions
{
	public class AdministrationApplicationException : Exception
	{
		public int ErrorCode
		{
			get;
		}

		public AdministrationApplicationException(int errorCode)
		{
			ErrorCode = errorCode;
		}

		public AdministrationApplicationException(int errorCode, string message)
			: base(message)
		{
			ErrorCode = errorCode;
		}

		public AdministrationApplicationException(int errorCode, string message, Exception innerException)
			: base(message, innerException)
		{
			ErrorCode = errorCode;
		}
	}
}
