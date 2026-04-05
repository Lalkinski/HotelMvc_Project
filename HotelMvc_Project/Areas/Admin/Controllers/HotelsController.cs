using HotelMvc.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HotelMvc.Core.Models.Admin;
namespace HotelMvc.Web.Areas.Admin.Controllers;


[Area("Admin")]
[Authorize(Roles = "Administrator")]
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