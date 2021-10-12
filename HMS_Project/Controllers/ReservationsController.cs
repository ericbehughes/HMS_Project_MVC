using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HMS_Project.Data;
using HMS_Project.Models;
using HMS_Project.Services;
using HMS_Project.Repositories;

namespace HMS_Project.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly HMS_ProjectContext _context;
        private readonly IHotelBookingRequestProcessor _hotelBookingRequestProcessor;
        private readonly IHotelBookingRepository _hotelBookingRepository;

        public ReservationsController(HMS_ProjectContext context, IHotelBookingRequestProcessor hotelBookingRequestProcessor, IHotelBookingRepository hotelBookingRepository)
        {
            _context = context;
            _hotelBookingRequestProcessor = hotelBookingRequestProcessor;
            _hotelBookingRepository = hotelBookingRepository;
        }

        // GET: Reservations
        public async Task<IActionResult> Index(string days)
        {
            int lookAheadDays;
            if (string.IsNullOrWhiteSpace(days) || !int.TryParse(days, out lookAheadDays))
                return View(await _hotelBookingRepository.GetAll());
            
            var reservations = await _hotelBookingRepository.GetReservationsFor(lookAheadDays);
            return View(reservations);
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Request)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["RequestId"] = new SelectList(_context.Set<Request>(), "Id", "Id");
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "Id", "Id");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,From,To,NumberOfPeople")] Request request)
        {
            if (ModelState.IsValid)
            {
                if (request.To < request.From)
                {
                    return BadRequest("To date must be before to");
                }
                var result = await _hotelBookingRequestProcessor.BookHotelRoom(request);
                if (result.ReserveStatus == 1)
                {
                    ViewData["RequestId"] = new SelectList(_context.Set<Request>(), "Id", "Id", result.HotelBookingId);
                    ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "Id", "Id", result.RoomId);
                    return RedirectToAction(nameof(Index));
                }
                else if (result.ReserveStatus == 0)
                {
                    ModelState.AddModelError("DeskBookingRequest.Date",
                      "No desk available for selected date");
                    return RedirectToAction(nameof(Index));
                }
                
            }
            return View();
            
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["RequestId"] = new SelectList(_context.Set<Request>(), "Id", "Id", reservation.RequestId);
            ViewData["RoomId"] = new SelectList(_context.Set<Room>(), "Id", "Id", reservation.RoomId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,From,To,NumberOfPeople,IsActive,ReservDate,ReservStatus,RequestId,RoomId")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Request)
                .Include(r => r.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
