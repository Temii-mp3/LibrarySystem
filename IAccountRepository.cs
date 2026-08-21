public interface IAccountRepository
{
    public Task<Account> addAccount(Account a);
    public Task<Account> deleteAccount(Account a);
    public Task<Account> LookupAccount(string email, string password);
    public Task<Account> LookupAccount(Account a);
    public Task<Account> updateAccount(Account a);
    public Task<Account> addBookToAccount(Account a);
    public Task<List<Book>> booksInAccount(Account a);
    public Task<List<Room>> roomsInAccount(Account a);
    public Task<List<Room>> addRoomToAccount(Room b, Account a);
    public Task<Book> returnBook(int isbn, Account a);
    public Task<Room> checkoutRoom(int bookID, Account a);


}