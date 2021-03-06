using HMS_Project.Models;
using HMS_Project.Repositories;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace HMS_Project.Services
{
    public class HotelBookingRequestProcessor : IHotelBookingRequestProcessor
    {
        private IHotelBookingRepository _hotelBookingRepository;
        private IHotelRoomRepository _hotelRoomRepository;

        public HotelBookingRequestProcessor(IHotelBookingRepository hotelBookingRepository, IHotelRoomRepository hotelRoomRepository)
        {
            _hotelBookingRepository = hotelBookingRepository;
            _hotelRoomRepository = hotelRoomRepository;
        }

        public async Task<RequestResult> BookHotelRoom(Request hotelRequest)
        {
            if (hotelRequest == null)
                throw new ArgumentNullException(nameof(hotelRequest));

            if (hotelRequest.To <= hotelRequest.From)
            {
                throw new InvalidOperationException(nameof(hotelRequest));
            }

            await _hotelBookingRepository.SaveRequest(hotelRequest);
            var availableRooms = await _hotelRoomRepository.GetAvailableRooms(hotelRequest.From, hotelRequest.To, hotelRequest.NumberOfPeople);

            var hotelBookingResult = new RequestResult();

            if (availableRooms.Any())
            {
                var firstAvailableRoom = availableRooms.First();
                var reservation = new Reservation()
                {
                    FirstName = hotelRequest.FirstName,
                    LastName = hotelRequest.LastName,
                    From = hotelRequest.From,
                    To = hotelRequest.To,
                    IsActive = true,
                    NumberOfPeople = hotelRequest.NumberOfPeople,
                    RoomId = firstAvailableRoom.Id,
                    ReservStatus = 1,
                    ReservDate = DateTime.Today,
                    RequestId = hotelRequest.Id
                };
                await _hotelBookingRepository.SaveReservation(reservation);
                hotelBookingResult.ReserveStatus = 1;
                hotelBookingResult.RequestId = hotelRequest.Id;
                hotelBookingResult.HotelBookingId = reservation.Id;
            }
            else
            {
                // assign reserve status to 0 if no save is done
                hotelBookingResult.ReserveStatus = 0;
                hotelBookingResult.RequestId = hotelRequest.Id;
            }
            return hotelBookingResult;
            
        }
    }
}
