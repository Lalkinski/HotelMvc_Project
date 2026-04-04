using HotelMvc.Core.Models.Admin;
using HotelMvc.Core.Models.Room;

namespace HotelMvc.Core.Contracts;

public interface IRoomService
{
    Task<IEnumerable<RoomListItemServiceModel>> GetAllAsync();

    Task<RoomDetailsServiceModel?> GetByIdAsync(int id);

    Task<AdminRoomFormModel> GetCreateModelAsync();

    Task CreateAsync(AdminRoomFormModel model);
}