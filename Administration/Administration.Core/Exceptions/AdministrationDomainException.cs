using System;

namespace Administration.Core.Exceptions
{

	public class AdministrationDomainException : Exception
	{
		public int ErrorCode
		{
			get;
		}

		public AdministrationDomainException(int errorCode)
		{
			ErrorCode = errorCode;
		}

		public AdministrationDomainException(int errorCode, string message)
			: base(message)
		{
			ErrorCode = errorCode;
		}

		public AdministrationDomainException(int errorCode, string message, Exception innerException)
			: base(message, innerException)
		{
			ErrorCode = errorCode;
		}
	}
}
