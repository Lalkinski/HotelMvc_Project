using HotelMvc_Project.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelMvc_ASP.Net.Controllers
{
    public class GuestController : Controller
    {
        private readonly HotelMvcDbContext _db;

        public GuestController(HotelMvcDbContext db)
        {
            _db = db;
        }

        // GET - Guest
        public async Task<IActionResult> Index()
        {
            var guests = await _db.Guests
                .Include(g => g.Reservations)
                .OrderByDescending(g => g.Id)
                .ToListAsync();

            return View(guests);
        }

        // GET - Guest - Details
        public async Task<IActionResult> Details(int id)
        {
            var guest = await _db.Guests
                .Include(g => g.Reservations)
                    .ThenInclude(r => r.HotelRoom)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (guest == null)
                return NotFound();

            return View(guest);
        }

        // POST - Guest - Delete 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var guest = await _db.Guests
                .Include(g => g.Reservations)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (guest == null)
            {
                TempData["Error"] = "Guest not found.";
                return RedirectToAction(nameof(Index));
            }

            //Cannot delete guest if they have reservations
            if (guest.Reservations != null && guest.Reservations.Any())
            {
                TempData["Error"] = "Cannot delete this guest because they have reservations.";
                return RedirectToAction(nameof(Index));
            }

            _db.Guests.Remove(guest);
            await _db.SaveChangesAsync();

            TempData["Success"] = "Guest deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
