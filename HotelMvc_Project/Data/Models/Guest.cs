using System.ComponentModel.DataAnnotations;

namespace HotelMvc_Project.Data.Models
{
    public class Guest
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Phone]
        [StringLength(30)]
        public string? PhoneNumber { get; set; }

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
