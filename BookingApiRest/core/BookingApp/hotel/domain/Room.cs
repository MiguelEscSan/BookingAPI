using BookingApiRest.Core.Shared.Domain;

namespace BookingApp.Hotel.Domain;
public class Room {

    public string Id { get; set; }
    public int Number { get; set; }
    public RoomType RoomType { get; set; }

    public Room(int number, RoomType roomType) {
        Number = number;
        RoomType = roomType;
    }
}
