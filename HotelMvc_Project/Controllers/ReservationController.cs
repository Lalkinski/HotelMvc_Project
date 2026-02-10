using HotelMvc_Project.Data;
using HotelMvc_Project.Data.Models;
using HotelMvc_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.RegularExpressions;

namespace HotelMvc_Project.Controllers
{
    public class ReservationController : Controller
    {
        private readonly HotelMvcDbContext _reservation;

        public ReservationController(HotelMvcDbContext reservation)
        {
            _reservation = reservation;
        }

        public async Task<IActionResult> Index()
        {
            var reservation = await _reservation.Reservations
                .Include(r => r.Guest)
                .Include(r => r.HotelRoom)
                .OrderByDescending(r => r.Id)
                .ToListAsync();

            return View(reservation);
        }

        //Create - GET
        public IActionResult Create()
        {
            var vm = new CreateReservationViewModel
            {
                CheckIn = DateTime.Today,
                CheckOut = DateTime.Today.AddDays(1)
            };

            return View(vm);
        }

        //Create - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateReservationViewModel vm)
        {
            if (vm.CheckOut.Date <= vm.CheckIn.Date)
            {
                ModelState.AddModelError("", "Check-out must be after check-in.");
            }

            if (!Regex.IsMatch(vm.GuestPhoneNumber ?? string.Empty, @"^(\+|0)?\d+$"))
            {
                ModelState.AddModelError(nameof(vm.GuestPhoneNumber), "Phone number must contain only digits, optionally starting with '+' or '0'.");
            }

            if (!string.IsNullOrWhiteSpace(vm.RoomNumber))
            {
                var hasRoomReservation = await _reservation.Reservations
                    .Include(r => r.HotelRoom)
                    .AnyAsync(r => r.HotelRoom != null && r.HotelRoom.RoomNumber == vm.RoomNumber);
                if (hasRoomReservation)
                {
                    ModelState.AddModelError(nameof(vm.RoomNumber), "A reservation already exists for this room number.");
                }
            }

            if (!string.IsNullOrWhiteSpace(vm.GuestFirstName))
            {
                if (!Regex.IsMatch(vm.GuestFirstName ?? string.Empty, @"^\p{L}+$"))
                {
                    ModelState.AddModelError(nameof(vm.GuestFirstName), "First name must contain only letters.");
                }
            }

            if (!string.IsNullOrWhiteSpace(vm.GuestLastName))
            {
                if (!Regex.IsMatch(vm.GuestLastName ?? string.Empty, @"^\p{L}+$"))
                {
                    ModelState.AddModelError(nameof(vm.GuestLastName), "Last name must contain only letters.");
                }
            }

            if (vm.GuestsCount > 5)
            {
                ModelState.AddModelError(nameof(vm.GuestsCount), "Max guests count of room is 5");
            }


            if (!ModelState.IsValid)
                return View(vm);


            var guest = await _reservation.Guests.FirstOrDefaultAsync(g => g.Email == vm.GuestEmail);
            if (guest == null)
            {
                guest = new Guest
                {
                    FirstName = vm.GuestFirstName,
                    LastName = vm.GuestLastName,
                    Email = vm.GuestEmail,
                    PhoneNumber = vm.GuestPhoneNumber
                };

                _reservation.Guests.Add(guest);
                await _reservation.SaveChangesAsync();
            }



            var room = await _reservation.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == vm.RoomNumber);
            if (room == null)
            {
                room = new HotelRoom
                {
                    RoomNumber = vm.RoomNumber,
                    RoomType = vm.RoomType,
                    Capacity = vm.RoomCapacity,
                    IsAvailable = true
                };

                _reservation.Rooms.Add(room);
                await _reservation.SaveChangesAsync();
            }


            var reservation = new Reservation
            {
                GuestId = guest.Id,
                HotelRoomId = room.Id,
                CheckIn = vm.CheckIn,
                CheckOut = vm.CheckOut,
                GuestsCount = vm.GuestsCount
            };

            _reservation.Reservations.Add(reservation);
            await _reservation.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var reservation = await _reservation.Reservations
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return RedirectToAction(nameof(Index));

            _reservation.Reservations.Remove(new Reservation { Id = id });
            await _reservation.SaveChangesAsync();

            var guestStillUsed = await _reservation.Reservations
                .AnyAsync(r => r.GuestId == reservation.GuestId);

            if (!guestStillUsed)
            {
                _reservation.Guests.Remove(new Guest { Id = reservation.GuestId });
            }

            var roomStillUsed = await _reservation.Reservations
                .AnyAsync(r => r.HotelRoomId == reservation.HotelRoomId);

            if (!roomStillUsed)
            {
                _reservation.Rooms.Remove(new HotelRoom { Id = reservation.HotelRoomId });
            }

            await _reservation.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
