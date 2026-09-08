using LibraryDomain.Models;
using System;

public interface IRoomService
{
    public Task<ICollection<Room>> RoomsInAccount(Account a);
    public Task<Room> AddRoomToAccount(Room b, Account a);
    public Task<Room> CheckoutRoom(string bookID, Account a);
    public Task<Room> AddRoomToLibrary(string type);
    public ICollection<Room> GetAllRooms();
    public ICollection<Room> GetCheckedOutRooms(int id);
}