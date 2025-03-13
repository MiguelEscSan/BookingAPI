using BookingApiRest.core.BookingApp.booking.application.ports;
using BookingApiRest.core.BookingApp.booking.domain;
using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.booking.infrastructure
{
    public class InMemoryBookingRepository : BookingRepository
    {
        internal readonly Dictionary<string, List<Booking>> _bookings = new Dictionary<string, List<Booking>>();


        public void Save(string hotelId, Booking booking)
        {
            if (!_bookings.ContainsKey(hotelId))
            {
                _bookings[hotelId] = new List<Booking>();
            }

            _bookings[hotelId].Add(booking);
        }

        public List<Booking> GetBookings(string hotelId, RoomType roomType)
        {
            if (!_bookings.ContainsKey(hotelId))
            {
                return new List<Booking>();
            }

            return _bookings[hotelId].FindAll(book => book.RoomType == roomType);
        }


        public void Delete(string employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
