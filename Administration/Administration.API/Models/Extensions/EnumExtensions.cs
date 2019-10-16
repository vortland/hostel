using System;
using Administration.API.Models.EnumResources;
using Administration.Core.Model.Enums;

namespace Administration.API.Models.Extensions
{
    public static class EnumExtensions
    {
        public static RoomState FromResource(this RoomStateResource state)
		{
			switch (state)
            {
                case RoomStateResource.Available:
                    return RoomState.Available;
                case RoomStateResource.FullyOccupied:
                    return RoomState.FullyOccupied;
                case RoomStateResource.PartiallyOccupied:
                    return RoomState.PartiallyOccupied;
                default:
	                throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
		}

        public static RoomType FromResource(this RoomTypeResource type)
        {
	        switch (type)
	        {
		        case RoomTypeResource.Standard:
			        return RoomType.Standard;
		        case RoomTypeResource.Suite:
			        return RoomType.Suite;
		        case RoomTypeResource.Deluxe:
			        return RoomType.Deluxe;
		        default:
			        throw new ArgumentOutOfRangeException(nameof(type), type, null);
	        }
		}
		public static RoomStateResource ToResource(this RoomState state)
		{
			switch (state)
			{
				case RoomState.Available:
					return RoomStateResource.Available;
				case RoomState.FullyOccupied:
					return RoomStateResource.FullyOccupied;
				case RoomState.PartiallyOccupied:
					return RoomStateResource.PartiallyOccupied;
				default:
					throw new ArgumentOutOfRangeException(nameof(state), state, null);
			}
		}

		public static RoomTypeResource ToResource(this RoomType type)
		{
			switch (type)
			{
				case RoomType.Standard:
					return RoomTypeResource.Standard;
				case RoomType.Suite:
					return RoomTypeResource.Suite;
				case RoomType.Deluxe:
					return RoomTypeResource.Deluxe;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}
	}
}