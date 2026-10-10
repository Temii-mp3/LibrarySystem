using Library.Infrastructure.Auth;
using Library.Infrastructure.Services;
using LibraryDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using System.ComponentModel.Design;
using System.Data;
using System.Security.Claims;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _service;
        private readonly ICurrentUserService _currentUser;


        public RoomController(IRoomService service, ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
            _service = service;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("AddRoomToLibrary")]
        public async Task<IActionResult> AddRoomToLbrary(CreateRoomRequest request)
        {

            Room room = await _service.AddRoomToLibrary(request.type);

            if (room is null)
                return BadRequest();
            return Ok(room);
        }

        [Authorize]
        [HttpPost("{id}/checkout")]
        public async Task<IActionResult> AddRoomToAccount([FromRoute] string id)
        {
            var userEmail = _currentUser.GetEmail();
            if (string.IsNullOrEmpty(userEmail))
                return Forbid();

            Room result = await _service.AddRoomToAccount(id, userEmail);
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest();

        }

        [Authorize]
        [HttpPost("{id}/checkin")]
        public async Task<IActionResult> RemoveRoomFromAccount([FromRoute] string id)
        {
            var userEmail = _currentUser.GetEmail();
            if (string.IsNullOrEmpty(userEmail))
                return Forbid();

            Room result = await _service.RemoveRoomFromAccount(id, userEmail);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{roomId}/delete")]
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

        [Authorize]
        [HttpGet("{email}/rooms")]
        public async Task<IActionResult> GetBookedRoomsInAccount()
        {
            var userEmail = _currentUser.GetEmail();
            if (string.IsNullOrEmpty(userEmail))
                return Forbid();
            ICollection<Room> Rooms = await _service.GetBorrowedRooms(userEmail);

            if (Rooms is null)
                return BadRequest();
            return Ok(Rooms);
        }

    }
}


