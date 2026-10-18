using Library.Api.Controllers;
using Library.Api.DTO;
using Library.Domain.Models;
using Library.Infrastructure.Auth;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace LibrarySystemTests
{
    public class RoomTests
    {
        [Fact]
        public async Task AddRoomToLibrary_ReturnsOk_WhenServiceSucceeds()
        {
            var svc = new Mock<IRoomService>();
            var current = new Mock<ICurrentUserService>();
            var controller = new RoomController(svc.Object, current.Object);

            var room = new Room { Id = "123", Type = "study" };
            svc.Setup(s => s.AddRoomToLibrary("study")).ReturnsAsync(room);

            var req = new CreateRoomRequest("study");
            var result = await controller.AddRoomToLbrary(req);

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(room, ok.Value);
        }

        [Fact]
        public async Task AddRoomToLibrary_ReturnsBadRequest_WhenServiceReturnsNull()
        {
            var svc = new Mock<IRoomService>();
            var current = new Mock<ICurrentUserService>();
            var controller = new RoomController(svc.Object, current.Object);

            var req = new CreateRoomRequest("study");
            svc.Setup(s => s.AddRoomToLibrary("study")).ReturnsAsync((Room?)null);

            var result = await controller.AddRoomToLbrary(req);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task CheckoutRoom_ReturnsOk_WhenAuthenticatedAndServiceSucceeds()
        {
            var svc = new Mock<IRoomService>();
            var current = new Mock<ICurrentUserService>();
            current.Setup(c => c.GetEmail()).Returns("a@b.com");
            var controller = new RoomController(svc.Object, current.Object);

            var room = new Room { Id = "123", Type = "conference", Bookedby = 1 };
            svc.Setup(s => s.AddRoomToAccount("2", "a@b.com")).ReturnsAsync(room);

            var result = await controller.AddRoomToAccount("2");

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(room, ok.Value);
        }

        [Fact]
        public async Task CheckoutRoom_ReturnsForbid_WhenUnauthenticated()
        {
            var svc = new Mock<IRoomService>();
            var current = new Mock<ICurrentUserService>();
            current.Setup(c => c.GetEmail()).Returns<string?>(null);
            var controller = new RoomController(svc.Object, current.Object);

            var result = await controller.AddRoomToAccount("2");

            Assert.IsType<ForbidResult>(result);
        }

        [Fact]
        public async Task GetBorrowedRooms_ReturnsOk_WhenAuthenticated()
        {
            var svc = new Mock<IRoomService>();
            var current = new Mock<ICurrentUserService>();
            current.Setup(c => c.GetEmail()).Returns("a@b.com");
            var controller = new RoomController(svc.Object, current.Object);

            var rooms = new List<Room> { new Room { Id = "123", Bookedby = 1 } };
            svc.Setup(s => s.GetBorrowedRooms("a@b.com")).ReturnsAsync(rooms);

            var result = await controller.GetBookedRoomsInAccount();

            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(rooms, ok.Value);
        }

        [Fact]
        public async Task GetBorrowedRooms_ReturnsForbid_WhenUnauthenticated()
        {
            var svc = new Mock<IRoomService>();
            var current = new Mock<ICurrentUserService>();
            current.Setup(c => c.GetEmail()).Returns<string?>(null);
            var controller = new RoomController(svc.Object, current.Object);

            var result = await controller.GetBookedRoomsInAccount();

            Assert.IsType<ForbidResult>(result);
        }
    }
}