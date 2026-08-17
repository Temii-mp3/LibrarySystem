

using LibrarySystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystemTests
{

    [TestClass]
    public sealed class LibraryAccountTest
    {
        private LibraryContext context;
        Account account;

        [TestInitialize]
        public void setup()
        {
            Account account = new Account
            {
                Email = "janedoe@gmail.com",
                Password = "janedoeiscool",
                Username = "jane"
            };
            var options = new DbContextOptionsBuilder<LibraryContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            context = new(options);
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

            Assert.IsTrue(context.Accounts.Contains(account));
        }


    }
    
}
