using System.ComponentModel.DataAnnotations;

namespace HotelMvc.Infrastructure.Models;

public class Review
{
    public int Id { get; set; }

    public int HotelId { get; set; }
    public Hotel Hotel { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;

    [Range(1, 5)]
    public int Rating { get; set; }

    [MaxLength(1000)]
    public string Comment { get; set; } = null!;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}