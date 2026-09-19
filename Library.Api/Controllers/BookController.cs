using Library.Infrastructure.Services;
using LibraryDomain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using System.Data;

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

        [HttpPost("AddBookToAccount")]
        public async Task<IActionResult> BorrowBook(BorrowBookRequest req)
        {
            Book result = await _service.AddBookToAccount(req.isbn, req.email);
            if (result is not null)
            {
                var bookdto = new BookReturnDTO(result.Isbn, result.Name, result.Author);
                return Ok(bookdto);
            }
            return BadRequest();
            
        }

        [HttpPost("RemoveFromAccount/{isbn}")]
        public async Task<IActionResult> RemoveBookFromAccount([FromRoute]string isbn)
        {

            Book result = await _service.ReturnBook(isbn);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBook(BookDTO _book)
        {
            Book result = await _service.DeleteBook(_book.Isbn);
            if (result is not null)
                return Ok(result);
            return BadRequest();
        }

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            ICollection<Book> books = await _service.GetAllBooks();

            if (books is null)
                return BadRequest();
            return Ok(books);
        }

    } 
}


