using BookingApiRest.core.BookingApp.booking.application;
using BookingApiRest.core.BookingApp.booking.controller.DTO;
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
        //private readonly HotelService _hotelService;
        private readonly BookingService _bookingService;
        //private readonly PolicyService _policyService;

        public BookingController(BookingService bookingService)
        {
            //this._hotelService = hotelService;
            this._bookingService = bookingService;
            //this._policyService = policyService;
        }

        [HttpPost("{hotelId}/{employeeId}")]
        public async Task<IActionResult> BookRoom(string hotelId, string employeeId, [FromBody] CreateBookingDTO bookingDTO)
        {
            var roomType = Enum.Parse<RoomType>(bookingDTO.RoomType);
            var CheckIn = DateTime.Parse(bookingDTO.CheckIn);
            var CheckOut = DateTime.Parse(bookingDTO.CheckOut);

            //if (_policyService.IsBookingAllowed(employeeId, roomType) is false)
            //{
            //    return BadRequest();
            //}

            //var HotelRoomsCapacity = _hotelService.GetHotelRoomCapacity(hotelId, roomType);
            //var BookingsAtTheSameTime = _bookingService.GetBookingsAtTheSameTime(hotelId, roomType, CheckIn, CheckOut);

            //if (HasEnoughRoomsForBooking(HotelRoomsCapacity, BookingsAtTheSameTime) is false)
            //{
            //    return Conflict();
            //}


            var result = await _bookingService.BookRoom(hotelId, employeeId, roomType, CheckIn, CheckOut);

            if (!result.IsSuccess)
            {
                if (result.Exception is RoomsFullyBookedException)
                {
                    return Conflict(new { message = "No hay habitaciones disponibles para la fecha seleccionada." });
                }

                return BadRequest(new { message = "No se puede realizar la reserva debido a la política o algún error." });
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

        //private bool HasEnoughRoomsForBooking(int HotelRoomsCapacity, int BookingsAtTheSameTime)
        //{
        //    return HotelRoomsCapacity > BookingsAtTheSameTime;
        //}

    }
}
