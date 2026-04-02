using HotelMvc.Core.Contracts;
using HotelMvc.Core.Models.Reservation;
using HotelMvc.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelMvc.Web.Controllers;

[Authorize]
public class ReservationsController : Controller
{
    private readonly IReservationService reservationService;
    private readonly UserManager<ApplicationUser> userManager;

    public ReservationsController(
        IReservationService reservationService,
        UserManager<ApplicationUser> userManager)
    {
        this.reservationService = reservationService;
        this.userManager = userManager;
    }

    [HttpGet]
    public IActionResult Create(int roomId)
    {
        var model = new ReservationFormModel
        {
            RoomId = roomId,
            CheckInDate = DateTime.Today,
            CheckOutDate = DateTime.Today.AddDays(1)
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ReservationFormModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return Unauthorized();
        }

        bool isCreated = await reservationService.CreateAsync(user.Id, model);

        if (!isCreated)
        {
            ModelState.AddModelError(string.Empty, "Reservation was not created. Check the dates, guests count, and room availability.");
            return View(model);
        }

        //return RedirectToAction("Index", "Rooms");
        return RedirectToAction(nameof(My));
    }

    [HttpGet]
    public async Task<IActionResult> My()
    {
        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return Unauthorized();
        }

        var reservations = await reservationService.GetUserReservationsAsync(user.Id);

        return View(reservations);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return Unauthorized();
        }

        var reservation = await reservationService.GetReservationDetailsAsync(id, user.Id);

        if (reservation == null)
        {
            return NotFound();
        }

        return View(reservation);
    }
}