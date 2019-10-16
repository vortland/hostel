using System;
using Administration.API.Models.Responses.Base;

namespace Administration.API.Models.Responses
{
	public class VisitorResponse : RestResponse
	{
		public string FullName { get; set; }
		public DateTime CheckInDate { get; set; }
		public DateTime? CheckOutDate { get; set; }
	}
}
