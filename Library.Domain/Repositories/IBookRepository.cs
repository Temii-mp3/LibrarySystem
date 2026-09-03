using LibraryDomain.Models;

public interface IBookRepository
{
    void PrintBooks();
    Task<Book> AddBookToAccount(int id, Book b);
    Task<List<Book>> BooksInAccount(Account a);
    Task<Book> GetBookfromDb(string isbn);
    void PrintBorrowedBooks(Account user);
    Task<Book> ReturnBook(Book b);
    Task<Book> AddBookToDB(Book b);
    Task<ICollection<Book>> GetAllBooks();


}