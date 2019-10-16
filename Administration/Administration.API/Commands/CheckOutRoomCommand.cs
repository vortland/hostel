using System.Linq;
using Administration.API.Commands.Base;
using Administration.API.Infrastructure.Exceptions;
using Administration.API.Models.InputResources;
using Administration.Core.Exceptions;
using Administration.Core.Model;
using Administration.Core.Repositories;
using Administration.Core.Resources;

namespace Administration.API.Commands
{
	public class CheckOutRoomCommand : ICommand<IRoomRepository, Room, decimal>
	{
		private readonly RoomCheckOutInput _checkOutInput;
		private readonly int _roomId;

		public CheckOutRoomCommand(int roomId, RoomCheckOutInput checkOutInput)
		{
			Guard.IsNotNull(checkOutInput, nameof(checkOutInput));
			_checkOutInput = checkOutInput;
			_roomId = roomId;
		}

		public decimal Execute(IRoomRepository repository)
		{
			var room = repository.GetRoomById(_roomId);
			
			if (room == null)
			{
				throw new AdministrationApplicationNotFoundException(ValidationCodes.Room_NotFound_WrongId,
					ValidationMessages.Room_NotFound_WrongId);
			}

			var visitorsForCheckOut = room.Visitors.Where(v => _checkOutInput.VisitorsIds.Contains(v.Id)).ToList();

			var totalCOst = room.CheckOutVisitors(visitorsForCheckOut, _checkOutInput.CheckOutDate);

			repository.SaveChanges();

			return totalCOst;
		}
	}
}
