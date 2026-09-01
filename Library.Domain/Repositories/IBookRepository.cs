using LibraryDomain.Models;

public interface IBookRepository
{
    public void PrintBooks();
    public Task<Book> AddBookToAccount(Account a, Book b);
    public Task<List<Book>> BooksInAccount(Account a);
    public Task<Book> GetBookfromDb(string isbn);
    public void PrintBorrowedBooks(Account user);
    public Task<Book> ReturnBook(Account a, string isbn);


}