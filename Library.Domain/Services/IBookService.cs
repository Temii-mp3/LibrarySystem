using LibraryDomain.Models;
using System;
using System.Reflection.Metadata;

public interface IBookService
{
    public Task<Book> AddBookToAccount(string isbn, string email);
    public Task<ICollection<Book>> BooksInAccount(string email);
    public Task<Book> ReturnBook(string isbn, string userEmail);

    public Task<Book> AddBookToLibrary(string isbn, string author, string name);

    public Task<ICollection<Book>> GetAllBooks();
    public Task<Book> DeleteBook(string isbn);
}