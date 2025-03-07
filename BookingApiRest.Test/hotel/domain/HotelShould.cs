using BookingApiRest.Core.BookingApp.Hotel.Domain;
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

}