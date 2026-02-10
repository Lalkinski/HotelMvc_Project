using System.ComponentModel.DataAnnotations;

namespace HotelMvc_Project.Data.Models
{
    public class HotelRoom
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string RoomNumber { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string RoomType { get; set; } = "Standart";

        [Range(1, 5)]
        public int Capacity { get; set; } = 2;


        public bool IsAvailable { get; set; } = true;

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
