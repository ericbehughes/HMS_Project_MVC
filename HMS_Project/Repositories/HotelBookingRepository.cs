using HMS_Project.Data;
using HMS_Project.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_Project.Repositories
{
    public class ReservationRepository : IHotelBookingRepository
    {
        private readonly HMS_ProjectContext _context;

        public ReservationRepository(HMS_ProjectContext context)
        {
            _context = context;
        }

        //One(1) SQL query to list how many reservations per day for the next 10 days
        //.The date must be a column and not a row. (If no reservation for a specific date, should shown 0)
        public async Task<IEnumerable<Reservation>> GetReservationsFor(int lookAheadDays)
        {
            var reservations = await _context.Reservations
                .Include(r => r.Request)
                .Include(r => r.Room)
                .Where(o => o.From >= DateTime.Today && o.From <= DateTime.Today.AddDays(lookAheadDays))
                .OrderBy(x => x.From)
                .ToListAsync();
            
            return reservations;
        }

        public async Task SaveRequest(Request request)
        {
            _context.Request.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task SaveReservation(Reservation Reservation)
        {
            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Reservation>> GetAll()
        {
            return await _context.Reservations
               .Include(r => r.Request)
               .Include(r => r.Room)
               .Where(o => o.IsActive == true)
               .OrderBy(x => x.From).ToListAsync();
        }
    }
}
