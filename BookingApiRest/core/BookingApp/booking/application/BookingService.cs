using BookingApiRest.core.BookingApp.booking.application.ports;
using BookingApiRest.core.BookingApp.booking.domain;
using BookingApiRest.core.BookingApp.company.domain;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApiRest.Core.Shared.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace BookingApiRest.core.BookingApp.booking.application
{
    public class BookingService
    {

        private readonly BookingRepository _bookingRepository;

        public BookingService(BookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public Booking BookRoom(string employeeId, string hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut)
        {
            var Booking = new Booking(hotelId, roomType, checkIn, checkOut);
            _bookingRepository.Save(employeeId, Booking);
            return Booking;
        }
    }
}
