using BookingApiRest.core.BookingApp.hotel.application;
using BookingApiRest.core.shared.exceptions;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApp.Hotel.Application.Ports;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace BookingApiRest.Test.hotel.application;
public class HotelServiceShould {
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

        _hotelRepository.Received().Create(validation);
    }

    [Test]
    public void not_allow_when_hotel_id_is_already_used()
    {
        var hotel = new Hotel("1", "Hotel One");

        _hotelRepository.Exists("1").Returns(true);
        var exception = Should.Throw<ConflictException>(() => _hotelService.AddHotel("1", "Hotel One"));
        
        exception.Message.ShouldBe("Hotel ID already exists");
        _hotelRepository.DidNotReceive().Create(Arg.Any<Hotel>());
    }
}