using LibraryDomain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Text.RegularExpressions;
public class AccountService : IAccountService
{
    const int BOOKLIMIT = 5;
    const int ROOMLIMIT = 1;
    private readonly IAccountRepository _repo;
    private readonly IPasswordHasher<Account> _hasher;
    public AccountService(IAccountRepository repo, IPasswordHasher<Account> hasher)
    {
        _repo = repo;
        _hasher = hasher;
    }

    public async Task<Book> AddBookToAccount(Book b, Account a)
    {
        Account? user = await _repo.LookupAccount(a);
        if (user is null)
            throw new AccountNotFoundException("Account not found");
        if (user.Books.Count > BOOKLIMIT)
            throw new BookLimitReachedException($"Book Limit of {BOOKLIMIT} has been reached");
        b.BorrowedBy = user.Id;
        return b;
    }

    public async Task<ICollection<Book>> BooksInAccount(Account a)
    {
        Account? user = await _repo.LookupAccount(a);
        if (user is null)
            throw new AccountNotFoundException("Account not found");

        return user.Books;


    }
    public async Task<ICollection<Room>> RoomsInAccount(Account a)
    {
        Account? user = await _repo.LookupAccount(a);
        if (user is null)
            throw new AccountNotFoundException("Account not found");

        return user.Rooms;

    }
    public async Task<Room> AddRoomToAccount(Room b, Account a)
    {
        Account? user = await _repo.LookupAccount(a);
        if (user is null)
            throw new AccountNotFoundException();
        if (a.Rooms.Count > ROOMLIMIT)
            throw new RoomLimitReachedException("Room Limit of {LIMIT} has been reached");
        b.Bookedby = user.Id;
        return b;
    }
    public async Task<Book> ReturnBook(string isbn, Account a)
    {
        Account? user = await _repo.LookupAccount(a);

        if (user is null)
            throw new AccountNotFoundException();
        Book? book = user.Books.FirstOrDefault(b => b.Isbn == isbn);
        if (book is null)
            throw new BookNotFoundException();
        book.BorrowedBy = null;
        return book;

    }
    public async Task<Room> CheckoutRoom(string roomID, Account a)
    {
        Account? user = await _repo.LookupAccount(a);

        if (user is null)
            throw new AccountNotFoundException();
        Room? room = user.Rooms.FirstOrDefault(b => b.Id == roomID);
        if (room is null)
            throw new BookNotFoundException();
        room.Bookedby = null;
        return room;
    }

    public async Task<Account> AddAccountToDB(string email, string password, string username)
    {

        if (!CheckEmail(email))
            throw new InvalidEmailFormatException();
        if (!CheckPassword(password))
            throw new InvalidPasswordFormatException();
        if (!CheckUser(username))
            throw new InvalidUsernameFormatException();

        if (await _repo.LookupAccount(email) is not null)
            throw new AccountExistsException();

        Account user = new Account
        {
            Email = email,
            Username = username
        };
        var hashedPassword = _hasher.HashPassword(user, password);
        user.Password = hashedPassword;
        Account result = await _repo.AddAccount(user);

        return result;
    }


    static bool CheckEmail(string? email)
    {
        if (email is not null)
        {
            return Regex.IsMatch(email, @"^[\w\.-]+@[\w\.-]+\.\w+$");
        }
        return false;
    }

    static bool CheckPassword(string? password)
    {
        if (password is not null)
        {
            return Regex.IsMatch(password, @"^\w{8,}$");
        }
        return false;
    }

    static bool CheckUser(String? user)
    {
        if (user is not null)
        {
            return Regex.IsMatch(user, @"^\w{3,}$");
        }

        return false;
    }
}
