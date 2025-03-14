
using BookingApiRest.core.shared.exceptions;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApiRest.Core.Shared.Domain;
using BookingApiRest.Infrastructure.Repositories;
using Shouldly;

namespace BookingApiRest.Test.hotel;

public class InMemoryHotelRepositoryCreateShould
{
    private InMemoryHotelRepository _inMemoryHotelRepository;
    [SetUp]
    public void SetUp()
    {
        _inMemoryHotelRepository = new InMemoryHotelRepository();
    }

    [Test]
    public void create_a_single_hotel() {
        var hotel = new Hotel("1", "Gloria Palace");

        _inMemoryHotelRepository.Save(hotel);
        var result = _inMemoryHotelRepository._hotels[0];
        result.Id.ShouldBe("1");
        result.Name.ShouldBe("Gloria Palace");
    }

    [Test]
    public void create_multiple_hotels()
    {
        var hotelOne = new Hotel("1", "Gloria Palace");
        var hotelTwo = new Hotel("2", "Gloria Palace2");

        _inMemoryHotelRepository.Save(hotelOne);
        _inMemoryHotelRepository.Save(hotelTwo);

        _inMemoryHotelRepository._hotels.Count.ShouldBe(2);
        _inMemoryHotelRepository._hotels[0].Id.ShouldBe("1");
        _inMemoryHotelRepository._hotels[0].Name.ShouldBe("Gloria Palace");
        _inMemoryHotelRepository._hotels[1].Id.ShouldBe("2");
        _inMemoryHotelRepository._hotels[1].Name.ShouldBe("Gloria Palace2");
    }

    

    [Test]
    public void update_a_hotel()
    {
        var hotel = new Hotel("1", "Gloria Palace");
        _inMemoryHotelRepository.Save(hotel);
        hotel = _inMemoryHotelRepository.GetById("1");

        hotel.SetRoom(1, RoomType.Standard);
        _inMemoryHotelRepository.Update(hotel);
        var result = _inMemoryHotelRepository._hotels[0];

        result.Rooms.Count.ShouldBe(1);
    }
}