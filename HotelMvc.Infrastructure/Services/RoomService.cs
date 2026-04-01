using HotelMvc.Core.Contracts;
using HotelMvc.Core.Models.Room;
using HotelMvc.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelMvc.Infrastructure.Services;

public class RoomService : IRoomService
{
    private readonly ApplicationDbContext context;

    public RoomService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<RoomListItemServiceModel>> GetAllAsync()
    {
        return await context.Rooms
            .AsNoTracking()
            .Include(r => r.Hotel)
            .Include(r => r.RoomType)
            .Select(r => new RoomListItemServiceModel
            {
                Id = r.Id,
                HotelName = r.Hotel.Name,
                RoomType = r.RoomType.Name,
                Capacity = r.Capacity,
                PricePerNight = r.PricePerNight,
                IsAvailable = r.IsAvailable
            })
            .ToListAsync();
    }
}