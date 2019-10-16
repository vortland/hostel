using Administration.API.Models.EnumResources;
using Administration.API.Models.Responses.Base;

namespace Administration.API.Models.Responses
{
	public class RoomResponse : RestResponse
	{
		public int Number { get; set; }
		public int Capacity { get; set; }
		public RoomTypeResource Type { get; set; }
		public RoomStateResource State { get; set; }
	}
}
