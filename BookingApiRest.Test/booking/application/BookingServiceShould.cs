using BookingApiRest.core.BookingApp.booking.application;
using BookingApiRest.core.BookingApp.booking.application.ports;
using BookingApiRest.core.BookingApp.booking.domain;
using BookingApiRest.Core.Shared.Domain;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApiRest.Test.booking
{
    public class BookingServiceShould
    {
        private BookingService _bookingService;
        private BookingRepository _bookingRepository;

        [SetUp]
        public void SetUp()
        {
            _bookingRepository = Substitute.For<BookingRepository>();
            
            this._bookingService = new BookingService(_bookingRepository);
        }

        [Test]
        public void book_a_room()
        {
            var employeeId = Guid.NewGuid().ToString();
            var hotelId = Guid.NewGuid().ToString();
            var roomType = RoomType.Standard;
            var checkIn = DateTime.Now;
            var checkOut = DateTime.Now.AddDays(3);


            _bookingService.BookRoom(employeeId, hotelId, roomType, checkIn, checkOut);

            _bookingRepository.Received().Save(Arg.Is<string>(id => id == employeeId), 
                Arg.Is<Booking>(b => 
                b.EmployeeId == hotelId 
                && b.RoomType == roomType 
                && b.CheckIn == checkIn 
                && b.CheckOut == checkOut
            ));
        }
    }
}
