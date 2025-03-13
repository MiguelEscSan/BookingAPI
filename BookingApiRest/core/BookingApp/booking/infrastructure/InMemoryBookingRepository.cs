using BookingApiRest.core.BookingApp.booking.application.ports;
using BookingApiRest.core.BookingApp.booking.domain;

namespace BookingApiRest.core.BookingApp.booking.infrastructure
{
    public class InMemoryBookingRepository : BookingRepository
    {
        internal readonly Dictionary<string, List<Booking>> _bookings = new Dictionary<string, List<Booking>>();


        public void Save(string employeeId, Booking booking)
        {
            if (!_bookings.ContainsKey(employeeId))
            {
                _bookings[employeeId] = new List<Booking>();
            }

            _bookings[employeeId].Add(booking);
        }
    }
}
