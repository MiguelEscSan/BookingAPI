namespace BookingApiRest.core.BookingApp.hotel.controller.DTO.response
{
    public class HotelDTO {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<RoomDTO> Rooms { get; set; }
    }
}
