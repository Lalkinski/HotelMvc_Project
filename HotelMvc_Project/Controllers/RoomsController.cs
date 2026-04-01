using HotelMvc.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HotelMvc.Web.Controllers;

public class RoomsController : Controller
{
    private readonly IRoomService roomService;

    public RoomsController(IRoomService roomService)
    {
        this.roomService = roomService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var rooms = await roomService.GetAllAsync();

        return View(rooms);
    }
}