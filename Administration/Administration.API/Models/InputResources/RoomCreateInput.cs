using Administration.API.Models.EnumResources;

namespace Administration.API.Models.InputResources
{
	public class RoomCreateInput
	{
		public int Number { get; set; }
		public int Capacity { get; set; }
		public RoomTypeResource RoomType { get; set; }
	}
}
