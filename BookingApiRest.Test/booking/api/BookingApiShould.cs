using BookingApiRest.core.BookingApp.booking.application;
using BookingApiRest.core.BookingApp.booking.controller.DTO;
using BookingApiRest.core.BookingApp.company.application;
using BookingApiRest.core.BookingApp.hotel.application;
using BookingApiRest.core.BookingApp.policy.application;
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
        private string employeeId = Guid.NewGuid().ToString();
        private string hotelId = Guid.NewGuid().ToString();
        private HotelService hotelService;

        [SetUp]
        public void SetUp()
        {
            factory = new CustomWebApplicationFactory<Program>();
            client = factory.CreateClient();
            var companyService = factory.Services.GetRequiredService<CompanyService>();
            companyService.AddEmployee(companyId, employeeId);

            hotelService = factory.Services.GetRequiredService<HotelService>();
            hotelService.AddHotel(hotelId, "Gloria Palace");
            hotelService.setRoom(hotelId, 5, RoomType.Standard);

            var policyService = factory.Services.GetRequiredService<PolicyService>();
            policyService.SetEmployeePolicy(employeeId, RoomType.Standard);

            var bookingService = factory.Services.GetRequiredService<BookingService>();
            bookingService.BookRoom(hotelId, employeeId, RoomType.Standard, DateTime.Now, DateTime.Now.AddDays(3));
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
            var bookingDTO = new CreateBookingDTO {
                RoomType = roomType,
                CheckIn = checkIn,
                CheckOut = checkOut,
            };

            var response = await client.PostAsJsonAsync($"/api/booking/{hotelId}/{employeeId}", bookingDTO);
            var result = await response.Content.ReadFromJsonAsync<BookingDTO>();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            result.EmployeeId.ShouldBe(employeeId);
            result.RoomType.ShouldBe(roomType);
            result.CheckIn.ShouldBe(checkIn);
            result.CheckOut.ShouldBe(checkOut);

        }

        [Test]
        public async Task not_allow_a_booking_cause_of_the_policy()
        {
            var roomType = RoomType.Suite.ToString();
            var checkIn = DateTime.Now.ToString("yyyy-MM-dd");
            var checkOut = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
            var bookingDTO = new CreateBookingDTO {
                RoomType = roomType,
                CheckIn = checkIn,
                CheckOut = checkOut,
            };

            var response = await client.PostAsJsonAsync($"/api/booking/{hotelId}/{employeeId}", bookingDTO);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task not_allow_a_booking_cause_of_the_hotel_capacity()
        {
            var roomType = RoomType.Standard.ToString();
            var checkIn = DateTime.Now.ToString("yyyy-MM-dd");
            var checkOut = DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
            var bookingDTO = new CreateBookingDTO {
                RoomType = roomType,
                CheckIn = checkIn,
                CheckOut = checkOut,
            };
            hotelService.setRoom(hotelId, 0, RoomType.Standard);

            var response = await client.PostAsJsonAsync($"/api/booking/{hotelId}/{employeeId}", bookingDTO);

            response.StatusCode.ShouldBe(HttpStatusCode.Conflict);
        }

        [Test]
        public async Task not_allow_a_booking_due_to_hotel_capacity_and_date()
        {
            var roomType = RoomType.Standard.ToString();
            var checkIn = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            var checkOut = DateTime.Now.AddDays(4).ToString("yyyy-MM-dd");
            var bookingDTO = new CreateBookingDTO
            {
                RoomType = roomType,
                CheckIn = checkIn,
                CheckOut = checkOut,
            };
            hotelService.setRoom(hotelId, 1, RoomType.Standard);

            var response = await client.PostAsJsonAsync($"/api/booking/{hotelId}/{employeeId}", bookingDTO);

            response.StatusCode.ShouldBe(HttpStatusCode.Conflict);
        }

    }
}
