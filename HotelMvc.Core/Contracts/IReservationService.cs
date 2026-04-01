using HotelMvc.Core.Models.Reservation;

namespace HotelMvc.Core.Contracts;

public interface IReservationService
{
    Task<bool> CreateAsync(string userId, ReservationFormModel model);

    Task<IEnumerable<ReservationListItemServiceModel>> GetUserReservationsAsync(string userId);
}