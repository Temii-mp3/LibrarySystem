using LibraryDomain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

public class AccountRepositry : IAccountRepository
{
    LibraryContext _context;

    public AccountRepositry(LibraryContext context)
    {
        _context = context;
    }


    public async Task<Account> AddAccount(Account a)
    {
        try
        {
            await _context.Accounts.AddAsync(a);
            if (await _context.SaveChangesAsync() >= 1)
                return a;
        }
        catch (DbUpdateException)
        {
            throw new GenericException("Something went wrong");
        }
        throw new GenericException("Something went wrong");
    }

    public async Task<Account> DeleteAccount(Account a)
    {
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

    public async Task<Account?> LookupAccount(string email)
    {
        Account? account = await _context.Accounts.FirstOrDefaultAsync(f => f.Email == email);

        if (account is null)
            return null;
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
        if (b.BorrowedBy is not null)
            throw new BookBorrowedException();
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

    public async Task<Book> ReturnBook( Account a, string isbn)
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
    public async Task<Room> CheckoutRoom(Account a, string roomID)
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

    public void PrintBooks()
    {
         _context.Books.ForEachAsync(Console.WriteLine);
    }


    public void PrintRooms()
    {
         _context.Rooms.ForEachAsync(Console.WriteLine);
    }


    public async Task<Book> GetBookfromDb(string isbn)
    {
        Book? book = await _context.Books.FirstOrDefaultAsync(b => b.Isbn == isbn);
        if (book is null)
            throw new BookNotFoundException();
        return book;
    }
    public async Task<Room> GetRoomFromDb(string id)
    {
        Room? room = await _context.Rooms.FirstOrDefaultAsync(b => b.Id == id);
        if (room is null)
            throw new RoomNotFoundException();
        return room;
    }

    public void PrintBorrowedBooks(Account user)
    {
        foreach (var item in user.Books)
        {
            Console.WriteLine(item);
        }
    }

    public void PrintBookedRooms(Account user)
    {
        foreach (var item in user.Rooms)
        {
            Console.WriteLine(item);
        }
    }


}