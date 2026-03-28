namespace HotelMvc.Infrastructure.Models;

public class Reservation
{
    public int Id { get; set; }

    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;

    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }

    public int GuestsCount { get; set; }

    public decimal TotalPrice { get; set; }

    public string Status { get; set; } = "Pending";
}