using LibraryDomain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly IAccountService _service;


        public BookController (IAccountService service, IAccountRepository repo)
        {
            _service = service;
        }

        [HttpPost("AddBookToLibrary")]
        public async Task<IActionResult> AddBookToLbrary()
        {


            return BadRequest();
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
