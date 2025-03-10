using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApiRest.Core.Shared.Domain;
using NUnit.Framework;
using Shouldly;

namespace BookingApiRest.Test.hotel.domain;

public class HotelShould {

    [Test]
    public void create_a_hotel()
    {
        var hotel = new Hotel("1", "Gloria Palace");
        hotel.Name.ShouldBe("Gloria Palace");
        hotel.Id.ShouldBe("1");
    }

    [Test]
    public void add_a_room_to_hotel()
    {
        var hotel = new Hotel("1", "Gloria Palace");

        hotel.AddRoom(1, RoomType.Standard);

        hotel.Rooms.Count.ShouldBe(1);
        hotel.Rooms[0].Number.ShouldBe(1);
        hotel.Rooms[0].RoomType.ShouldBe(RoomType.Standard);
    }

    [Test]
    public void update_a_room()
    {
        var hotel = new Hotel("1", "Gloria Palace");
        hotel.AddRoom(1, RoomType.Standard);
        hotel.Rooms[0].RoomType.ShouldBe(RoomType.Standard);

        hotel.AddRoom(1, RoomType.Suite);

        hotel.Rooms.Count.ShouldBe(1);
        hotel.Rooms[0].RoomType.ShouldBe(RoomType.Suite);
    }

}