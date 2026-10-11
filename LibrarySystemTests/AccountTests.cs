using Library.Api.Controllers;
using Library.Api.DTO;
using Library.Domain.Models;
using Library.Infrastructure.Auth;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace LibrarySystemTests
{
    public class AccountTests
    {
        [Fact]
        public async Task CreateAccount_ReturnsOk_WhenServiceCreatesAccount()
        {
            var svc = new Mock<IAccountService>();
            var current = new Mock<ICurrentUserService>();
            var controller = new AccountController(svc.Object, current.Object);

            var req = new CreateAccountRequest("a@b.com", "password1", "user1");
            var created = new Account { Id = 1, Email = req.Email, Username = req.Username, Password = "hashed" };

            svc.Setup(s => s.AddAccountToDB(req.Email, req.Password, req.Username)).ReturnsAsync(created);

            var result = await controller.CreateAccount(new CreateAccountRequest(req.Email, req.Password, req.Username));

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(created, ok.Value);
        }

        [Fact]
        public async Task CreateAccount_ReturnsBadRequest_WhenServiceReturnsNull()
        {
            var svc = new Mock<IAccountService>();
            var current = new Mock<ICurrentUserService>();
            var controller = new AccountController(svc.Object, current.Object);

            var req = new CreateAccountRequest("bad", "pw", "u");
            svc.Setup(s => s.AddAccountToDB(req.Email, req.Password, req.Username)).ReturnsAsync((Account?)null);

            var result = await controller.CreateAccount(new CreateAccountRequest(req.Email, req.Password, req.Username));

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsOk_WithToken()
        {
            var svc = new Mock<IAccountService>();
            var current = new Mock<ICurrentUserService>();
            var controller = new AccountController(svc.Object, current.Object);

            var dto = new LoginDTO("a@b.com", "password1");
            svc.Setup(s => s.LoginUser(dto.email, dto.password)).ReturnsAsync("token123");

            var result = await controller.Login(dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("token123", ok.Value);
        }

        [Fact]
        public async Task LookupAccount_ReturnsForbid_WhenUserMismatch()
        {
            var svc = new Mock<IAccountService>();
            var current = new Mock<ICurrentUserService>();
            current.Setup(c => c.GetEmail()).Returns("other@b.com");
            var controller = new AccountController(svc.Object, current.Object);

            var result = await controller.LookupAccount("a@b.com");

            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task UpdateAccount_ReturnsForbid_WhenUnauthenticated()
        {
            var svc = new Mock<IAccountService>();
            var current = new Mock<ICurrentUserService>();
            current.Setup(c => c.GetEmail()).Returns<string?>(null);
            var controller = new AccountController(svc.Object, current.Object);

            var request = new UpdateAccountRequest(null, null, null);

            var result = await controller.UpdateAccount(request);

            Assert.IsType<ForbidResult>(result);
        }
    }
}