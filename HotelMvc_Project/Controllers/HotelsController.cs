using HotelMvc.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HotelMvc.Web.Controllers;

public class HotelsController : Controller
{
    private readonly IHotelService hotelService;

    public HotelsController(IHotelService hotelService)
    {
        this.hotelService = hotelService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string? searchTerm)
    {
        var hotels = await hotelService.GetAllAsync(searchTerm);
        ViewBag.SearchTerm = searchTerm;

        return View(hotels);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var hotel = await hotelService.GetByIdAsync(id);

        if (hotel == null)
        {
            return NotFound();
        }

        return View(hotel);
    }
}