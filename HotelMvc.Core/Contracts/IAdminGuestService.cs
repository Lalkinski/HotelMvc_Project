using HotelMvc.Core.Models.Admin;

namespace HotelMvc.Core.Contracts;

public interface IAdminGuestService
{
    Task<IEnumerable<AdminGuestListItemServiceModel>> GetAllGuestsAsync();
}