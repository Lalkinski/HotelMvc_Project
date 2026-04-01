using HotelMvc.Core.Contracts;
using HotelMvc.Core.Models.Reservation;
using HotelMvc.Infrastructure.Data;
using HotelMvc.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelMvc.Infrastructure.Services;

public class ReservationService : IReservationService
{
    private readonly ApplicationDbContext context;

    public ReservationService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<bool> CreateAsync(string userId, ReservationFormModel model)
    {
        var room = await context.Rooms
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == model.RoomId);

        if (room == null)
        {
            return false;
        }

        if (model.CheckInDate >= model.CheckOutDate)
        {
            return false;
        }

        if (model.GuestsCount > room.Capacity)
        {
            return false;
        }

        bool hasOverlappingReservation = await context.Reservations.AnyAsync(r =>
            r.RoomId == model.RoomId &&
            model.CheckInDate < r.CheckOutDate &&
            model.CheckOutDate > r.CheckInDate);

        if (hasOverlappingReservation)
        {
            return false;
        }

        int nights = (model.CheckOutDate - model.CheckInDate).Days;

        var reservation = new Reservation
        {
            RoomId = model.RoomId,
            UserId = userId,
            CheckInDate = model.CheckInDate,
            CheckOutDate = model.CheckOutDate,
            GuestsCount = model.GuestsCount,
            TotalPrice = nights * room.PricePerNight,
            Status = "Confirmed"
        };

        await context.Reservations.AddAsync(reservation);
        await context.SaveChangesAsync();

        return true;
    }
}