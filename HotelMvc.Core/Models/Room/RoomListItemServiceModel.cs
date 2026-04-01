namespace HotelMvc.Core.Models.Room;

public class RoomListItemServiceModel
{
    public int Id { get; set; }

    public string HotelName { get; set; } = null!;

    public string RoomType { get; set; } = null!;

    public int Capacity { get; set; }

    public decimal PricePerNight { get; set; }

    public bool IsAvailable { get; set; }
}