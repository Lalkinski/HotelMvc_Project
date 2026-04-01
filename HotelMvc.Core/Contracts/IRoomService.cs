using HotelMvc.Core.Models.Room;

namespace HotelMvc.Core.Contracts;

public interface IRoomService
{
    Task<IEnumerable<RoomListItemServiceModel>> GetAllAsync();

    Task<RoomDetailsServiceModel?> GetByIdAsync(int id);
}