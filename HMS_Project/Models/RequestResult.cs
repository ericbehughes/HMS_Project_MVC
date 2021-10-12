using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_Project.Models
{
    public class RequestResult
    {
        public int? RoomId { get; internal set; }
        public int ReserveStatus { get; internal set; }
        public int? HotelBookingId { get; set; }
        public int RequestId { get; internal set; }
    }
}
