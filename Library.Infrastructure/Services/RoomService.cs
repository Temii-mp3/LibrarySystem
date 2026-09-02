using LibraryDomain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Services
{
    public class RoomService : IRoomService
    {
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
    }
}
