using HotelMvc.Core.Models.Admin;
using HotelMvc.Core.Models.Hotel;

namespace HotelMvc.Core.Contracts;

public interface IHotelService
{
    Task<IEnumerable<HotelListItemServiceModel>> GetAllAsync(string? searchTerm = null);
    Task<HotelDetailsServiceModel?> GetByIdAsync(int id);
    Task CreateAsync(AdminHotelFormModel model);
}