using Administration.API.Commands.Base;
using Administration.API.Infrastructure.Exceptions;
using Administration.API.Models.InputResources;
using Administration.Core.Exceptions;
using Administration.Core.Model;
using Administration.Core.Repositories;
using Administration.Core.Resources;

namespace Administration.API.Commands
{
	public class CheckInRoomCommand : ICommand<IRoomRepository, Room>
	{
		private readonly RoomCheckInInput _checkInInput;
		private readonly int _roomId;

		public CheckInRoomCommand(int roomId, RoomCheckInInput checkInInput)
		{
			Guard.IsNotNull(checkInInput, nameof(checkInInput));
			_checkInInput = checkInInput;
			_roomId = roomId;
		}

		public void Execute(IRoomRepository repository)
		{
			var room = repository.GetRoomById(_roomId);

			if (room == null)
			{
				throw new AdministrationApplicationNotFoundException(ValidationCodes.Room_NotFound_WrongId,
					ValidationMessages.Room_NotFound_WrongId);
			}

			room.CheckInVisitor(_checkInInput.VisitorFullName, _checkInInput.CheckInDate);

			repository.SaveChanges();
		}
	}
}
