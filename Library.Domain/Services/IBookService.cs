using LibraryDomain.Models;
using System;

public interface IBookService
{
    public Task<Book> AddBookToAccount(string isbn, string email);
    public Task<ICollection<Book>> BooksInAccount(Account a);
    public Task<Book> ReturnBook(string isbn);

    public Task<Book> AddBookToLibrary(string isbn, string author, string name);

    public Task<ICollection<Book>> GetAllBooks();
    public Task<Book> DeleteBook(string isbn);
}