using HMS_Project.Data;
using HMS_Project.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.Repositories
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly HMS_ProjectContext _context;

        public HotelRoomRepository(HMS_ProjectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAll()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetAvailableRooms(DateTime from, DateTime to, int capacity)
        {
            var emptyRooms = await GetAvailableRoomsFor(0);
            return emptyRooms.Where(o => capacity <= o.Capacity);
        }


        //- One(1) SQL query to list every empty room for the next 15 days.
        public async Task<IEnumerable<Room>> GetAvailableRoomsFor(int days)
        {
            var bookedRoomIDs = await _context
                .Reservations
                .Include(r => r.Room)
                .Where(o => o.IsActive == true)
                .Where(o => o.From >= DateTime.Today && o.To <= DateTime.Today.AddDays(days))
                .Select(o => o.RoomId)
                .ToListAsync();

            var emptyRooms = await _context.Rooms
                .Where(o => !bookedRoomIDs.Contains(o.Id))
                .ToListAsync();

            return emptyRooms;
        }
    }
}
