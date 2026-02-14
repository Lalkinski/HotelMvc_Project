using HotelMvc_Project.Data;
using HotelMvc_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelMvc_ASP.Net.Controllers
{
    public class HotelRoomController : Controller
    {
        private readonly HotelMvcDbContext _context;

        public HotelRoomController(HotelMvcDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
                return NotFound();

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
                return NotFound();

            return View(room);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string roomNumber)
        {
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(roomNumber))
            {
                ModelState.AddModelError(nameof(room.RoomNumber), "Room number is required.");
                return View(room);
            }

            room.RoomNumber = roomNumber;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _context.Rooms
                .Include(r => r.Reservations)
                    .ThenInclude(res => res.Guest)
                .OrderBy(r => r.RoomNumber)
                .ToListAsync();

            return View(rooms);
        }

        public async Task<IActionResult> Details(int id)
        {
            var room = await _context.Rooms
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room == null) return NotFound();

            var reservations = await _context.Reservations
                .Include(r => r.Guest)
                .AsNoTracking()
                .Where(r => r.HotelRoomId == id)
                .OrderByDescending(r => r.CheckIn)  
                .ToListAsync();

            var vm = new RoomDetailsViewModel
            {
                Room = room,
                Reservations = reservations
            };

            return View(vm);
        }

    }
}
