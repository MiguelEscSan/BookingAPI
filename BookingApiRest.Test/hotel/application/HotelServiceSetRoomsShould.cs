using BookingApiRest.core.BookingApp.hotel.application;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApiRest.Core.Shared.Domain;
using BookingApiRest.core.shared.exceptions;
using BookingApp.Hotel.Application.Ports;
using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApiRest.Test.hotel
{
    public class HotelServiceSetRoomsShould
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
        public void set_a_room()
        {
            var hotel = new Hotel("1", "Hotel One");
            _hotelRepository.GetById("1").Returns(hotel);

            _hotelService.setRoom("1", 1, RoomType.Standard);
            var validation = Arg.Is<Hotel>(hotel => hotel.Id == "1" 
                                                && hotel.Rooms[RoomType.Standard] == 1);

            _hotelRepository.Received().Update(validation);
        }

        [Test]
        public void not_allow_to_set_a_room_when_hotel_does_not_exist()
        {
            _hotelRepository.GetById("1").Returns((Hotel)null);
            var exception = Should.Throw<HotelHasNotBeenFound>(() => _hotelService.setRoom("1", 1, RoomType.Standard));

            exception.Message.ShouldBe("Hotel with id 1 not found");
            _hotelRepository.DidNotReceive().Update(Arg.Any<Hotel>());
        }

        [Test]
        public void update_an_existing_room()
        {
            var hotel = new Hotel("1", "Hotel One");
            hotel.SetRoom(3, RoomType.Standard);
            _hotelRepository.GetById("1").Returns(hotel);

            _hotelService.setRoom("1", 1, RoomType.Standard);

            var validation = Arg.Is<Hotel>(hotel => hotel.Id == "1" 
                                                && hotel.Rooms[RoomType.Standard] == 1);
            _hotelRepository.Received().Update(validation);
        }
    }
}
