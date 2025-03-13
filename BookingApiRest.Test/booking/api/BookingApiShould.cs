using BookingApiRest.core.BookingApp.booking.controller.DTO;
using BookingApiRest.core.BookingApp.company.application;
using BookingApiRest.core.BookingApp.hotel.application;
using BookingApiRest.Core.Shared.Domain;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace BookingApiRest.Test.booking
{
    public class BookingApiShould
    {
        private CustomWebApplicationFactory<Program> factory;
        private HttpClient client;

        private string companyId = Guid.NewGuid().ToString();
        private string createdEmployeeId = Guid.NewGuid().ToString();
        private string hotelId = Guid.NewGuid().ToString();

        [SetUp]
        public void SetUp()
        {
            factory = new CustomWebApplicationFactory<Program>();
            client = factory.CreateClient();
            var companyService = factory.Services.GetRequiredService<CompanyService>();
            companyService.AddEmployee(companyId, createdEmployeeId);

            var hotelService = factory.Services.GetRequiredService<HotelService>();
            hotelService.AddHotel(hotelId, createdEmployeeId);
            hotelService.setRoom(hotelId, 5, RoomType.Standard);
        }

        [TearDown]
        public void TearDown()
        {
            client.Dispose();
            factory.Dispose();
        }

        [Test]
        public async Task allow_book_a_room()
        {
            var roomType = RoomType.Standard.ToString();
            var checkIn = DateTime.Now.ToString("yyyy-MM-dd");
            var checkOut = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
            var bookingDTO = new BookingDTO {
                RoomType = roomType,
                CheckIn = checkIn,
                CheckOut = checkOut,
            };

            var response = await client.PostAsJsonAsync($"/api/booking/{hotelId}/{createdEmployeeId}", bookingDTO);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

        }
    }
}
