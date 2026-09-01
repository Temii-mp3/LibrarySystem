using LibraryDomain.Models;

public interface IRoomRepository
{
    public void PrintRooms();
    public Task<List<Room>> RoomsInAccount(Account a);
    public Task<Room> AddRoomToAccount(Room b, Account a);
    public Task<Room> GetRoomFromDb(string id);
    public Task<Room> CheckoutRoom(Account a, string roomID);


}