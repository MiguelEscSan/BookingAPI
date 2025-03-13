using BookingApiRest.core.BookingApp.booking.application;
using BookingApiRest.core.BookingApp.booking.controller.DTO;
using BookingApiRest.core.BookingApp.company.application;
using BookingApiRest.core.BookingApp.hotel.application;
using BookingApiRest.core.BookingApp.policy.application;
using BookingApiRest.Core.Shared.Domain;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookingApiRest.core.BookingApp.booking.controller
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly HotelService _hotelService;
        private readonly BookingService _bookingService;
        private readonly PolicyService _policyService;

        public BookingController(HotelService hotelService, BookingService bookingService, PolicyService policyService)
        {
            this._hotelService = hotelService;
            this._bookingService = bookingService;
            this._policyService = policyService;
        }

        [HttpPost("{hotelId}/{employeeId}")]
        public IActionResult BookRoom(string hotelId, string employeeId, [FromBody] BookingDTO bookingDTO)
        {
            var roomType = Enum.Parse<RoomType>(bookingDTO.RoomType);
            var CheckIn = DateTime.Parse(bookingDTO.CheckIn);
            var CheckOut = DateTime.Parse(bookingDTO.CheckOut);

            if (_policyService.IsBookingAllowed(employeeId, roomType) is false)
            {
                return BadRequest("Booking not allowed");
            }
            _bookingService.BookRoom(hotelId, employeeId, roomType, CheckIn, CheckOut);
            return Ok();
        }
    }
}
