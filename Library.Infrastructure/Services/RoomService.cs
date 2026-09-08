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

        public async Task<ICollection<Room>> RoomsInAccount(Account a)
        {
            Account? user = await account_repo.LookupAccount(a);
            if (user is null)
                throw new AccountNotFoundException("Account not found");

            return await room_repo.RoomsInAccount(user);

        }
        public async Task<Room> AddRoomToAccount(Room b, Account a)
        {
            Account? user = await account_repo.LookupAccount(a);
            if (user is null)
                throw new AccountNotFoundException();
            if (a.Rooms.Count > ROOMLIMIT)
                throw new RoomLimitReachedException("Room Limit of {LIMIT} has been reached");
            b.Bookedby = user.Id;
            return b;
        }

        public async Task<Room> CheckoutRoom(string roomID, Account a)
        {
            Account? user = await account_repo.LookupAccount(a);

            if (user is null)
                throw new AccountNotFoundException();
            Room? room = user.Rooms.FirstOrDefault(b => b.Id == roomID);
            if (room is null)
                throw new BookNotFoundException();
            room.Bookedby = null;
            return room;
        }

        public async Task<Room> AddRoomToLibrary(string type)
        {
            if (!roomTypes.Contains(type.ToLower()))
                throw new GenericException();

            Room room = new Room
            {
                Type = type
            };

            Room result = await room_repo.AddRoomToLibrary(room);
            if (result is not null)
                return result;
            throw new GenericException();

        }

        public async Task<ICollection<Room>> GetAllRooms()
        {
            ICollection<Room> rooms = room_repo.GetAllRooms();
            if (rooms is null)
                throw new GenericException();
            return rooms;
        }

        public async Task<ICollection<Room>> GetBorrowedRooms()
        {
            return null;
        }
    }
}
