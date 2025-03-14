using BookingApiRest.core.BookingApp.booking.domain;
using BookingApiRest.core.BookingApp.booking.infrastructure;
using BookingApiRest.Core.Shared.Domain;
using Shouldly;

namespace BookingApiRest.Test.booking
{
    public class InMemoryBookingRepositoryDeleteBookingsShould
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
        public void delete_existing_booking()
        {
            _inMemoryBookingRepository.Delete(employeeId);
            var bookings = _inMemoryBookingRepository._bookings[hotelId];

            bookings.ShouldNotContain(b => b.EmployeeId == employeeId);
        }
    }
}
