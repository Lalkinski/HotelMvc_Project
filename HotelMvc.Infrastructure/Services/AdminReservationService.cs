using HotelMvc.Core.Contracts;
using HotelMvc.Core.Models.Admin;
using HotelMvc.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelMvc.Infrastructure.Services;

public class AdminReservationService : IAdminReservationService
{
    private readonly ApplicationDbContext context;

    public AdminReservationService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<AdminReservationListItemServiceModel>> GetAllAsync()
    {
        return await context.Reservations
            .AsNoTracking()
            .Include(r => r.User)
            .Include(r => r.Room)
                .ThenInclude(room => room.Hotel)
            .Include(r => r.Room)
                .ThenInclude(room => room.RoomType)
            .OrderByDescending(r => r.CheckInDate)
            .Select(r => new AdminReservationListItemServiceModel
            {
                Id = r.Id,
                UserEmail = r.User.Email ?? "No email",
                HotelName = r.Room.Hotel.Name,
                RoomType = r.Room.RoomType.Name,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                GuestsCount = r.GuestsCount,
                TotalPrice = r.TotalPrice,
                Status = r.Status
            })
            .ToListAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var reservation = await context.Reservations.FindAsync(id);

        if (reservation == null)
        {
            return;
        }

        context.Reservations.Remove(reservation);
        await context.SaveChangesAsync();
    }
}