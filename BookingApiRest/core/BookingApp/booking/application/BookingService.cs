using BookingApiRest.core.BookingApp.booking.application.ports;
using BookingApiRest.core.BookingApp.booking.application.requests;
using BookingApiRest.core.BookingApp.booking.domain;
using BookingApiRest.core.BookingApp.company.domain;
using BookingApiRest.core.BookingApp.policy.application;
using BookingApiRest.core.BookingApp.policy.application.requests;
using BookingApiRest.core.shared.application;
using BookingApiRest.core.shared.exceptions;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApiRest.Core.Shared.Domain;
namespace BookingApiRest.core.BookingApp.booking.application
{
    public class BookingService
    {

        private readonly BookingRepository _bookingRepository;
        private readonly EventBus _eventBus;

        public BookingService(BookingRepository bookingRepository, EventBus eventBus)
        {
            _bookingRepository = bookingRepository;
            _eventBus = eventBus;
        }

        public async Task<Result<Booking>> BookRoom(string hotelId, string employeeId, RoomType roomType, DateTime checkIn, DateTime checkOut)
        {
            Result<BooleanResult> isBookingAllowed = await _eventBus.PublishAndWait<IsBookingAllowRequest, BooleanResult>(
                new IsBookingAllowRequest(employeeId, roomType.ToString())
            );
            if (isBookingAllowed.GetValue().IsSuccess is false)
            {
                return Result<Booking>.Fail(new BookingIsNotAllowException());
            }

            Result<IntResult> HotelRooms = await _eventBus.PublishAndWait<GetHotelRoomsCapacityRequest, IntResult>(
                new GetHotelRoomsCapacityRequest(hotelId, roomType.ToString())
            );

            var BookingsAtTheSameTime = GetBookingsAtTheSameTime(hotelId, roomType, checkIn, checkOut);

            if (HasEnoughRoomsForBooking(HotelRooms.GetValue().Capacity, BookingsAtTheSameTime) is false)
            {
                return Result<Booking>.Fail(new RoomsFullyBookedException());
            }

            var Booking = new Booking(employeeId, roomType, checkIn, checkOut);
            _bookingRepository.Save(hotelId, Booking);
            return Result<Booking>.Success(Booking);
        }

        public int GetBookingsAtTheSameTime(string hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut)
        {
            var bookings = _bookingRepository.GetBookings(hotelId, roomType);
            return bookings.Count(booking => booking.CheckIn < checkOut && booking.CheckOut > checkIn);
        }

        public bool HasEnoughRoomsForBooking(int HotelRoomsCapacity, int BookingsAtTheSameTime)
        {
            return HotelRoomsCapacity > BookingsAtTheSameTime;
        }

    }
}
