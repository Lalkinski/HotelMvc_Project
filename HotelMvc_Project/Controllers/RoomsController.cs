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

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var room = await roomService.GetByIdAsync(id);

        if (room == null)
        {
            return NotFound();
        }

        return View(room);
    }
}