using LibraryDomain.Models;

public interface IRoomRepository
{
    public void PrintRooms();
    public Task<List<Room>?> RoomsInAccount(Account a);
    public Task<Room?> AddRoomToAccount(int id, Room r);
    public Task<Room?> GetRoomFromDb(string id);
    public Task<Room?> RemoveRoomFromAccount( Room room);
    public Task<Room?> RemoveRoomFromLibrary(Room room);
    public Task<Room?> AddRoomToLibrary(Room r);
    public Task<ICollection<Room>?> GetAllRooms();
    public Task<ICollection<Room>?> GetBorrowedRooms(Account a);
}