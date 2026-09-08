using LibraryDomain.Models;

public interface IBookRepository
{
    void PrintBooks();
    Task<Book> AddBookToAccount(Book b,int id);
    Task<List<Book>> BooksInAccount(Account a);
    Task<Book> GetBookfromDb(string isbn);
    void PrintBorrowedBooks(Account user);
    Task<Book> ReturnBook(Book b);
    Task<Book> AddBookToDB(Book b);
    Task<ICollection<Book>> GetAllBooks();
    Task<Book> DeleteBook(Book b); 


}