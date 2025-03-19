using BookingApiRest.core.BookingApp.booking.application;
using BookingApiRest.core.BookingApp.booking.application.ports;
using BookingApiRest.core.BookingApp.booking.application.requests;
using BookingApiRest.core.BookingApp.booking.domain;
using BookingApiRest.core.BookingApp.company.domain;
using BookingApiRest.core.BookingApp.policy.application;
using BookingApiRest.core.shared.application;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApiRest.Core.Shared.Domain;
using NSubstitute;
using Shouldly;

namespace BookingApiRest.Test.booking
{
    public class BookingServiceShould
    {
        private BookingService _bookingService;
        private EventBus _eventBus;
        private BookingRepository _bookingRepository;

        [SetUp]
        public void SetUp()
        {
            _bookingRepository = Substitute.For<BookingRepository>();
            _eventBus = Substitute.For<EventBus>();

            this._bookingService = new BookingService(_bookingRepository, _eventBus);
        }

        [Test]
        public async Task book_a_room()
        {
            var employeeId = Guid.NewGuid().ToString();
            var hotelId = Guid.NewGuid().ToString();
            var roomType = RoomType.Standard;
            var checkIn = DateTime.Now;
            var checkOut = DateTime.Now.AddDays(3);

            _eventBus.PublishAndWait<IsBookingAllowRequest, BooleanResult>(Arg.Any<IsBookingAllowRequest>())
                .Returns(new Result<BooleanResult>(new BooleanResult(true), true));

            _eventBus.PublishAndWait<GetHotelRoomsCapacityRequest, IntResult>(Arg.Any<GetHotelRoomsCapacityRequest>())
                .Returns(new Result<IntResult>(new IntResult(5), true));

            _bookingRepository.Received().Save(Arg.Is<string>(id => id == employeeId),
                 Arg.Is<Booking>(b =>
                 b.EmployeeId == hotelId
                 && b.RoomType == roomType
                 && b.CheckIn == checkIn
                 && b.CheckOut == checkOut
             ));

            var result = await _bookingService.BookRoom(hotelId, employeeId, roomType, checkIn, checkOut);

            result.IsSuccess.ShouldBeTrue(); 

            var booking = result.GetValue();
            booking.ShouldNotBeNull();
            booking.EmployeeId.ShouldBe(employeeId);
            booking.RoomType.ShouldBe(roomType);
            booking.CheckIn.ShouldBe(checkIn);
            booking.CheckOut.ShouldBe(checkOut);
        }
    }
}
