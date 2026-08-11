public interface IAccountRepository
{
    public Account addAccount(Account a); 
    public Account deleteAccount(Account a);
    public Account LookupAccount(string email, string password);
    public Account updateAccount(Account a);
    public Account addBookToAccount(Account a); 
    public List<Book> booksInAccount(Account a);
    public List<Room> roomsInAccount(Account a);
    public List<Room> addRoomToAccount(Room b, Account a);
    public Book returnBook(int isbn, Account a);
    public Room checkoutRoom(int bookID, Account a);

}