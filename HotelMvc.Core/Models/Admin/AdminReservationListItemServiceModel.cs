namespace HotelMvc.Core.Models.Admin;

public class AdminReservationListItemServiceModel
{
    public int Id { get; set; }

    public string UserEmail { get; set; } = null!;

    public string HotelName { get; set; } = null!;

    public string RoomType { get; set; } = null!;

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public int GuestsCount { get; set; }

    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = null!;
}