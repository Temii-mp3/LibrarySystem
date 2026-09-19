using Library.Infrastructure.Services;
using LibraryDomain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using System.ComponentModel.Design;
using System.Data;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _service;


        public RoomController(IRoomService service)
        {
            _service = service;
        }

        [HttpPost("AddRoomToLibrary")]
        public async Task<IActionResult> AddRoomToLbrary(CreateRoomRequest request)
        {

            Room room = await _service.AddRoomToLibrary(request.type);

            if (room is null)
                return BadRequest();
            return Ok(room);
        }

        [HttpPost("AddRoomToAccount")]
        public async Task<IActionResult> AddRoomToAccount(RoomUserDTO dto)
        {
            Room result = await _service.AddRoomToAccount(dto.Room.id, dto.User.email);
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest();

        }

        [HttpPost("RemoveRooFromAccount")]
        public async Task<IActionResult> RemoveRoomFromAccount(RoomUserDTO dto)
        {
            Room result = await _service.RemoveRoomFromAccount(dto.Room.id, dto.User.email);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        [HttpDelete("{roomId}")]
        public async Task<IActionResult> DeleteRoom([FromRoute] string roomId)
        {
            Room result = await _service.RemoveRoomFromLibrary(roomId);
            if (result is not null)
                return Ok(result);
            return BadRequest();
        }

        [HttpGet("GetAllRooms")]
        public async Task<IActionResult> GetAllRooms()
        {
            ICollection<Room> Rooms = await _service.GetAllRooms();

            if (Rooms is null)
                return BadRequest();
            return Ok(Rooms);
        }

        [HttpGet("GetBookedRoomsInAccount")]
        public async Task<IActionResult> GetBookedRoomsInAccount([FromQuery]AccountDTO user)
        {
            ICollection<Room> Rooms = await _service.GetBorrowedRooms(user.email);

            if (Rooms is null)
                return BadRequest();
            return Ok(Rooms);
        }

    }
}


