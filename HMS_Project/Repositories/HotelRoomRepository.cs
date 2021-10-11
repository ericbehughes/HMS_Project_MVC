using HMS_Project.Data;
using HMS_Project.Models;

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

        public IEnumerable<Room> GetAll()
        {
            //return _context.Rooms.ToList();
            return null;
        }

        public IEnumerable<Room> GetAvailableRooms(DateTime from, DateTime to, int capacity)
        {
            var bookedHotelBookings = _context.Reservations.
              Where(x => x.IsActive == false && x.To > to)
              .Select(b => b.RoomId)
              .ToList();

            return _context.Rooms
              .Where(x => !bookedHotelBookings.Contains(x.Id))
              .Where(o => o.Capacity <= capacity)
              .ToList();
        }

        //- One(1) SQL query to list every empty room for the next 15 days.
        public IEnumerable<Room> GetAvailableRoomsFor(int days)
        {
            return null;
            //var bookedHotelBookings = _context.HotelBookings.
            //  Where(x => x.IsActive == false && x.From == DateTime.Today && x.To < DateTime.Today.AddDays(days))
            //  .Select(b => b.RoomId)
            //  .ToList();

            //return _context.Rooms
            //  .Where(x => !bookedHotelBookings.Contains(x.Id))
            //  .ToList();
        }
    }
}
