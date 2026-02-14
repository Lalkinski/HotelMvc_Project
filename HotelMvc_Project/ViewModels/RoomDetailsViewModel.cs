using HotelMvc_Project.Data.Models;

namespace HotelMvc_Project.ViewModels
{
    public class RoomDetailsViewModel
    {
        public HotelRoom Room { get; set; } = null!;
        public List<Reservation> Reservations { get; set; } = new();
    }
}
