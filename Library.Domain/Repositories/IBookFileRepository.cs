using Library.Domain.Models;

public interface IBookFileRepository
{
    Task AddAsync(BookFile file);
    Task<BookFile?> GetByBookAndFormatAsync(int bookId, BookFormat format);
    Task<List<BookFile>> GetByBookIdAsync(int bookId);
    Task DeleteAsync(BookFile file);
}