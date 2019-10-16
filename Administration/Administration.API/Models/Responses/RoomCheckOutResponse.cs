using Administration.API.Models.Responses.Base;

namespace Administration.API.Models.Responses
{
	public class RoomCheckOutResponse : RestResponse
	{
		public decimal TotalCost { get; set; }
	}
}
