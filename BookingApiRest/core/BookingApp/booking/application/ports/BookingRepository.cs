using BookingApiRest.core.BookingApp.booking.domain;
using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.booking.application.ports
{
    public interface BookingRepository
    {

        void Save(string employeeId, Booking booking);
        List<Booking> GetBookings(string hotelId, RoomType roomType);
        void Delete(string employeeId);
    }
}
