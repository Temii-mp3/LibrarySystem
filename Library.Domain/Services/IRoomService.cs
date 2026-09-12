using LibraryDomain.Models;
using System;

public interface IRoomService
{
    public Task<ICollection<Room>> RoomsInAccount(string email);
    public Task<Room> AddRoomToAccount(string roomID, string email);
    public Task<Room> RemoveRoomFromAccount(string roomID, string email);
    public Task<Room> AddRoomToLibrary(string type);
    public Task<Room> RemoveRoomFromLibrary(string id);
    public Task<ICollection<Room>> GetAllRooms();
    public Task<ICollection<Room>> GetBorrowedRooms(string email);
}