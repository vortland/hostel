using System.Linq;
using Administration.Core.Model;
using Administration.Core.Repositories.Base;

namespace Administration.Core.Repositories
{
	public interface IRoomRepository : IRepository<Room>
	{
		Room Add(Room room);
		Room GetRoomById(int roomId);
		IQueryable<Room> GetRooms();
	}
}
