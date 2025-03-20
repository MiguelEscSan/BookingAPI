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
            RoomType roomType = Enum.Parse<RoomType>(bookingDTO.RoomType);
            DateTime CheckIn = DateTime.Parse(bookingDTO.CheckIn);
            DateTime CheckOut = DateTime.Parse(bookingDTO.CheckOut);

            Result<Booking> result = await _bookingService.BookRoom(hotelId, employeeId, roomType, CheckIn, CheckOut);

            if (!result.IsSuccess)
            {
                if (result.Exception is RoomsFullyBookedException)
                {
                    return Conflict();
                }

                return BadRequest();
            }

            BookingDTO bookingResponse = new BookingDTO
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
