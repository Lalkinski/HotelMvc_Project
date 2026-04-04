using HotelMvc.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelMvc.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Administrator")]
public class ReservationsController : Controller
{
    private readonly IAdminReservationService adminReservationService;

    public ReservationsController(IAdminReservationService adminReservationService)
    {
        this.adminReservationService = adminReservationService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var reservations = await adminReservationService.GetAllAsync();

        return View(reservations);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await adminReservationService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}