using System;

public interface IAccountService
{
    public Account addBookToAccount(Book b, Account a);
    public List<Book> booksInAccount(Account a);
    public List<Room> roomsInAccount(Account a);
    public Room addRoomToAccount(Room b, Account a);
    public Book returnBook(int isbn, Account a);
    public Room checkoutRoom(int bookID, Account a);
}
