using BookingApiRest.Core.Shared.Domain;
using BookingApp.Hotel.Domain;

namespace BookingApiRest.Core.BookingApp.Hotel.Domain
{
    public class Hotel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Room> Rooms { get; set; }

        public Hotel(string id, string name)
        {
            Id = id;
            Name = name;
            Rooms = new List<Room>();
        }
        
        public void AddRoom(int number, RoomType roomType)
        {
            var room = new Room(number, roomType);
            Rooms.Add(room);
        }
    }
}