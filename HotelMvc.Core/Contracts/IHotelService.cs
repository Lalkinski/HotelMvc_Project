using HotelMvc.Core.Models.Hotel;

namespace HotelMvc.Core.Contracts;

public interface IHotelService
{
    Task<IEnumerable<HotelListItemServiceModel>> GetAllAsync();
}