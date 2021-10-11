using HMS_Project.Data;
using HMS_Project.Models;

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

        public IEnumerable<Reservation> GetAll()
        {
            return _context.Reservations.OrderBy(x => x.From).ToList();
        }

        //One(1) SQL query to list how many reservations per day for the next 10 days
        //.The date must be a column and not a row. (If no reservation for a specific date, should shown 0)
        public IEnumerable<Reservation> GetAllForNextDays(int days)
        {
            return _context.Reservations
                .Where(o => o.From >= DateTime.Today && o.From <= DateTime.Today.AddDays(days))
                .OrderBy(x => x.From).ToList();
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
    }
}
