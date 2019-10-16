using System;
using System.Collections.Generic;

namespace Administration.API.Models.InputResources
{
	public class RoomCheckOutInput
	{
		public List<int> VisitorsIds { get; set; }

		public DateTime CheckOutDate { get; set; }
	}
}
