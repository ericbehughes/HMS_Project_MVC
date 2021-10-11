using HMS_Project.Models;

using System.Threading.Tasks;

namespace HMS_Project.Services
{
    public interface IHotelBookingRequestProcessor
    {
        Task<RequestResult> BookHotelRoom(Request request);
    }
}