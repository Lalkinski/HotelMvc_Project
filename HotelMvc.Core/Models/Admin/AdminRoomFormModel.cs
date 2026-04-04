using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HotelMvc.Core.Models.Admin;

public class AdminRoomFormModel
{
    [Required]
    [Display(Name = "Hotel")]
    public int HotelId { get; set; }

    [Required]
    [Display(Name = "Room Type")]
    public int RoomTypeId { get; set; }

    [Required]
    [Range(1, 10)]
    public int Capacity { get; set; }

    [Required]
    [Range(typeof(decimal), "0.01", "10000")]
    [Display(Name = "Price Per Night")]
    public decimal PricePerNight { get; set; }

    [Display(Name = "Available")]
    public bool IsAvailable { get; set; } = true;

    public IEnumerable<SelectListItem> Hotels { get; set; } = new List<SelectListItem>();

    public IEnumerable<SelectListItem> RoomTypes { get; set; } = new List<SelectListItem>();
}