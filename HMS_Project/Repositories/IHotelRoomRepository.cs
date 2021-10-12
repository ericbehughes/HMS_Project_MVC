using HMS_Project.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS_Project.Repositories
{
    public interface IHotelRoomRepository
    {
        Task<IEnumerable<Room>> GetAvailableRooms(DateTime from, DateTime to, int capacity);
        Task<IEnumerable<Room>> GetAvailableRoomsFor(int days);
        Task<IEnumerable<Room>> GetAll();
    }
}
