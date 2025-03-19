using BookingApiRest.core.BookingApp.booking.domain;
using BookingApiRest.core.BookingApp.booking.infrastructure;
using BookingApiRest.core.BookingApp.company.infrastructure;
using BookingApiRest.Core.Shared.Domain;
using Shouldly;

namespace BookingApiRest.Test.booking
{
    public class InMemoryBookingRepositoryGetBookingsShould
    {

        private InMemoryBookingRepository _inMemoryBookingRepository;
        private string employeeId;
        private string hotelId;
        private Booking booking;

        [SetUp]
        public void SetUp()
        {
            _inMemoryBookingRepository = new InMemoryBookingRepository();
            employeeId = Guid.NewGuid().ToString();
            hotelId = Guid.NewGuid().ToString();
            booking = new Booking(employeeId, RoomType.Standard, DateTime.Now, DateTime.Now.AddDays(3));

            _inMemoryBookingRepository.Save(hotelId, booking);

        }

        [Test]
        public void return_the_bookings_of_an_hotel() {         
            var result = _inMemoryBookingRepository.GetBookings(hotelId, RoomType.Standard);

            result.ShouldContain(booking);
        }

        [Test]
        public void return_an_empty_list_if_there_are_no_bookings()
        {
            var result = _inMemoryBookingRepository.GetBookings(hotelId, RoomType.Suite);

            result.ShouldBeEmpty();
        }
    }
}
