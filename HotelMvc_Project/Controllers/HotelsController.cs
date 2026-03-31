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
    public async Task<IActionResult> Index()
    {
        var hotels = await hotelService.GetAllAsync();

        return View(hotels);
    }
}