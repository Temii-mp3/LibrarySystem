using LibraryDomain.Models;

public interface IRoomRepository
{
    public void PrintRooms();
    public Task<List<Room>> RoomsInAccount(Account a);
    public Task<Room> AddRoomToAccount(int id, Room r);
    public Task<Room> GetRoomFromDb(string id);
    public Task<Room> CheckoutRoom( Room room);


}