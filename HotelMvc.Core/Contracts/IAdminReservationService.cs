using HotelMvc.Core.Models.Admin;

namespace HotelMvc.Core.Contracts;

public interface IAdminReservationService
{
    Task<IEnumerable<AdminReservationListItemServiceModel>> GetAllAsync();

    Task DeleteAsync(int id);
}