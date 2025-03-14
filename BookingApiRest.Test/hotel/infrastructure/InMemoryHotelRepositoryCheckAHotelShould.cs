using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApiRest.Infrastructure.Repositories;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApiRest.Test.hotel
{
    public class InMemoryHotelRepositoryCheckAHotelShould
    {
        private InMemoryHotelRepository _inMemoryHotelRepository;
        private string hotelId;

        [SetUp]
        public void SetUp()
        {
            _inMemoryHotelRepository = new InMemoryHotelRepository();
            hotelId = Guid.NewGuid().ToString();
            var hotel = new Hotel(hotelId, "Gloria Palace");
            _inMemoryHotelRepository.Save(hotel);
        }

        [Test]
        public void return_a_hotel()
        {
            var result = _inMemoryHotelRepository.GetById(hotelId);

            result.Id.ShouldBe(hotelId);
            result.Name.ShouldBe("Gloria Palace");
        }

        [Test]
        public void return_null_when_hotel_does_not_exist()
        {
            var result = _inMemoryHotelRepository.GetById("1");

            result.ShouldBeNull();
        }

        [Test]
        public void confirm_existance_when_hotel_is_saved()
        {
            var result = _inMemoryHotelRepository.Exists(hotelId);
            
            result.ShouldBeTrue();
        }

        [Test]
        public void confirm_non_existance_when_hotel_is_not_saved()
        {
            var result = _inMemoryHotelRepository.Exists("2");

            result.ShouldBeFalse();
        }
    }
}
