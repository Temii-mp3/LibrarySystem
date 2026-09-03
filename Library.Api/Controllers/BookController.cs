using Library.Infrastructure.Services;
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

            Book book = await _service.AddBookToLibrary(request.Isbn, request.Author, request.Name);

            if (book is null)
                return BadRequest();
            return Ok(book);
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

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            List<Book> books = _service.GetAllBooks();
        }

    }
}


