namespace HotelMvc.Core.Models.Hotel;

public class HotelDetailsServiceModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }
}