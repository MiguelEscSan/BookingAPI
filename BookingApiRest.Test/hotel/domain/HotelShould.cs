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

        hotel.SetRoom(1, RoomType.Standard);

        hotel.Rooms[RoomType.Standard].ShouldBe(1);
    }

    [Test]
    public void update_a_room()
    {
        var hotel = new Hotel("1", "Gloria Palace");
        hotel.SetRoom(1, RoomType.Standard);
        hotel.Rooms[RoomType.Standard].ShouldBe(1);

        hotel.SetRoom(3, RoomType.Standard);
        hotel.Rooms[RoomType.Standard].ShouldBe(3);
    }

}