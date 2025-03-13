using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.booking.controller.DTO
{
    public class CreateBookingDTO
    {
        public string RoomType { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
    }
}
