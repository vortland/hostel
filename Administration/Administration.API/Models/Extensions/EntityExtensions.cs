using Administration.API.Models.Responses;
using Administration.Core.Model;

namespace Administration.API.Models.Extensions
{
    public static class EntityExtensions
	{
        public static RoomResponse ToResponse(this Room room)
		{
			return new RoomResponse()
			{
				Capacity = room.Capacity,
				Number = room.Number,
				Type = room.Type.ToResource(),
				State = room.State.ToResource(),
				Id = room.Id
			};
		}

        public static VisitorResponse ToResponse(this Visitor visitor)
        {
	        return new VisitorResponse()
	        {
		        FullName = visitor.FullName,
		        CheckInDate = visitor.CheckInDate,
		        CheckOutDate = visitor.CheckOutDate,
		        Id = visitor.Id
	        };
        }

	}
}