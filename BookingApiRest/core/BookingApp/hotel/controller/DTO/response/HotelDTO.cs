using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.hotel.controller.DTO.response
{
    public class HotelDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, int> Rooms { get; set; }

        public HotelDTO(string id, string name, Dictionary<RoomType, int> rooms)
        {
            Id = id;
            Name = name;
            Rooms = rooms.ToDictionary(kvp => kvp.Key.ToString(), kvp => kvp.Value);
        }
    }

}
