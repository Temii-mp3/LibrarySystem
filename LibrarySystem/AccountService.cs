using System;
using LibraryDomain.Models;
public class AccountService : IAccountService
{
    const int BOOKLIMIT = 5;
    const int ROOMLIMIT = 1;
    private readonly IAccountRepository accountRepository;
    public AccountService(AccountRepositry _repo)
    {
        accountRepository = _repo;
    }

    public async Task<Book> AddBookToAccount(Book b, Account a)
    {
        Account? user = await accountRepository.LookupAccount(a);
        if (user is null)
            throw new AccountNotFoundException("Account not found");
        if (user.Books.Count > BOOKLIMIT)
            throw new BookLimitReachedException($"Book Limit of {BOOKLIMIT} has been reached");
        b.BorrowedBy = user.Id;
        return b;
    }

    public async Task<ICollection<Book>> BooksInAccount(Account a)
    {
        Account? user = await accountRepository.LookupAccount(a);
        if (user is null)
            throw new AccountNotFoundException("Account not found");

        return user.Books;


    }
    public async Task<ICollection<Room>> RoomsInAccount(Account a)
    {
        Account? user = await accountRepository.LookupAccount(a);
        if (user is null)
            throw new AccountNotFoundException("Account not found");

        return user.Rooms;

    }
    public async Task<Room> AddRoomToAccount(Room b, Account a)
    {
        Account? user = await accountRepository.LookupAccount(a);
        if (user is null)
            throw new AccountNotFoundException();
        if (a.Rooms.Count > ROOMLIMIT)
            throw new RoomLimitReachedException("Room Limit of {LIMIT} has been reached");
        b.Bookedby = user.Id;
        return b;
    }
    public async Task<Book> ReturnBook(string isbn, Account a)
    {
        Account? user = await accountRepository.LookupAccount(a);

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
        Account? user = await accountRepository.LookupAccount(a);

        if (user is null)
            throw new AccountNotFoundException();
        Room? room = user.Rooms.FirstOrDefault(b => b.Id == roomID);
        if (room is null)
            throw new BookNotFoundException();
        room.Bookedby = null;
        return room;
    }
}
