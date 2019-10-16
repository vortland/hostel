using System;
using System.Linq.Expressions;
using Administration.API.Models.EnumResources;
using Administration.API.Models.Responses;

namespace Administration.API.Models.Extensions
{
	public static class SortingExtensions
	{
		public static Expression<Func<RoomResponse, object>> ToExpression(this RoomFilterSortFields roomFields)
		{
			switch (roomFields)
			{
				case RoomFilterSortFields.Capacity:
					return r => r.Capacity;
				case RoomFilterSortFields.Type:
					return r => r.Type;
				case RoomFilterSortFields.State:
					return r => r.State;
				default:
					throw new ArgumentOutOfRangeException(nameof(roomFields), roomFields, null);
			}
		}
	}
}
