using BookingApiRest.Core.Shared.Domain;

using System.Text.Json.Serialization;

namespace BookingApiRest.core.BookingApp.hotel.controller.DTO.response
{
    public class HotelDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, int> Rooms { get; set; }

        [JsonConstructor] 
        public HotelDTO(string id, string name, Dictionary<string, int> rooms)
        {
            Id = id;
            Name = name;
            Rooms = rooms ?? new Dictionary<string, int>();
        }
        public HotelDTO(string id, string name, Dictionary<RoomType, int> rooms)
        {
            Id = id;
            Name = name;
            Rooms = rooms.ToDictionary(kvp => kvp.Key.ToString(), kvp => kvp.Value);
        }
    }

}
