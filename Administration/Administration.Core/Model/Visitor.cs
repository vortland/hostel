using System;
using Administration.Core.Exceptions;
using Administration.Core.Model.Base;

namespace Administration.Core.Model
{
	public class Visitor : Entity
	{
		private string _fullName;
		private DateTime _checkInDate;
		private DateTime? _checkOutDate;
		private int _roomId;


		public string FullName
		{
			get => _fullName;
			protected set => _fullName = value;
		}

		public DateTime CheckInDate
		{
			get => _checkInDate;
			protected set => _checkInDate = value;
		}

		public DateTime? CheckOutDate
		{
			get => _checkOutDate;
			protected set => _checkOutDate = value;
		}

		public int RoomId
		{
			get => _roomId;
			protected set => _roomId = value;
		}

		public Visitor(int roomId, string fullName, DateTime checkInDate) 
		{
			Guard.IsNotNull(fullName, nameof(fullName));
			Guard.IsNotNull(checkInDate, nameof(checkInDate));
			
			_roomId = roomId;
			_fullName = fullName;
			_checkInDate = checkInDate;
		}

		public void CheckOut(DateTime checkOutDate)
		{
			Guard.IsNotNull(checkOutDate, nameof(checkOutDate));
			Guard.IsGreaterOrEqual(checkOutDate, nameof(checkOutDate), _checkInDate);

			_checkOutDate = checkOutDate;
		}
	}
}
