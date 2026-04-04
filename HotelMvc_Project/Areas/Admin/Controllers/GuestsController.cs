using HotelMvc.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelMvc.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrator")]
public class GuestsController : Controller
{
    private readonly IAdminGuestService adminGuestService;

    public GuestsController(IAdminGuestService adminGuestService)
    {
        this.adminGuestService = adminGuestService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var guests = await adminGuestService.GetAllGuestsAsync();

        return View(guests);
    }
}