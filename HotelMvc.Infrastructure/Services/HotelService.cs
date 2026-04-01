using HotelMvc.Core.Contracts;
using HotelMvc.Core.Models.Hotel;
using HotelMvc.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelMvc.Infrastructure.Services;

public class HotelService : IHotelService
{
    private readonly ApplicationDbContext context;

    public HotelService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<HotelListItemServiceModel>> GetAllAsync()
    {
        return await context.Hotels
            .AsNoTracking()
            .Select(h => new HotelListItemServiceModel
            {
                Id = h.Id,
                Name = h.Name,
                Location = h.Location,
                ImageUrl = h.ImageUrl
            })
            .ToListAsync();
    }

    public async Task<HotelDetailsServiceModel?> GetByIdAsync(int id)
    {
        return await context.Hotels
            .AsNoTracking()
            .Where(h => h.Id == id)
            .Select(h => new HotelDetailsServiceModel
            {
                Id = h.Id,
                Name = h.Name,
                Location = h.Location,
                Description = h.Description,
                ImageUrl = h.ImageUrl
            })
            .FirstOrDefaultAsync();
    }
}