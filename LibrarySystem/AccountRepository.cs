using LibrarySystem.Models;

using Microsoft.EntityFrameworkCore;

public class AccountRepositry : IAccountRepository
{
    LibraryContext _context;

    public AccountRepositry(LibraryContext context)
    {
        _context = context;
    }


    public async Task<Account> AddAccount(Account a)
    {
        if (await _context.Accounts.AnyAsync(f => f.Email == a.Email))
            throw new AccountExistsException("Account already exists");

        await _context.Accounts.AddAsync(a);
        if (await _context.SaveChangesAsync() >= 1)
            return a;
        throw new GenericException("Something went wrong");

    }

    public async Task<Account> DeleteAccount(Account a)
    {
        if (!(await _context.Accounts.AnyAsync(f => f.Id == a.Id)))
            throw new AccountNotFoundException("Account not found");
        _context.Accounts.Remove(a);
        if (await _context.SaveChangesAsync() >= 1)
            return a;
        throw new GenericException("Account cant be deleted");

    }
    public async Task<Account> LookupAccount(string email, string password)
    {
        Account? account = await _context.Accounts.FirstOrDefaultAsync(f => f.Email == email && f.Password == password);

        if (account is null)
            throw new AccountNotFoundException();
        return account;
    }

    public async Task<Account> LookupAccount(Account a)
    {
        Account? account = await _context.Accounts.FirstOrDefaultAsync(f => f.Id == a.Id);

        if (account is null)
            throw new AccountNotFoundException();
        return account;
    }


    public async Task<Account> UpdateAccount(Account a)
    {
        Account? account = await _context.Accounts.FirstOrDefaultAsync(f => f.Id == a.Id);
        account = a;
        if (await _context.SaveChangesAsync() >= 1)
            return a;
        throw new GenericException("Cannot Update Account");
    }


    public async Task<Book> AddBookToAccount(Account a, Book b)
    {
        Account? account = await _context.Accounts.FirstOrDefaultAsync(f => f.Id == a.Id);
        if (account is null)
            throw new NotLoggedInException();
        account.Books.Add(b);
        if (await _context.SaveChangesAsync() >= 1)
            return b;
        throw new GenericException();
    }

    public async Task<List<Book>> BooksInAccount(Account a)
    {
        Account? account = await _context.Accounts.FirstOrDefaultAsync(f => f.Id == a.Id);
        if (account is null)
            throw new NotLoggedInException();

        List<Book> books = account.Books.ToList();

        return books;
    }
    public async Task<List<Room>> RoomsInAccount(Account a)
    {
        Account? account = await _context.Accounts.FirstOrDefaultAsync(f => f.Id == a.Id);
        if (account is null)
            throw new NotLoggedInException();

        List<Room> rooms = account.Rooms.ToList();

        return rooms;
    }
    public async Task<Room> AddRoomToAccount(Room b, Account a)
    {
        Account? account = await _context.Accounts.FirstOrDefaultAsync(f => f.Id == a.Id);
        if (account is null)
            throw new NotLoggedInException();
        account.Rooms.Add(b);
        if (await _context.SaveChangesAsync() >= 1)
            return b;
        throw new GenericException();
    }

    public async Task<Book> ReturnBook(string isbn, Account a)
    {
        Account? account = await _context.Accounts.FirstOrDefaultAsync(f => f.Id == a.Id);
        if (account is null)
            throw new NotLoggedInException();

        Book? book = account.Books.FirstOrDefault(b => b.Isbn == isbn);
        if (book is null)
            throw new BookNotFoundException();
        book.BorrowedBy = null;

        if (await _context.SaveChangesAsync() >= 1)
            return book;
        throw new GenericException();
    }
    public async Task<Room> CheckoutRoom(string roomID, Account a)
    {
        Account? account = await _context.Accounts.FirstOrDefaultAsync(f => f.Id == a.Id);
        if (account is null)
            throw new NotLoggedInException();

        Room? room = account.Rooms.FirstOrDefault(b => b.Id == roomID);
        if (room is null)
            throw new BookNotFoundException();
        room.Bookedby = null;

        if (await _context.SaveChangesAsync() >= 1)
            return room;
        throw new GenericException();
    }
}