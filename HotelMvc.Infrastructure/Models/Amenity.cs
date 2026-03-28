using System.ComponentModel.DataAnnotations;

namespace HotelMvc.Infrastructure.Models;

public class Amenity
{
    public int Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public ICollection<HotelAmenity> HotelsAmenities { get; set; } = new List<HotelAmenity>();
}