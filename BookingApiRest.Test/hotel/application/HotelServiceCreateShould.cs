using BookingApiRest.core.BookingApp.hotel.application;
using BookingApiRest.core.shared.exceptions;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApiRest.Core.Shared.Domain;
using BookingApp.Hotel.Application.Ports;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace BookingApiRest.Test.hotel;
public class HotelServiceCreateShould {
    private HotelRepository _hotelRepository;
    private HotelService _hotelService;

    [SetUp]
    public void SetUp() {
        _hotelRepository = Substitute.For<HotelRepository>();
        _hotelService = new HotelService(_hotelRepository);
    }

    [Test]
    public async Task create_a_single_hotel() {
        var hotel = new Hotel("1", "Hotel One");
        
        _hotelService.AddHotel("1", "Hotel One");
        var validation = Arg.Is<Hotel>(hotel => hotel.Id == "1" && hotel.Name == "Hotel One");

        _hotelRepository.Received().Save(validation);
    }

    [Test]
    public void create_multiple_hotels()
    {
        var hotelOne = new Hotel("1", "Hotel One");
        var hotelTwo = new Hotel("2", "Hotel Two");

        _hotelService.AddHotel("1", "Hotel One");
        var validationOne = Arg.Is<Hotel>(hotel => hotel.Id == "1" && hotel.Name == "Hotel One");

        _hotelRepository.Received().Save(validationOne);

        _hotelService.AddHotel("2", "Hotel Two");
        var validationTwo = Arg.Is<Hotel>(hotel => hotel.Id == "2" && hotel.Name == "Hotel Two");

        
        _hotelRepository.Received().Save(validationTwo);
    }

    

    [Test]
    public void not_allow_when_hotel_id_is_already_used()
    {
        var hotel = new Hotel("1", "Hotel One");

        _hotelRepository.Exists("1").Returns(true);
        var exception = Should.Throw<HotelAlreadyExistsException>(() => _hotelService.AddHotel("1", "Hotel One"));

        exception.Message.ShouldBe("Hotel ID already exists");
        _hotelRepository.DidNotReceive().Save(Arg.Any<Hotel>());
    }
}