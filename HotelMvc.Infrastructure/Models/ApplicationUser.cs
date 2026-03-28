using Microsoft.AspNetCore.Identity;

namespace HotelMvc.Infrastructure.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
