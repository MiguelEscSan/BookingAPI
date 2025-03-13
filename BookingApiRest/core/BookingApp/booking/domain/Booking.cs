using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.booking.domain
{
    public class Booking
    {
        public string HotelId { get; set; }
        public RoomType RoomType { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public Booking(string hotelId, RoomType room, DateTime checkIn, DateTime checkOut) {
            this.HotelId = hotelId;
            this.RoomType = room;
            this.CheckIn = checkIn;
            this.CheckOut = checkOut;
        }
    }
}
