using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelMvc_Project.Data.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public int GuestId { get; set; }

        [Required]
        public int HotelRoomId { get; set; }

        [Required]
        public DateTime CheckIn { get; set; }

        [Required]
        public DateTime CheckOut { get; set; }

        [Range(1, 20)]
        public int GuestsCount { get; set; } = 1;

        public Guest Guest { get; set; } = null!;
        public HotelRoom HotelRoom { get; set; } = null!;

        [NotMapped]
        public int Nights => (CheckOut.Date - CheckIn.Date).Days;

        [NotMapped]
        public bool IsValidDates => CheckOut.Date > CheckIn.Date;
    }
}
