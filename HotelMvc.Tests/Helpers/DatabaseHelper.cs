using HotelMvc.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelMvc.Tests.Helpers;

public static class DatabaseHelper
{
    public static ApplicationDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }
}