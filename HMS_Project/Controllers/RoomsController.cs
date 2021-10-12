using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HMS_Project.Data;
using HMS_Project.Models;
using HMS_Project.Repositories;

namespace HMS_Project.Controllers
{
    public class RoomsController : Controller
    {
        private readonly HMS_ProjectContext _context;
        private readonly IHotelRoomRepository _hotelRoomRepository;

        public RoomsController(HMS_ProjectContext context, IHotelRoomRepository hotelRoomRepository)
        {
            _context = context;
            _hotelRoomRepository = hotelRoomRepository;
        }

        // GET: Rooms
        public async Task<IActionResult> Index(string days)
        {
            int lookAheadDays;
            if (string.IsNullOrWhiteSpace(days) || !int.TryParse(days,out lookAheadDays))
                return View(await _hotelRoomRepository.GetAll());

           var emptyRooms = await _hotelRoomRepository.GetAvailableRoomsFor(lookAheadDays);

            return View(emptyRooms);
        }

        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}
