using System.ComponentModel.DataAnnotations;

namespace HotelMvc.Core.Models.Admin;

public class AdminHotelFormModel
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(200, MinimumLength = 2)]
    public string Location { get; set; } = null!;

    [StringLength(1000)]
    public string? Description { get; set; }

    [Display(Name = "Image URL")]
    [Url]
    public string? ImageUrl { get; set; }
}