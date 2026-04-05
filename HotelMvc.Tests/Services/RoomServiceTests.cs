using FluentAssertions;
using HotelMvc.Core.Models.Admin;
using HotelMvc.Infrastructure.Models;
using HotelMvc.Infrastructure.Services;
using HotelMvc.Tests.Helpers;
using NUnit.Framework;

namespace HotelMvc.Tests.Services;

[TestFixture]
public class RoomServiceTests
{
    [Test]
    public async Task GetAllAsync_ShouldReturnAllRooms()
    {
        using var context = DatabaseHelper.CreateInMemoryDbContext();

        var hotel = new Hotel { Name = "Hotel One", Location = "Sofia" };
        var roomType = new RoomType { Name = "Single" };

        await context.Hotels.AddAsync(hotel);
        await context.RoomTypes.AddAsync(roomType);
        await context.SaveChangesAsync();

        await context.Rooms.AddRangeAsync(new List<Room>
        {
            new() { HotelId = hotel.Id, RoomTypeId = roomType.Id, Capacity = 1, PricePerNight = 100, IsAvailable = true },
            new() { HotelId = hotel.Id, RoomTypeId = roomType.Id, Capacity = 2, PricePerNight = 150, IsAvailable = false }
        });

        await context.SaveChangesAsync();

        var service = new RoomService(context);

        var result = await service.GetAllAsync();

        result.Should().HaveCount(2);
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnRoomDetails_WhenRoomExists()
    {
        using var context = DatabaseHelper.CreateInMemoryDbContext();

        var hotel = new Hotel { Name = "Hotel One", Location = "Sofia" };
        var roomType = new RoomType { Name = "Double" };

        await context.Hotels.AddAsync(hotel);
        await context.RoomTypes.AddAsync(roomType);
        await context.SaveChangesAsync();

        var room = new Room
        {
            HotelId = hotel.Id,
            RoomTypeId = roomType.Id,
            Capacity = 2,
            PricePerNight = 180,
            IsAvailable = true
        };

        await context.Rooms.AddAsync(room);
        await context.SaveChangesAsync();

        var service = new RoomService(context);

        var result = await service.GetByIdAsync(room.Id);

        result.Should().NotBeNull();
        result!.HotelName.Should().Be("Hotel One");
        result.RoomType.Should().Be("Double");
    }

    [Test]
    public async Task GetCreateModelAsync_ShouldReturnHotelsAndRoomTypes()
    {
        using var context = DatabaseHelper.CreateInMemoryDbContext();

        await context.Hotels.AddRangeAsync(new List<Hotel>
        {
            new() { Name = "Hotel A", Location = "Sofia" },
            new() { Name = "Hotel B", Location = "Varna" }
        });

        await context.RoomTypes.AddRangeAsync(new List<RoomType>
        {
            new() { Name = "Single" },
            new() { Name = "Double" }
        });

        await context.SaveChangesAsync();

        var service = new RoomService(context);

        var result = await service.GetCreateModelAsync();

        result.Hotels.Should().HaveCount(2);
        result.RoomTypes.Should().HaveCount(2);
    }

    [Test]
    public async Task CreateAsync_ShouldAddRoomToDatabase()
    {
        using var context = DatabaseHelper.CreateInMemoryDbContext();

        var hotel = new Hotel { Name = "Hotel One", Location = "Sofia" };
        var roomType = new RoomType { Name = "Apartment" };

        await context.Hotels.AddAsync(hotel);
        await context.RoomTypes.AddAsync(roomType);
        await context.SaveChangesAsync();

        var service = new RoomService(context);

        var model = new AdminRoomFormModel
        {
            HotelId = hotel.Id,
            RoomTypeId = roomType.Id,
            Capacity = 4,
            PricePerNight = 250,
            IsAvailable = true
        };

        await service.CreateAsync(model);

        context.Rooms.Should().HaveCount(1);
        context.Rooms.First().Capacity.Should().Be(4);
        context.Rooms.First().PricePerNight.Should().Be(250);
    }
}