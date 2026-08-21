using LibrarySystem.Models;
using System;

public interface IAccountService
{
    public Task<Book> addBookToAccount(Book b, Account a);
    public Task<List<Book>> booksInAccount(Account a);
    public Task<List<Room>> roomsInAccount(Account a);
    public Task<Room> addRoomToAccount(Room b, Account a);
    public Task<Book> returnBook(int isbn, Account a);
    public Task<Room> checkoutRoom(int bookID, Account a);
}