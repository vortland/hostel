using System;
using System.Linq;
using Administration.Core.Exceptions;
using Administration.Core.Model;
using Administration.Core.Repositories;

namespace Administration.DataAccessLayer.Repositories
{
    public class RoomRepository : IRoomRepository
    {
		private readonly AdministrationContext _context;
        public RoomRepository(AdministrationContext context)
		{
			Guard.IsNotNull(context, nameof(context));
			_context = context;
		}

        public void SaveChanges()
        {
	        _context.SaveChanges();
        }

        public Room Add(Room room)
        {
			Guard.IsNotNull(room, nameof(room));

            return  _context.Rooms.Add(room).Entity;
        }
		
        public Room GetRoomById(int roomId)
        {
            var room = _context.Rooms.Find(roomId);

            if(room != null)
            {
                _context.Entry(room)
                    .Collection(r => r.Visitors).Load();
            }

            return room;
        }

        public IQueryable<Room> GetRooms()
        {
	        return _context.Rooms;
        }
		
    }
}