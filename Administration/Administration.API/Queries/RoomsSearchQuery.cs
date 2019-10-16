using System;
using System.Linq;
using System.Linq.Expressions;
using Administration.API.Models.EnumResources;
using Administration.API.Models.Extensions;
using Administration.API.Models.InputResources;
using Administration.API.Models.Responses;
using Administration.API.Queries.Base;
using Administration.Core.Model;
using Administration.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Administration.API.Queries
{
	public class RoomsSearchQuery : BaseSearchQuery<IRoomRepository, Room, RoomResponse>
	{
		private readonly RoomSearchFilterInput _filter;
		public RoomsSearchQuery(RoomSearchFilterInput filter) 
			: base(filter.WithPaging, filter.Page, filter.PageSize, filter.Sort.ToExpression(), filter.Sorting)
		{
			_filter = filter;
		}

		protected override IQueryable<RoomResponse> GetQueryable(IRoomRepository repository)
		{
			return repository.GetRooms()
				.Include(r => r.Visitors)
				.WhereIf(_filter.State != null, ExtendedStateProvider())
				.WhereIf(_filter.Capacity != null, r => r.Capacity == _filter.Capacity)
				.WhereIf(_filter.Type != null, ExtendedTypeProvider())
				.Select(GetAttributesExpression());
		}

		private static Expression<Func<Room, RoomResponse>> GetAttributesExpression()
		{
			return r => new RoomResponse
			{
				Id = r.Id,
				Capacity = r.Capacity,
				Number = r.Number,
				State = (RoomStateResource) r.State,
				Type = (RoomTypeResource) r.Type
			};
		}

		private Expression<Func<Room, bool>> ExtendedStateProvider()
		{
			if (_filter.State == null)
				return r => false;

			var state = _filter.State.Value.FromResource();

			return r => r.State == state;
		}

		private Expression<Func<Room, bool>> ExtendedTypeProvider()
		{
			if (_filter.Type == null)
				return r => false;

			var type = _filter.Type.Value.FromResource();

			return r => r.Type == type;
		}

	}
}
