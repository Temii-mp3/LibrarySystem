using LibraryDomain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;


        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpPost("AddBookToLibrary")]
        public async Task<IActionResult> AddBookToLbrary(CreateBookRequest request)
        {
            _service.
        }

        [HttpGet("AddBookToAccount")]
        public async Task<IActionResult> BorrowBook()
        {

            return BadRequest();
        }

        [HttpDelete("RemoveBookFromAccount")]
        public async Task<IActionResult> RemoveBookFromAccount()
        {

            return BadRequest();

        }

        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBook()
        {
            return BadRequest();
        }

    }
}


