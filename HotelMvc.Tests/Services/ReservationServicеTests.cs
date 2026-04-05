using FluentAssertions;
using HotelMvc.Core.Models.Reservation;
using HotelMvc.Infrastructure.Models;
using HotelMvc.Infrastructure.Services;
using HotelMvc.Tests.Helpers;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace HotelMvc.Tests.Services
{
    [TestFixture]
    public class ReservationServicеTests
    {
        [Test]
        public async Task CreateAsync_ShouldReturnFalse_WhenRoomNotFound()
        {
            using var context = DatabaseHelper.CreateInMemoryDbContext();

            var service = new ReservationService(context);

            var model = new ReservationFormModel
            {
                RoomId = 999, 
                CheckInDate = System.DateTime.Today.AddDays(1),
                CheckOutDate = System.DateTime.Today.AddDays(2),
                GuestsCount = 1
            };

            var result = await service.CreateAsync("user-id", model);

            result.Should().BeFalse();
            context.Reservations.Should().BeEmpty();
        }

        [Test]
        public async Task GetUserReservationsAsync_ShouldReturnReservations_ForGivenUser()
        {
            using var context = DatabaseHelper.CreateInMemoryDbContext();

            var hotel = new Hotel { Name = "Test Hotel", Location = "City" };
            var roomType = new RoomType { Name = "Single" };
            await context.Hotels.AddAsync(hotel);
            await context.RoomTypes.AddAsync(roomType);
            await context.SaveChangesAsync();

            var room = new Room
            {
                HotelId = hotel.Id,
                RoomTypeId = roomType.Id,
                Capacity = 2,
                PricePerNight = 100,
                IsAvailable = true
            };
            await context.Rooms.AddAsync(room);
            await context.SaveChangesAsync();

            var reservation = new Reservation
            {
                RoomId = room.Id,
                UserId = "user-123",
                CheckInDate = System.DateTime.Today.AddDays(1),
                CheckOutDate = System.DateTime.Today.AddDays(3),
                GuestsCount = 2,
                TotalPrice = 200,
                Status = "Confirmed"
            };
            await context.Reservations.AddAsync(reservation);
            await context.SaveChangesAsync();

            var service = new ReservationService(context);

            var list = (await service.GetUserReservationsAsync("user-123")).ToList();

            list.Should().HaveCount(1);
            var item = list[0];
            item.HotelName.Should().Be(hotel.Name);
            item.RoomType.Should().Be(roomType.Name);
            item.TotalPrice.Should().Be(200);
            item.Status.Should().Be("Confirmed");
        }

        [Test]
        public async Task GetReservationDetailsAsync_ShouldReturnDetails_ForReservationAndUser()
        {
            using var context = DatabaseHelper.CreateInMemoryDbContext();

            var hotel = new Hotel { Name = "Hotel X", Location = "Town" };
            var roomType = new RoomType { Name = "Double" };
            await context.Hotels.AddAsync(hotel);
            await context.RoomTypes.AddAsync(roomType);
            await context.SaveChangesAsync();

            var room = new Room
            {
                HotelId = hotel.Id,
                RoomTypeId = roomType.Id,
                Capacity = 3,
                PricePerNight = 120,
                IsAvailable = true
            };
            await context.Rooms.AddAsync(room);
            await context.SaveChangesAsync();

            var reservation = new Reservation
            {
                RoomId = room.Id,
                UserId = "abc-user",
                CheckInDate = System.DateTime.Today.AddDays(4),
                CheckOutDate = System.DateTime.Today.AddDays(6),
                GuestsCount = 2,
                TotalPrice = 240,
                Status = "Confirmed"
            };
            await context.Reservations.AddAsync(reservation);
            await context.SaveChangesAsync();

            var service = new ReservationService(context);

            var details = await service.GetReservationDetailsAsync(reservation.Id, "abc-user");

            details.Should().NotBeNull();
            details!.HotelName.Should().Be(hotel.Name);
            details.RoomType.Should().Be(roomType.Name);
            details.TotalPrice.Should().Be(240);
            details.Status.Should().Be("Confirmed");
        }
    }
}