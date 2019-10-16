using Administration.API.Commands;
using Administration.API.Commands.Base;
using Administration.API.Models;
using Administration.API.Models.Extensions;
using Administration.API.Models.InputResources;
using Administration.API.Models.Responses;
using Administration.API.Models.Responses.Base;
using Administration.API.Queries;
using Administration.Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Administration.API.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	[ApiController]
	public class RoomsController : ControllerBase
	{

		private readonly IRoomRepository _roomRepository;

		public RoomsController(IRoomRepository roomRepository)
		{
			_roomRepository = roomRepository;
		}

		[HttpGet]
		[Route("{roomId:int}")]
		public ActionResult<RoomResponse> GetRoom(int roomId)
		{
			if (roomId == default)
				return BadRequest("Room Id required");

			var roomResponse = new RoomByIdQuery(roomId)
				.Execute(_roomRepository);

			var selfLink = GetRoomSelfLinkById(roomResponse.Id.Value);

			return Ok(roomResponse.WithLinks(selfLink));
		}

		[HttpGet]
		public ActionResult<Page<RoomResponse>> GetRooms([FromQuery] RoomSearchFilterInput filter)
		{
			if (filter == null)
				filter = new RoomSearchFilterInput();

			var roomsPageResponse = new RoomsSearchQuery(filter)
				.Execute(_roomRepository);

			foreach (var roomResponse in roomsPageResponse.Items)
			{
				var selfLink = GetRoomSelfLinkById(roomResponse.Id.Value);

				roomResponse.WithLinks(selfLink);
			}

			return Ok(roomsPageResponse);
		}

		[HttpGet]
		[Route("{roomId:int}/visitors")]
		public ActionResult<Page<VisitorResponse>> GetVisitors(int roomId, bool withPaging, int page, int pageSize)
		{
			if (roomId == default)
				return BadRequest("Room Id required");

			var visitorResponse = new VisitorsByRoomIdQuery(roomId, withPaging,  page,  pageSize)
				.Execute(_roomRepository);

			return Ok(visitorResponse);
		}

		[HttpPost]
		public ActionResult<RoomResponse> CreateRoom([FromBody] RoomCreateInput roomInput)
		{
			if (roomInput == null)
				return BadRequest("Wrong input parameters");
			if (roomInput.Number == default)
				return BadRequest("Room Number required");
			if (roomInput.RoomType == default)
				return BadRequest("Room Type required");
			if (roomInput.Capacity == default)
				return BadRequest("Room Capacity required");

			var roomResponse = new CreateRoomCommand(roomInput)
				.InTransactionScope()
				.Execute(_roomRepository);

			var selfLink = GetRoomSelfLinkById(roomResponse.Id.Value);

			return Created(selfLink.Rel, roomResponse.WithLinks(selfLink));
		}

		[Route("{roomId:int}/checkin")]
		[HttpPost]
		public ActionResult CheckInRoom(int roomId, [FromBody] RoomCheckInInput checkOutInput)
		{
			if (checkOutInput == null)
				return BadRequest("Wrong input parameters");
			if (roomId == default)
				return BadRequest("Room Id required");

			new CheckInRoomCommand(roomId, checkOutInput)
				.InTransactionScope()
				.Execute(_roomRepository);

			return Ok();
		}

		[Route("{roomId:int}/checkout")]
		[HttpPost]
		public ActionResult<RoomCheckOutResponse> CheckOutRoom(int roomId, [FromBody] RoomCheckOutInput checkOutInput)
		{
			if (checkOutInput == null)
				return BadRequest("Wrong input parameters");
			if (roomId == default)
				return BadRequest("Room Id required");

			var totalCost = new CheckOutRoomCommand(roomId, checkOutInput)
				.InTransactionScope()
				.Execute(_roomRepository);

			var selfLink = GetRoomSelfLinkById(roomId);
			var response = new RoomCheckOutResponse() { Id = roomId,TotalCost = totalCost};
			return Ok(response.WithLinks(selfLink));
		}

		private Link GetRoomSelfLinkById(int roomId)
		{
			var url = Url.Action(nameof(GetRoom), new {roomId});

			var selfLink = new Link("self", url);

			return selfLink;
		}
	}
}
