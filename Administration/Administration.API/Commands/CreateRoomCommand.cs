using System.Linq;
using Administration.API.Commands.Base;
using Administration.API.Models.Extensions;
using Administration.API.Models.InputResources;
using Administration.API.Models.Responses;
using Administration.Core.Exceptions;
using Administration.Core.Model;
using Administration.Core.Repositories;
using Administration.Core.Resources;

namespace Administration.API.Commands
{
	public class CreateRoomCommand : ICommand<IRoomRepository, Room, RoomResponse>
	{
		private readonly RoomCreateInput _roomInput;

		public CreateRoomCommand(RoomCreateInput roomInput)
		{
			Guard.IsNotNull(roomInput, nameof(roomInput));
			_roomInput = roomInput;
		}

		public RoomResponse Execute(IRoomRepository repository)
		{
			var existRoom = repository.GetRooms().FirstOrDefault(r => r.Number == _roomInput.Number);

			if (existRoom != null)
			{
				throw new AdministrationDomainException(ValidationCodes.Room_CannotCreate_NumberNotUnique,
					ValidationMessages.Room_CannotCreate_NumberNotUnique);
			}
			var room = new Room(_roomInput.Number, _roomInput.Capacity, _roomInput.RoomType.FromResource());

			repository.Add(room);

			repository.SaveChanges();

			return room.ToResponse();
		}
	}
}
