using BookingApiRest.core.BookingApp.booking.application;
using BookingApiRest.core.BookingApp.booking.controller.DTO;
using BookingApiRest.core.BookingApp.booking.domain;
using BookingApiRest.core.BookingApp.company.application;
using BookingApiRest.core.BookingApp.hotel.application;
using BookingApiRest.core.BookingApp.policy.application;
using BookingApiRest.core.BookingApp.policy.application.requests;
using BookingApiRest.core.shared.application;
using BookingApiRest.core.shared.exceptions;
using BookingApiRest.Core.Shared.Domain;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookingApiRest.core.BookingApp.booking.controller
{
    [ApiController]
    [Route("api/booking")]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService)
        {
            this._bookingService = bookingService;
        }

        [HttpPost("{hotelId}/{employeeId}")]
        public async Task<IActionResult> BookRoom(string hotelId, string employeeId, [FromBody] CreateBookingDTO bookingDTO)
        {
            var roomType = Enum.Parse<RoomType>(bookingDTO.RoomType);
            var CheckIn = DateTime.Parse(bookingDTO.CheckIn);
            var CheckOut = DateTime.Parse(bookingDTO.CheckOut);

            Result<Booking> result = await _bookingService.BookRoom(hotelId, employeeId, roomType, CheckIn, CheckOut);

            if (!result.IsSuccess)
            {
                if (result.Exception is RoomsFullyBookedException)
                {
                    return Conflict();
                }

                return BadRequest();
            }

            var bookingResponse = new BookingDTO
            {
                EmployeeId = result.GetValue().EmployeeId,
                RoomType = result.GetValue().RoomType.ToString(),
                CheckIn = result.GetValue().CheckIn.ToString("yyyy-MM-dd"),
                CheckOut = result.GetValue().CheckOut.ToString("yyyy-MM-dd")
            };

            return Ok(bookingResponse);
        }
    }
}
