using LibraryDomain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _service;


        public AccountController (IAccountService service, IAccountRepository repo)
        {
            _service = service;
        }



        [HttpPost]
        public async Task<IActionResult> CreateAccount(CreateAccountRequest request)
        {
            Account user = await _service.AddAccountToDB(request.Email, request.Password, request.Username);

            if (user is not null)
            {
                return Ok(user);
            }

            return NotFound();
        }
    }
}
