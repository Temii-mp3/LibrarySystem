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
        public async Task<IActionResult> CheckoutRoom(CheckoutRoomRrequest req)
        {
            Room result = await _service.AddRoomToAccount(req.isbn, req.email);
            if (result is not null)
            {
                var Roomdto = new RoomReturnDTO(result.Isbn, result.Name, result.Author);
                return Ok(Roomdto);
            }
            return BadRequest();

        }

        [HttpPost("RemoveRoomFromAccount")]
        public async Task<IActionResult> RemoveRoomFromAccount(RoomDTO Room)
        {

            Room result = await _service.ReturnRoom(Room.Isbn);
            if (result is not null)
                return Ok(result);

            return BadRequest();


        }

        [HttpDelete("DeleteRoom")]
        public async Task<IActionResult> DeleteRoom(RoomDTO _Room)
        {
            Room result = await _service.DeleteRoom(_Room.Isbn);
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

    }
}


