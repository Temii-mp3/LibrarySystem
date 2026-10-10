using Library.Infrastructure.Auth;
using LibraryDomain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;
        private readonly ICurrentUserService _currentUser;

        public AccountController(IAccountService service, ICurrentUserService currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }

        [HttpPost("CreateAccount")]
        public async Task<IActionResult> CreateAccount(CreateAccountRequest request)
        {
            Account user = await _service.AddAccountToDB(request.Email, request.Password, request.Username);

            if (user is not null)
            {
                return Ok(user);
            }

            return BadRequest();
        }


        [Authorize]
        [HttpGet("{email}/lookup")]
        public async Task<IActionResult> LookupAccount([FromRoute] string email)
        {
            var userEmail = _currentUser.GetEmail();

            if (string.IsNullOrEmpty(userEmail) || !string.Equals(userEmail, email, StringComparison.OrdinalIgnoreCase))
            {
                return Forbid();
            }
            Account user = await _service.LookupAccount(email);
            if (user is not null)
            {
                return Ok(user);
            }

            return BadRequest();
        }
        [Authorize]
        [HttpDelete("{email}/delete")]
        public async Task<IActionResult> DeleteAccount([FromRoute] string email)
        {
            var userEmail = _currentUser.GetEmail();
            var isAdmin = _currentUser.IsAdmin();

            if (string.IsNullOrEmpty(userEmail) || !string.Equals(userEmail, email, StringComparison.OrdinalIgnoreCase) && !isAdmin)
                return Forbid();

            Account user = await _service.DeleteAccount(email);
            if (user is null)
                return BadRequest();
            return Ok(user);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet("GetAllAccounts")]
        public async Task<IActionResult> GetAllAccounts()
        {
            ICollection<Account> accounts = await _service.GetAllAccounts();
            if (accounts is null)
                return BadRequest();
            return Ok(accounts);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            string token = await _service.LoginUser(dto.email, dto.password);

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest();
            }

            return Ok(token);
        }
        //[HttpPost("UpdateAccount")]
        //public async Task<IActionResult> UpdateAccount(UpdateAccountRequest request)
        //{
        //    Account updatedAccount = new Account
        //    {
        //        Email = string.IsNullOrEmpty(request.Email) ? request.Email : null,
        //        Password = string.IsNullOrEmpty(request.Password) ? request.Password : null,
        //        Username = string.IsNullOrEmpty(request.Username) ? request.Username : null
        //    };
        //}

    }
}
