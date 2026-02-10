using System.ComponentModel.DataAnnotations;

namespace HotelMvc_Project.ViewModels
{
    public class CreateReservationViewModel
    {
        /* Guest */
        [Required]
        public string GuestFirstName { get; set; } = null!;

        [Required]
        public string GuestLastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string GuestEmail { get; set; } = null!;

        [Required]
        [Phone]
        public string? GuestPhoneNumber { get; set; }

        //Room
        [Required]
        public string RoomNumber { get; set; } = null!;

        public string RoomType { get; set; } = "Standard";

        public int RoomCapacity { get; set; } = 2;

        // Reservation
        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }

        public int GuestsCount { get; set; } = 1;

    }
}
