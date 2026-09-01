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

        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount(CreateAccountRequest request)
        {
            Account user = await _service.AddAccountToDB(request.Email, request.Password, request.Username);

            if (user is not null)
            {
                return Ok(user);
            }

            return BadRequest();
        }

        [HttpGet("lookup")]
        public async Task<IActionResult> LookupAccount([FromQuery]LookupAccountRequest request)
        {
            Account user = await _service.LookupAccount(request.Email);
            if(user is not null)
            {
                return Ok(user);
            }

            return BadRequest();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAccount(DeleteAccountRequst request)
        {
            Account user = await _service.DeleteAccount(request.Email);
            if (user is null)
                return BadRequest();
            return Ok(user);
        }

    }
}
