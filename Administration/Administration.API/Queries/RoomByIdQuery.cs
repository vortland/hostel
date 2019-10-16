using Administration.API.Models.Extensions;
using Administration.API.Models.Responses;
using Administration.API.Queries.Base;
using Administration.Core.Exceptions;
using Administration.Core.Model;
using Administration.Core.Repositories;
namespace Administration.API.Queries
{
	public class RoomByIdQuery : IQuery<IRoomRepository, Room, RoomResponse>
	{
		private readonly int _id;

		public RoomByIdQuery(int id)
		{
			Guard.IsNotDefault(id, nameof(id));
			_id = id;
		}

		public RoomResponse Execute(IRoomRepository repository)
		{
			return repository.GetRoomById(_id)
				.ToResponse();
		}
	}
}
