using BookingApiRest.core.BookingApp.hotel.application;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApp.Hotel.Application.Ports;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace BookingApiRest.Test.hotel.application;
    public class HotelServiceShould
    {
        private HotelRepository _hotelRepository;
        private HotelService _hotelService;

        [SetUp]
        public void SetUp()
        {
            _hotelRepository = Substitute.For<HotelRepository>();
            _hotelService = new HotelService(_hotelRepository);
        }

        [Test]
        public async Task create_a_single_hotel() {
            var hotel = new Hotel("1", "Hotel One");
            _hotelRepository.Received().Create(hotel);

            _hotelService.AddHotel("1", "Hotel One");
            _hotelRepository.Returns(hotel);

            result.ShouldBe(hotel);
        }
    }