using HotelMvc.Core.Contracts;
using HotelMvc.Core.Models.Admin;
using HotelMvc.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelMvc.Infrastructure.Services;

public class AdminGuestService : IAdminGuestService
{
    private readonly ApplicationDbContext context;

    public AdminGuestService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<AdminGuestListItemServiceModel>> GetAllGuestsAsync()
    {
        return await context.Users
            .AsNoTracking()
            .Where(u => u.Reservations.Any())
            .Select(u => new AdminGuestListItemServiceModel
            {
                Id = u.Id,
                Email = u.Email ?? "No email",
                PhoneNumber = u.PhoneNumber,
                ReservationsCount = u.Reservations.Count
            })
            .OrderByDescending(u => u.ReservationsCount)
            .ThenBy(u => u.Email)
            .ToListAsync();
    }
}