using HotelMvc.Core.Contracts;
using HotelMvc.Core.Models.Admin;
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

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = await roomService.GetCreateModelAsync();

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AdminRoomFormModel model)
    {
        if (!ModelState.IsValid)
        {
            var createModel = await roomService.GetCreateModelAsync();
            model.Hotels = createModel.Hotels;
            model.RoomTypes = createModel.RoomTypes;

            return View(model);
        }

        await roomService.CreateAsync(model);

        return RedirectToAction(nameof(Index));
    }
}