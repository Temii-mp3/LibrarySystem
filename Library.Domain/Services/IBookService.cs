using LibraryDomain.Models;
using System;

public interface IBookService
{
    public Task<Book> AddBookToAccount(Book b, Account a);
    public Task<ICollection<Book>> BooksInAccount(Account a);
    public Task<Book> ReturnBook(string isbn, Account a);

    public Task<Book> AddBookToLibrary(string isbn, string author, string name);
}