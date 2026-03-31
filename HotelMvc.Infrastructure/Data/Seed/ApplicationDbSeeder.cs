using HotelMvc.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelMvc.Infrastructure.Data.Seed;

public static class ApplicationDbSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        await context.Database.MigrateAsync();

        await SeedRolesAsync(roleManager);
        await SeedAdminAsync(userManager);
        await SeedRoomTypesAsync(context);
        await SeedAmenitiesAsync(context);
        await SeedHotelsAsync(context);
        await SeedRoomsAsync(context);

        await context.SaveChangesAsync();
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { "Administrator", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
    {
        const string adminEmail = "admin@hotelmvc.com";
        const string adminPassword = "Admin123!";

        var admin = await userManager.FindByEmailAsync(adminEmail);

        if (admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(admin, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Administrator");
            }
        }
    }

    private static async Task SeedRoomTypesAsync(ApplicationDbContext context)
    {
        if (await context.RoomTypes.AnyAsync()) return;

        var roomTypes = new List<RoomType>
        {
            new() { Name = "Single" },
            new() { Name = "Double" },
            new() { Name = "Deluxe" },
            new() { Name = "Apartment" }
        };

        await context.RoomTypes.AddRangeAsync(roomTypes);
    }

    private static async Task SeedAmenitiesAsync(ApplicationDbContext context)
    {
        if (await context.Amenities.AnyAsync()) return;

        var amenities = new List<Amenity>
        {
            new() { Name = "WiFi" },
            new() { Name = "Pool" },
            new() { Name = "Parking" },
            new() { Name = "Spa" },
            new() { Name = "Restaurant" }
        };

        await context.Amenities.AddRangeAsync(amenities);
    }

    private static async Task SeedHotelsAsync(ApplicationDbContext context)
    {
        if (await context.Hotels.AnyAsync()) return;

        var hotels = new List<Hotel>
        {
            new()
            {
                Name = "Grand Palace Hotel",
                Location = "Sofia",
                Description = "Luxury hotel in the city center",
                ImageUrl = "https://example.com/hotel1.jpg"
            },
            new()
            {
                Name = "Sea View Resort",
                Location = "Varna",
                Description = "Beautiful seaside resort",
                ImageUrl = "https://example.com/hotel2.jpg"
            },
            new()
            {
                Name = "Mountain Escape Lodge",
                Location = "Bansko",
                Description = "Cozy mountain hotel",
                ImageUrl = "https://example.com/hotel3.jpg"
            }
        };

        await context.Hotels.AddRangeAsync(hotels);
        await context.SaveChangesAsync();
    }

    private static async Task SeedRoomsAsync(ApplicationDbContext context)
    {
        if (await context.Rooms.AnyAsync()) return;

        var hotels = await context.Hotels.ToListAsync();
        var roomTypes = await context.RoomTypes.ToListAsync();

        var rooms = new List<Room>
        {
            new()
            {
                HotelId = hotels[0].Id,
                RoomTypeId = roomTypes.First(rt => rt.Name == "Single").Id,
                Capacity = 1,
                PricePerNight = 90,
                IsAvailable = true
            },
            new()
            {
                HotelId = hotels[1].Id,
                RoomTypeId = roomTypes.First(rt => rt.Name == "Double").Id,
                Capacity = 2,
                PricePerNight = 150,
                IsAvailable = true
            }
        };

        await context.Rooms.AddRangeAsync(rooms);
    }
}