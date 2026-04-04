namespace HotelMvc.Core.Models.Admin;

public class AdminGuestListItemServiceModel
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public int ReservationsCount { get; set; }
}