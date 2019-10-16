using Newtonsoft.Json;

namespace Administration.API.Models.Responses
{
	public class ValidationErrorResponse 
	{
		[JsonProperty]
		public int ErrorCode { get; protected set; }
		
		[JsonProperty]
		public string Message { get; protected set; }

		public ValidationErrorResponse(string message) : this(0, message)
		{
		}

		public ValidationErrorResponse(int errorCode, string message)
		{
			ErrorCode = errorCode;
			Message = message;
		}
	}
}
