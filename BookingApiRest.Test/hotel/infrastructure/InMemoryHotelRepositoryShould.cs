
using BookingApiRest.core.shared.exceptions;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApiRest.Infrastructure.Repositories;
using Shouldly;

namespace BookingApiRest.Test.hotel.infrastructure;

public class InMemoryHotelRepositoryShould
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

        _inMemoryHotelRepository.Create(hotel);
        var result = _inMemoryHotelRepository._hotels[0];
        result.Id.ShouldBe("1");
        result.Name.ShouldBe("Gloria Palace");
    }

    [Test]
    public void not_allow_when_hotel_id_is_already_used() {
        var hotel = new Hotel("1", "Gloria Palace");

        _inMemoryHotelRepository.Create(hotel);

        _inMemoryHotelRepository.Exists("1").ShouldBeTrue();
    }

}