using Library.Api.Controllers;
using Library.Api.DTO;
using Library.Domain.Models;
using Library.Infrastructure.Auth;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace LibrarySystemTests
{
    public class BookTests
    {
        [Fact]
        public async Task BorrowBook_ReturnsOk_WhenAuthenticatedAndServiceSucceeds()
        {
            var svc = new Mock<IBookService>();
            var current = new Mock<ICurrentUserService>();
            current.Setup(c => c.GetEmail()).Returns("a@b.com");
            var controller = new BookController(svc.Object, current.Object);

            var book = new Book { Isbn = "ISBN1", Name = "N", Author = "A" };
            svc.Setup(s => s.AddBookToAccount("ISBN1", "a@b.com")).ReturnsAsync(book);

            var result = await controller.BorrowBook("ISBN1");

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(ok.Value);

            // controller returns BookReturnDTO; check the DTO has expected ISBN via reflection
            var dtoType = ok.Value.GetType();
            var isbnProp = dtoType.GetProperty("isbn", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            Assert.NotNull(isbnProp);
            Assert.Equal("ISBN1", isbnProp.GetValue(ok.Value));
        }

        [Fact]
        public async Task BorrowBook_ReturnsForbid_WhenUnauthenticated()
        {
            var svc = new Mock<IBookService>();
            var current = new Mock<ICurrentUserService>();
            current.Setup(c => c.GetEmail()).Returns<string?>(null);
            var controller = new BookController(svc.Object, current.Object);

            var result = await controller.BorrowBook("ISBN1");

            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task ReturnBook_ReturnsOk_WhenAuthenticatedAndServiceSucceeds()
        {
            var svc = new Mock<IBookService>();
            var current = new Mock<ICurrentUserService>();
            current.Setup(c => c.GetEmail()).Returns("a@b.com");
            var controller = new BookController(svc.Object, current.Object);

            var book = new Book { Isbn = "ISBN1", Name = "N", Author = "A" };
            svc.Setup(s => s.ReturnBook("ISBN1", "a@b.com")).ReturnsAsync(book);

            var result = await controller.RemoveBookFromAccount("ISBN1");

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(book, ok.Value);
        }

        [Fact]
        public async Task GetBooksInAccount_ReturnsOk_WhenAuthenticated()
        {
            var svc = new Mock<IBookService>();
            var current = new Mock<ICurrentUserService>();
            current.Setup(c => c.GetEmail()).Returns("a@b.com");
            var controller = new BookController(svc.Object, current.Object);

            var books = new List<Book> { new Book { Isbn = "ISBN1" } };
            svc.Setup(s => s.BooksInAccount("a@b.com")).ReturnsAsync(books);

            var result = await controller.GetBooksInAccount();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(books, ok.Value);
        }

        [Fact]
        public async Task GetBooksInAccount_ReturnsForbid_WhenUnauthenticated()
        {
            var svc = new Mock<IBookService>();
            var current = new Mock<ICurrentUserService>();
            current.Setup(c => c.GetEmail()).Returns<string?>(null);
            var controller = new BookController(svc.Object, current.Object);

            var result = await controller.GetBooksInAccount();

            Assert.IsType<ForbidResult>(result);
        }
    }
}