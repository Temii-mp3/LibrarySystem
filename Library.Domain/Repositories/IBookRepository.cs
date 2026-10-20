using Library.Domain.Models;

public interface IBookRepository
{
    void PrintBooks();
    Task<Book> AddBookToAccount(Book b,int id);
    Task<List<Book>> BooksInAccount(Account a);
    Task<Book> GetBookfromDb(string isbn);
    void PrintDownloadedBooks(Account user);
    Task<ICollection<Book>> GetAllBooks();
    Task<Book> DeleteBook(Book b);


}