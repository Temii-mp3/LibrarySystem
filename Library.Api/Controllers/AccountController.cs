using LibraryDomain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;


        public AccountController(IAccountService service, IAccountRepository repo)
        {
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAccount(CreateAccountRequest request)
        {
            Account user = await _service.AddAccountToDB(request.Email, request.Password, request.Username);

            if (user is not null)
            {
                return Ok(user);
            }

            return BadRequest();
        }

        [HttpGet("Lookup")]
        public async Task<IActionResult> LookupAccount([FromQuery] LookupAccountRequest request)
        {
            Account user = await _service.LookupAccount(request.Email);
            if (user is not null)
            {
                return Ok(user);
            }

            return BadRequest();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAccount(DeleteAccountRequst request)
        {
            Account user = await _service.DeleteAccount(request.Email);
            if (user is null)
                return BadRequest();
            return Ok(user);
        }

    }
}
