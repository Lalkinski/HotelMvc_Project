namespace HotelMvc.Core.Models.Hotel;

public class HotelListItemServiceModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public string? ImageUrl { get; set; }
}