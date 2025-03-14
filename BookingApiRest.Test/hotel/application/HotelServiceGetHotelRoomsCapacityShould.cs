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
    public class HotelServiceGetHotelRoomsCapacityShould
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
        public void return_hotel_rooms_capacity()
        {
            var hotel = new Hotel("1", "Hotel One");
            hotel.SetRoom(5, RoomType.Standard);
            _hotelRepository.GetById("1").Returns(hotel);

            var result = _hotelService.GetHotelRoomCapacity("1", RoomType.Standard);

            result.ShouldBe(5);
        }

        [Test]
        public void throw_not_found_exception_when_hotel_does_not_exist()
        {
            _hotelRepository.GetById("1").Returns((Hotel)null);

            var exception = Should.Throw<HotelHasNotBeenFound>(() => _hotelService.GetHotelRoomCapacity("1", RoomType.Standard));
        }
    }
}
