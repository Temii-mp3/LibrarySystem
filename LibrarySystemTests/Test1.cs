

namespace LibrarySystemTests
{

    [TestClass]
    public sealed class LibraryAccountTest
    {

        string testEmail;
        string testPass;
        string testUser;
        int testId;
        Account user;

        AccountRepositry repo;
        AccountService service;

        [TestInitialize]
        public void setup() { 
        
             repo = new AccountRepositry();
             service = new AccountService(repo);
             testEmail = "John@test.com";
             testPass = "test";
             testUser = "John";
             testId = 2;
             user = new Account(testEmail, testUser, testPass, testId);
            repo.addAccount(user);

        }
        [TestMethod]
        public void ValidateAccountCreated()
        {
            
            string actualEmail = repo.LookupAccount(user).Email;

            Assert.AreEqual(testEmail, actualEmail, true, "Account not created");
            
        }


        [TestMethod]
        public void CheckAccountAddedToRepo()
        {

            Assert.HasCount(1,repo.accounts);
        }


        [TestMethod]
        public void CheckCanBorrowBook()
        {
            Library lib = new Library();

            Account account = service.addBookToAccount(lib.getBook(110), repo.LookupAccount(user));

            Assert.AreEqual(repo.LookupAccount(testEmail, testPass), account);

        }
    }
}
