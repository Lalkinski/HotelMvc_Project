using System.ComponentModel.DataAnnotations;

namespace HotelMvc.Core.Models.Reservation;

public class ReservationFormModel
{
    [Required]
    public int RoomId { get; set; }

    [Required]
    [Display(Name = "Check-in Date")]
    [DataType(DataType.Date)]
    public DateTime CheckInDate { get; set; }

    [Required]
    [Display(Name = "Check-out Date")]
    [DataType(DataType.Date)]
    public DateTime CheckOutDate { get; set; }

    [Required]
    [Range(1, 10)]
    [Display(Name = "Guests Count")]
    public int GuestsCount { get; set; }
}