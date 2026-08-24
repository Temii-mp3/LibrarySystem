using LibrarySystem.Models;

public interface IAccountRepository
{
    public Task<Account> AddAccount(Account a);
    public Task<Account> DeleteAccount(Account a);
    public Task<Account> LookupAccount(string email, string password);
    public Task<Account> LookupAccount(Account a);
    public Task<Account> UpdateAccount(Account a);
    public Task<Book> AddBookToAccount(Account a, Book b);
    public Task<List<Book>> BooksInAccount(Account a);
    public Task<List<Room>> RoomsInAccount(Account a);
    public Task<Room> AddRoomToAccount(Room b, Account a);
    public Task<Book> ReturnBook(string isbn, Account a);
    public Task<Room> CheckoutRoom(string bookID, Account a);


}