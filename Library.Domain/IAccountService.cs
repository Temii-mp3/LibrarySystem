using LibraryDomain.Models;
using System;

public interface IAccountService
{
    public Task<Book> AddBookToAccount(Book b, Account a);
    public Task<ICollection<Book>> BooksInAccount(Account a);
    public Task<ICollection<Room>> RoomsInAccount(Account a);
    public Task<Room> AddRoomToAccount(Room b, Account a);
    public Task<Book> ReturnBook(string isbn, Account a);
    public Task<Room> CheckoutRoom(string bookID, Account a);

    public Task<Account> AddAccountToDB(string email, string password, string username);
}