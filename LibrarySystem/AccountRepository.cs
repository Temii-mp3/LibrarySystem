public class AccountRepositry : IAccountRepository
{
    private readonly List<Account> accounts = new();


    public Account addAccount(Account a)
    {
        if (accounts.Exists(f => f.Id == a.Id))
        {
            throw new AccountExistsException("Account already exists");
        }
        else
        {
            accounts.Add(a);
            return a;
        }

        throw new GenericException("Account Exists");
    }

    public Account deleteAccount(Account a)
    {
        if (!accounts.Exists(f => f.Id == a.Id))
        {
            throw new AccountNotFoundException("Account not found");
        }
        else
        {
            accounts.Remove(a);
            return a;
        }
    }
    public Account LookupAccount(string email, string password)
    {
        Account account = accounts.Find(f => f.Email == email && f.Password == password);
        if(account is null)
        {
            throw new AccountNotFoundException("Account not found");
        }

        return account;
    }
    public Account updateAccount(Account a)
    {

    }
    public Account addBookToAccount(Account a)
    {

    }

    public List<Book> booksInAccount(Account a)
    {

    }
    public List<Room> roomsInAccount(Account a)
    {

    }
    public List<Room> addRoomToAccount(Room b, Account a)
    {

    }
    public Book returnBook(int isbn, Account a)
    {

    }
    public Room checkoutRoom(int bookID, Account a)
    {

    }
}