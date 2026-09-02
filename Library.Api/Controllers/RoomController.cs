using LibraryDomain.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> AddRoomToLbrary()
        {


            return BadRequest();
        }

        [HttpGet("AddRoomToAccount")]
        public async Task<IActionResult> BorrowRoom()
        {

            return BadRequest();
        }

        [HttpDelete("RemoveRoomFromAccount")]
        public async Task<IActionResult> RemoveRoomFromAccount()
        {

            return BadRequest();

        }

        [HttpDelete("DeleteRoom")]
        public async Task<IActionResult> DeleteRoom()
        {
            return BadRequest();
        }

    }
}


