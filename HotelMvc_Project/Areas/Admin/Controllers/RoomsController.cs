using HotelMvc.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelMvc.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrator")]
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