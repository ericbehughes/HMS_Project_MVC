using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_Project.Models
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime From { get; set; }
        [DataType(DataType.Date)]
        public DateTime To { get; set; }
        public int NumberOfPeople { get; set; }
        public bool IsActive { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReservDate { get; set; }
        public int ReservStatus { get; set; }

        public int RequestId { get; set; }
        public Request Request { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

    }
}
