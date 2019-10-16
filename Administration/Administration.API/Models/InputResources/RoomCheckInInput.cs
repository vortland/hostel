using System;

namespace Administration.API.Models.InputResources
{
	public class RoomCheckInInput
	{
		public string VisitorFullName { get; set; }

		public DateTime CheckInDate { get; set; }
	}
}
