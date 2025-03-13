using BookingApiRest.core.BookingApp.booking.domain;
using BookingApiRest.core.BookingApp.booking.infrastructure;
using BookingApiRest.Core.Shared.Domain;
using Shouldly;

namespace BookingApiRest.Test.booking
{
    public class InMemoryBookingRepositoryShould
    {

        private readonly InMemoryBookingRepository _inMemoryBookingRepository;
        private readonly string employeeId = Guid.NewGuid().ToString();
        private readonly string hotelId = Guid.NewGuid().ToString();

        public InMemoryBookingRepositoryShould()
        {
            _inMemoryBookingRepository = new InMemoryBookingRepository();
        }

        [Test]
        public void save_a_single_booking()
        {
            var checkIn = DateTime.Now;
            var checkOut = DateTime.Now.AddDays(3);
            var booking = new Booking(hotelId, RoomType.Standard, checkIn, checkOut);

            _inMemoryBookingRepository.Save(employeeId, booking);

            var result = _inMemoryBookingRepository._bookings[employeeId][0];

            result.RoomType.ShouldBe(RoomType.Standard);
        }
    }
}
