using FluentAssertions;
using HotelMvc.Core.Models.Admin;
using HotelMvc.Infrastructure.Models;
using HotelMvc.Infrastructure.Services;
using HotelMvc.Tests.Helpers;
using NUnit.Framework;

namespace HotelMvc.Tests.Services;

[TestFixture]
public class HotelServiceTests
{
    [Test]
    public async Task GetAllAsync_ShouldReturnAllHotels_WhenNoSearchTermIsProvided()
    {
        using var context = DatabaseHelper.CreateInMemoryDbContext();

        await context.Hotels.AddRangeAsync(new List<Hotel>
        {
            new() { Name = "Grand Palace", Location = "Sofia" },
            new() { Name = "Sea View", Location = "Varna" }
        });

        await context.SaveChangesAsync();

        var service = new HotelService(context);

        var result = await service.GetAllAsync();

        result.Should().HaveCount(2);
    }

    [Test]
    public async Task GetAllAsync_ShouldFilterHotelsBySearchTerm()
    {
        using var context = DatabaseHelper.CreateInMemoryDbContext();

        await context.Hotels.AddRangeAsync(new List<Hotel>
        {
            new() { Name = "Grand Palace", Location = "Sofia" },
            new() { Name = "Sea View", Location = "Varna" },
            new() { Name = "Mountain Lodge", Location = "Bansko" }
        });

        await context.SaveChangesAsync();

        var service = new HotelService(context);

        var result = await service.GetAllAsync("Varna");

        result.Should().HaveCount(1);
        result.First().Name.Should().Be("Sea View");
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnHotelDetails_WhenHotelExists()
    {
        using var context = DatabaseHelper.CreateInMemoryDbContext();

        var hotel = new Hotel
        {
            Name = "Grand Palace",
            Location = "Sofia",
            Description = "Luxury hotel",
            ImageUrl = "test.jpg"
        };

        await context.Hotels.AddAsync(hotel);
        await context.SaveChangesAsync();

        var service = new HotelService(context);

        var result = await service.GetByIdAsync(hotel.Id);

        result.Should().NotBeNull();
        result!.Name.Should().Be("Grand Palace");
        result.Location.Should().Be("Sofia");
    }

    [Test]
    public async Task CreateAsync_ShouldAddHotelToDatabase()
    {
        using var context = DatabaseHelper.CreateInMemoryDbContext();

        var service = new HotelService(context);

        var model = new AdminHotelFormModel
        {
            Name = "New Hotel",
            Location = "Plovdiv",
            Description = "Nice hotel",
            ImageUrl = "image.jpg"
        };

        await service.CreateAsync(model);

        context.Hotels.Should().HaveCount(1);
        context.Hotels.First().Name.Should().Be("New Hotel");
    }
}