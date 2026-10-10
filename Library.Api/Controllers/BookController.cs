using Library.Infrastructure.Services;
using LibraryDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.CodeAnalysis.Operations;
using System.Data;
using Library.Infrastructure.Auth;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;
        private readonly ICurrentUserService _currentUser;


        public BookController(IBookService service, ICurrentUserService currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }


        [Authorize(Policy = "AdminOnly")]
        [HttpPost("AddBookToLibrary")]
        public async Task<IActionResult> AddBookToLbrary(CreateBookRequest request)
        {

            Book book = await _service.AddBookToLibrary(request.Isbn, request.Author, request.Name);

            if (book is null)
                return BadRequest();
            return Ok(book);
        }

        [Authorize]
        [HttpPost("{isbn}/borrow")]
        public async Task<IActionResult> BorrowBook([FromRoute] string isbn)
        {
            var userEmail = _currentUser.GetEmail();

            if (string.IsNullOrEmpty(userEmail))
                return Forbid();

            Book result = await _service.AddBookToAccount(isbn, userEmail);
            if (result is not null)
            {
                var bookdto = new BookReturnDTO(result.Isbn, result.Name, result.Author);
                return Ok(bookdto);
            }
            return BadRequest();
        }

        [Authorize]
        [HttpPost("{isbn}/return")]
        public async Task<IActionResult> RemoveBookFromAccount([FromRoute] string isbn)
        {
            var userEmail = _currentUser.GetEmail();
            if (string.IsNullOrEmpty(userEmail))
                return Forbid();
            Book result = await _service.ReturnBook(isbn, userEmail);
            if (result is not null)
                return Ok(result);

            return BadRequest();
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{isbn}/delete")]
        public async Task<IActionResult> DeleteBook([FromRoute] string isbn)
        {
            Book result = await _service.DeleteBook(isbn);
            if (result is not null)
                return Ok(result);
            return BadRequest();
        }

        [Authorize]
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            ICollection<Book> books = await _service.GetAllBooks();

            if (books is null)
                return BadRequest();
            return Ok(books);
        }

        [Authorize]
        [HttpGet("{email}/books")]
        public async Task<IActionResult> GetBooksInAccount()
        {
            var userEmail = _currentUser.GetEmail();
            if (string.IsNullOrEmpty(userEmail)) return Forbid();
            ICollection<Book> books = await _service.BooksInAccount(userEmail);

            if (books is null)
                return BadRequest();
            return Ok(books);
        }



    }
}


