using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_Project.Models
{
    public class RequestResult
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int NumberOfPeople { get; set; }
        public bool IsActive { get; set; }
        public int? RoomId { get; internal set; }
        public int ReserveStatus { get; internal set; }
        public int? HotelBookingId { get; set; }
    }
}
