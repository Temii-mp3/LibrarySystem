using System;

public class AccountService : IAccountService
{
    const int BOOKLIMIT = 5;
    const int ROOMLIMIT = 1;
    private readonly IAccountRepository accountRepository;
    public AccountService(AccountRepositry _repo)
    {
        accountRepository = _repo;
    }

    public Task<Book> addBookToAccount(Book b, Account a)
    {
        if (accountRepository.LookupAccount(a) is null)
        {
            throw new AccountNotFoundException("Account not found");
        }

        if (a.Books.Count > 5)
        {
            throw new BookLimitReachedException($"Book Limit of {BOOKLIMIT} has been reached");
        }

        a.addBook(b);
        return a;
    }

    public List<Book> booksInAccount(Account a)
    {

        if (a is not null && accountRepository.LookupAccount(a) is not null)
        {
            return a.Books;
        }

        throw new AccountNotFoundException("Account not found");


    }
    public List<Room> roomsInAccount(Account a)
    {
        if (a is not null && accountRepository.LookupAccount(a) is not null)
        {
            return a.Rooms;
        }

        throw new AccountNotFoundException("Account not found");

    }
    public Room addRoomToAccount(Room b, Account a)
    {
        if (accountRepository.LookupAccount(a) is not null && a.Rooms.Count() < ROOMLIMIT)
        {
            a.addRoom(b);
            return b;
        }
        throw new RoomLimitReachedException("Room Limit of {LIMIT} has been reached");
    }
    public Book returnBook(int isbn, Account a)
    {
        List<Book> bookArr = a.Books;


        Book book = bookArr.Find(r => r.ISBN == isbn);

        if (book != null)
        {
            bookArr.Remove(book);
            return book;
        }
        throw new BookNotFoundException("Book not found");
    }
    public Room checkoutRoom(int roomID, Account a)
    {
        List<Room> roomArr = a.Rooms;

        Room room = roomArr.Find(r => r.Id == roomID);

        if (room != null)
        {
            roomArr.Remove(room);
            return room;
        }
        throw new RoomNotFoundException("Room not found");
    }
}
