using System;
using System.Collections.Generic;
using System.Linq;
using Administration.Core.Exceptions;
using Administration.Core.Model.Base;
using Administration.Core.Model.Enums;
using Administration.Core.Resources;

namespace Administration.Core.Model
{
	public class Room : Entity, IAggregateRoot
	{
		#region Consts

		private const decimal StandardCoefficient = 2;
		private const decimal SuiteCoefficient = 3;
		private const decimal DeluxeCoefficient = 4;

		#endregion

		#region Fields

		private int _capacity;
		private int _number;
		private RoomState _state;
		private RoomType _type;
		private readonly List<Visitor> _visitors;

		#endregion Fields


		#region Properties
		public IReadOnlyCollection<Visitor> Visitors => _visitors.AsReadOnly();

		public int Capacity
		{
			get => _capacity;
			protected set => _capacity = value;
		}

		public int Number
		{
			get => _number;
			protected set => _number = value;
		}

		public RoomState State
		{
			get => _state;
			protected set => _state = value;
		}

		public RoomType Type
		{
			get => _type;
			protected set => _type = value;
		}

		#endregion Properties


		#region Constructors

		public Room(int number, int capacity, RoomType type) : this()
		{
			Guard.IsNotDefault(capacity, nameof(capacity));
			Guard.IsNotDefault(number, nameof(number));
			Guard.IsNotDefault(type, nameof(type));

			_capacity = capacity;
			_number = number;
			_type = type;
			_state = RoomState.Available;
		}

		protected Room()
		{
			_visitors = new List<Visitor>();
		}

		#endregion Constructors


		#region Methods

		public void CheckInVisitor(string visitorFullName, DateTime checkInDate)
		{
			var visitor = new Visitor(Id, visitorFullName, checkInDate);

			_visitors.Add(visitor);

			UpdateState();
		}

		public decimal CheckOutVisitors(List<Visitor> visitors, DateTime checkOutDate)
		{
			Guard.IsNotNullOrEmpty(visitors, nameof(visitors));

			var totalCost = visitors.Sum(visitor => CheckOutVisitor(visitor, checkOutDate));

			return totalCost;
		}

		public decimal CheckOutVisitor(Visitor visitor, DateTime checkOutDate)
		{
			Guard.IsNotNull(checkOutDate, nameof(checkOutDate));

			var visitorToCheckOut = _visitors.FirstOrDefault(visitorSearch => visitorSearch.Equals(visitor));

			if (visitorToCheckOut == null)
			{
				throw new AdministrationDomainException(
					ValidationCodes.Room_CannotCheckedOut_VisitorNotExists,
					ValidationMessages.Room_CannotCheckedOut_VisitorNotExists);
			}
			
			visitorToCheckOut.CheckOut(checkOutDate);

			UpdateState();

			return CalculateCost(visitorToCheckOut.CheckInDate, checkOutDate);
		}

		private decimal CalculateCost(DateTime checkInDate, DateTime checkOutDate)
		{
			var daysCount = (int) Math.Ceiling (checkOutDate.Subtract(checkInDate).TotalDays);

			var cost = daysCount * GetRoomCoefficient();

			return cost;
		}

		private void UpdateState()
		{
			var visitorsCount = _visitors.Count(v => v.CheckOutDate == null);

			if (visitorsCount == 0)
			{
				_state = RoomState.Available;
			}
			else if (visitorsCount == _capacity)
			{
				_state = RoomState.FullyOccupied;
			}
			else if (visitorsCount < _capacity)
			{
				_state = RoomState.PartiallyOccupied;
			}
			else
			{
				throw new AdministrationDomainException(
					ValidationCodes.Room_VisitorNumberOutOfRange,
					ValidationMessages.Room_VisitorNumberOutOfRange);
			}
		}

		private decimal GetRoomCoefficient()
		{
			switch (_type)
			{
				case RoomType.Standard:
					return StandardCoefficient;
				case RoomType.Suite:
					return SuiteCoefficient;
				case RoomType.Deluxe:
					return DeluxeCoefficient;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		#endregion Methods
	}
}
