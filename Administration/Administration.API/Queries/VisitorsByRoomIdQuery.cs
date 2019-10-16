using System;
using System.Linq;
using System.Linq.Expressions;
using Administration.API.Infrastructure.Exceptions;
using Administration.API.Models.Responses;
using Administration.API.Queries.Base;
using Administration.Core.Exceptions;
using Administration.Core.Model;
using Administration.Core.Repositories;
using Administration.Core.Resources;

namespace Administration.API.Queries
{
	public class VisitorsByRoomIdQuery : BaseSearchQuery<IRoomRepository, Room, VisitorResponse>
	{
		private readonly int _roomId;

		public VisitorsByRoomIdQuery(int roomId, bool withPaging, int page, int pageSize)
			: base(withPaging, page, pageSize)
		{
			Guard.IsNotDefault(roomId, nameof(roomId));
			_roomId = roomId;
		}

		protected override IQueryable<VisitorResponse> GetQueryable(IRoomRepository repository)
		{
			var room = repository.GetRoomById(_roomId);

			if (room == null)
			{
				throw new AdministrationApplicationNotFoundException(ValidationCodes.Room_NotFound_WrongId,
					ValidationMessages.Room_NotFound_WrongId);
			}
			return room.Visitors
				.AsQueryable()
				.Select(GetAttributesExpression());
		}
		private static Expression<Func<Visitor, VisitorResponse>> GetAttributesExpression()
		{
			return r => new VisitorResponse
			{
				Id = r.Id,
				FullName = r.FullName,
				CheckInDate = r.CheckInDate,
				CheckOutDate = r.CheckOutDate
			};
		}
	}
}
