

using LibraryDomain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystemTests
{

    [TestClass]
    public sealed class LibraryAccountTest
    {
        private LibraryContext context;
        Account? account;
        Book? book;
        Room? room;
        AccountRepositry _repo;

        [TestInitialize]
        public void setup()
        {
            account = new Account
            {
                Email = "janedoe@gmail.com",
                Password = "janedoeiscool",
                Username = "jane"
            };
            book = new Book
            {
                Isbn = "111",
                Author = "Mike Hawk",
                BorrowedBy = null,
                Name = "Book of Mike Hawk"
            };

            var options = new DbContextOptionsBuilder<LibraryContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            context = new(options);
            _repo = new(context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            context.Dispose();
        }
        [TestMethod]
        public void AccountCreated_InsertsToDB()
        {
            context.Accounts.Add(account);
            context.SaveChanges();

            Assert.IsTrue(context.Accounts.Any(a => a.Id == account.Id));
        }

        [TestMethod]
        public async Task Account_CanBorrow()
        {
            context.Accounts.Add(account);
            context.Books.Add(book);
            context.SaveChanges();

            Account userAccount = context.Accounts.FirstOrDefault(a => a.Id == account.Id);

            var result = await _repo.AddBookToAccount(userAccount, book);

            Assert.AreSame(result, book);             

        }


    }

}
