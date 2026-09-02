using LibraryDomain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Infrastructure.Repositories
{

    public class RoomRepository : IRoomRepository
    {
        LibraryContext _context;

        public RoomRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<List<Room>> RoomsInAccount(Account a)
        {
            List<Room> rooms = a.Rooms.ToList();
            return rooms;
        }
        public async Task<Room> AddRoomToAccount(int id, Room r)
        {
            r.Bookedby = id;
            if (await _context.SaveChangesAsync() >= 1)
                return r;
            throw new GenericException();
        }


        public async Task<Room> CheckoutRoom(Room room)
        {
            room.Bookedby = null;

            if (await _context.SaveChangesAsync() >= 1)
                return room;
            throw new GenericException();
        }

        public void PrintRooms()
        {
            _context.Rooms.ForEachAsync(Console.WriteLine);
        }

        public async Task<Room> GetRoomFromDb(string id)
        {
            Room? room = await _context.Rooms.FirstOrDefaultAsync(b => b.Id == id);
            if (room is null)
                throw new RoomNotFoundException();
            return room;
        }

        public void PrintBookedRooms(Account user)
        {
            foreach (var item in user.Rooms)
            {
                Console.WriteLine(item);
            }
        }
    }
}
