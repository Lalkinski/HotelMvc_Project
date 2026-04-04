using HotelMvc.Core.Contracts;
using HotelMvc.Core.Models.Admin;
using HotelMvc.Core.Models.Room;
using HotelMvc.Infrastructure.Data;
using HotelMvc.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    public async Task<RoomDetailsServiceModel?> GetByIdAsync(int id)
    {
        return await context.Rooms
            .AsNoTracking()
            .Where(r => r.Id == id)
            .Select(r => new RoomDetailsServiceModel
            {
                Id = r.Id,
                HotelName = r.Hotel.Name,
                RoomType = r.RoomType.Name,
                Capacity = r.Capacity,
                PricePerNight = r.PricePerNight,
                IsAvailable = r.IsAvailable
            })
            .FirstOrDefaultAsync();
    }
    public async Task<AdminRoomFormModel> GetCreateModelAsync()
    {
        var hotels = await context.Hotels
            .AsNoTracking()
            .OrderBy(h => h.Name)
            .Select(h => new SelectListItem
            {
                Value = h.Id.ToString(),
                Text = h.Name
            })
            .ToListAsync();

        var roomTypes = await context.RoomTypes
            .AsNoTracking()
            .OrderBy(rt => rt.Name)
            .Select(rt => new SelectListItem
            {
                Value = rt.Id.ToString(),
                Text = rt.Name
            })
            .ToListAsync();

        return new AdminRoomFormModel
        {
            Hotels = hotels,
            RoomTypes = roomTypes
        };
    }

    public async Task CreateAsync(AdminRoomFormModel model)
    {
        var room = new Room
        {
            HotelId = model.HotelId,
            RoomTypeId = model.RoomTypeId,
            Capacity = model.Capacity,
            PricePerNight = model.PricePerNight,
            IsAvailable = model.IsAvailable
        };

        await context.Rooms.AddAsync(room);
        await context.SaveChangesAsync();
    }
}