using LibraryDomain.Models;

public interface IBookRepository
{
    public void PrintBooks();
    public Task<Book> AddBookToAccount(int id, Book b);
    public Task<List<Book>> BooksInAccount(Account a);
    public Task<Book> GetBookfromDb(string isbn);
    public void PrintBorrowedBooks(Account user);
    public Task<Book> ReturnBook(Book b);
    public Task<Book> AddBookToDB(Book b);


}