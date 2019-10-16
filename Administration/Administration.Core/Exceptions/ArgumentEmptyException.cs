using System;
using System.Runtime.Serialization;
using System.Security;

namespace Administration.Core.Exceptions
{
	[Serializable]
	public class ArgumentEmptyException : ArgumentException
	{
		#region Constants

		private const string _messasgeDefault = "Value cannot be empty.";

		#endregion

		#region Constructors

		public ArgumentEmptyException()
			: base(_messasgeDefault)
		{
		}

		public ArgumentEmptyException(string message, Exception innerException)
			: base(string.IsNullOrWhiteSpace(message) ? _messasgeDefault : message, innerException)
		{
		}

		public ArgumentEmptyException(string paramName)
			: this(paramName, (string)null)
		{
		}

		public ArgumentEmptyException(string paramName, string message)
			: base(string.IsNullOrWhiteSpace(message) ? _messasgeDefault : message, paramName)
		{
		}

		[SecurityCritical]
		protected ArgumentEmptyException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		#endregion
	}
}
