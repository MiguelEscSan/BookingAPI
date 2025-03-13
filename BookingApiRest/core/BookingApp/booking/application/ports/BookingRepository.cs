using BookingApiRest.core.BookingApp.booking.domain;

namespace BookingApiRest.core.BookingApp.booking.application.ports
{
    public interface BookingRepository
    {

        void Save(string employeeId, Booking booking);
    }
}
