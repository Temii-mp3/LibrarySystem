using LibraryDomain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.XPath;

namespace Library.Infrastructure.Services
{
    public class RoomService : IRoomService
    {
        List<string> roomTypes = new List<string> { "conference", "study" };
        const int ROOMLIMIT = 1;
        IRoomRepository room_repo;
        IAccountRepository account_repo;
        public RoomService(IRoomRepository roomRepo, IAccountRepository accountRepo)
        {
            room_repo = roomRepo;
            account_repo = accountRepo;
        }

        public async Task<ICollection<Room>> RoomsInAccount(string email)
        {
            Account? user = await account_repo.LookupAccount(email);
            if (user is null)
                throw new AccountNotFoundException("Account not found");

            ICollection<Room>? roomsInAcc = await room_repo.RoomsInAccount(user);
            if (roomsInAcc is null)
                throw new GenericException();

            return roomsInAcc;

        }
        public async Task<Room> AddRoomToAccount(string roomID, string email)
        {
            Room? room = await room_repo.GetRoomFromDb(roomID);
            Account user = await account_repo.LookupAccount(email);

            if (user.Rooms.Count > ROOMLIMIT)
                throw new BookLimitReachedException("Room Limit of {LIMIT} has been reached");

            if (room is null)
                throw new RoomNotFoundException();
            if (user is null)
                throw new NotLoggedInException();

            Room? result = await room_repo.AddRoomToAccount(user.Id, room);

            if (result is null)
                throw new GenericException();
            return result;
        }

        public async Task<Room> RemoveRoomFromAccount(string roomID, string email)
        {
            Room? room = await room_repo.GetRoomFromDb(roomID);
            Account user = await account_repo.LookupAccount(email);

            if (room is null)
                throw new RoomNotFoundException();
            if (user is null)
                throw new AccountNotFoundException();

            ICollection<Room>? borrowedRooms = await room_repo.GetBorrowedRooms(user);
            if (borrowedRooms is null)
                throw new GenericException();
            if(!borrowedRooms.Contains(room))
                throw new GenericException("Room not in account");

            Room? result = await room_repo.RemoveRoomFromAccount(room);
            if (result is null)
                throw new GenericException();
            return result;
        }

        public async Task<Room> AddRoomToLibrary(string type)
        {
            if (!roomTypes.Contains(type.ToLower()))
                throw new GenericException();

            Room room = new Room
            {
                Type = type,
                Bookedby = null,
                BookedbyNavigation = null
            };

            Room? result = await room_repo.AddRoomToLibrary(room);
            if (result is not null)
                return result;
            throw new GenericException();

        }

        public async Task<Room> RemoveRoomFromLibrary(string roomID)
        {
            Room? room = await room_repo.GetRoomFromDb(roomID);
            if (room is null)
                throw new GenericException();
            Room? result = await room_repo.RemoveRoomFromLibrary(room);

            if (result is null)
                throw new GenericException();
            return result;
        }

        public async Task<ICollection<Room>> GetAllRooms()
        {
            ICollection<Room>? rooms = await room_repo.GetAllRooms();
            if (rooms is null)
                throw new GenericException();
            return rooms;
        }

        public async Task<ICollection<Room>> GetBorrowedRooms(string email)
        {
            Account user = await account_repo.LookupAccount(email);
            if (user is null)
                throw new AccountNotFoundException();
            ICollection<Room>? borrowedRooms = await room_repo.GetBorrowedRooms(user);
            if (borrowedRooms is null)
                throw new GenericException();
            return borrowedRooms;
        }
    }
}
