using BookingApiRest.core.BookingApp.booking.application;
using BookingApiRest.core.BookingApp.booking.application.ports;
using BookingApiRest.core.BookingApp.booking.application.requests;
using BookingApiRest.core.BookingApp.booking.domain;
using BookingApiRest.core.BookingApp.company.domain;
using BookingApiRest.core.BookingApp.policy.application;
using BookingApiRest.core.shared.application;
using BookingApiRest.core.shared.application.results;
using BookingApiRest.core.shared.exceptions;
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
        private string hotelId;
        private string employeeId;

        [SetUp]
        public void SetUp()
        {
            _bookingRepository = Substitute.For<BookingRepository>();
            _eventBus = Substitute.For<EventBus>();
            hotelId = Guid.NewGuid().ToString();
            employeeId = Guid.NewGuid().ToString();

            this._bookingService = new BookingService(_bookingRepository, _eventBus);
            var Booking = new Booking(employeeId, RoomType.Standard, DateTime.Now, DateTime.Now.AddDays(3));
            this._bookingRepository.Save(hotelId, Booking);
        }

        [Test]
        public async Task book_a_room()
        {
            var roomType = RoomType.Standard;
            var checkIn = DateTime.Now;
            var checkOut = DateTime.Now.AddDays(3);
            _eventBus.PublishAndWait<IsBookingAllowRequest, BooleanResult>(Arg.Any<IsBookingAllowRequest>())
                .Returns(new Result<BooleanResult>(new BooleanResult(true), true));
            _eventBus.PublishAndWait<GetHotelRoomsCapacityRequest, IntResult>(Arg.Any<GetHotelRoomsCapacityRequest>())
                .Returns(new Result<IntResult>(new IntResult(5), true));
            _bookingRepository.GetBookings(hotelId, roomType).Returns(new List<Booking>());

            var result = await _bookingService.BookRoom(hotelId, employeeId, roomType, checkIn, checkOut);

            _bookingRepository.Received().Save(Arg.Is<string>(id => id == hotelId),
                 Arg.Is<Booking>(booking =>
                 booking.EmployeeId == employeeId
                 && booking.RoomType == roomType
                 && booking.CheckIn == checkIn
                 && booking.CheckOut == checkOut
             ));
            result.IsSuccess.ShouldBeTrue(); 
            var booking = result.GetValue();
            booking.ShouldNotBeNull();
            booking.EmployeeId.ShouldBe(employeeId);
            booking.RoomType.ShouldBe(roomType);
            booking.CheckIn.ShouldBe(checkIn);
            booking.CheckOut.ShouldBe(checkOut);
        }

        [Test]
        public async Task not_allow_when_policy_does_not_allow_it()
        {
            var roomType = RoomType.Standard;
            var checkIn = DateTime.Now;
            var checkOut = DateTime.Now.AddDays(3);
            _eventBus.PublishAndWait<IsBookingAllowRequest, BooleanResult>(Arg.Any<IsBookingAllowRequest>())
                .Returns(new Result<BooleanResult>(new BooleanResult(false), true));

            var result = await _bookingService.BookRoom(hotelId, employeeId, roomType, checkIn, checkOut);

            result.IsSuccess.ShouldBeFalse();
            result.GetError().ShouldBeOfType<BookingIsNotAllowException>();
        }

        [Test]
        public async Task not_allow_when_is_fully_booked()
        {
            var roomType = RoomType.Standard;
            var checkIn = DateTime.Now;
            var checkOut = DateTime.Now.AddDays(3);
            _eventBus.PublishAndWait<IsBookingAllowRequest, BooleanResult>(Arg.Any<IsBookingAllowRequest>())
                .Returns(new Result<BooleanResult>(new BooleanResult(true), true));
            _eventBus.PublishAndWait<GetHotelRoomsCapacityRequest, IntResult>(Arg.Any<GetHotelRoomsCapacityRequest>())
                .Returns(new Result<IntResult>(new IntResult(1), true));
            _bookingRepository.GetBookings(hotelId, roomType).Returns(new List<Booking> { new Booking(employeeId, roomType, checkIn, checkOut) });

            var result = await _bookingService.BookRoom(hotelId, employeeId, roomType, checkIn, checkOut);

            result.IsSuccess.ShouldBeFalse();
            result.GetError().ShouldBeOfType<RoomsFullyBookedException>();
        }
    }
}
