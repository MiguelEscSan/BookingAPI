using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.Core.BookingApp.Hotel.Domain;
public class Hotel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public Dictionary<RoomType, int> Rooms;

    public Hotel(string id, string name)
    {
        Id = id;
        Name = name;
        Rooms = new Dictionary<RoomType, int>();
    }
        
    public void SetRoom(int number, RoomType roomType)
    {
        Rooms[roomType] = number;
    }
}
