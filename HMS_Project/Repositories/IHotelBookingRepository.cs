using HMS_Project.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.Repositories
{
    public interface IHotelBookingRepository
    {
        Task SaveRequest(Request request);
        Task SaveReservation(Reservation hotelBooking);
        IEnumerable<Reservation> GetAll();
    }
}
