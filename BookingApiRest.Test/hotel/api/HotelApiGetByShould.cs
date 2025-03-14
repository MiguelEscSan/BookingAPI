using BookingApiRest.core.BookingApp.hotel.controller.DTO.request;
using BookingApiRest.core.BookingApp.hotel.controller.DTO.response;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApp.Hotel.Application.Ports;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookingApiRest.Test.hotel
{
    public class HotelApiGetByShould
    {
        private CustomWebApplicationFactory<Program> factory;
        private HttpClient client;
        private string hotelId;

        [SetUp]
        public void SetUp()
        {
            factory = new CustomWebApplicationFactory<Program>();
            client = factory.CreateClient();

            hotelId = Guid.NewGuid().ToString();
            var hotel = new Hotel(hotelId, "Gloria Palace");
            var hotelRepository = factory.Services.GetRequiredService<HotelRepository>();
            hotelRepository.Save(hotel);
        }

        [TearDown]
        public void TearDown()
        {
            client.Dispose();
            factory.Dispose();
        }

        [Test]
        public async Task return_a_hotel()
        {
            var response = await client.GetAsync($"/api/hotel/{hotelId}");
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            var hotelDto = await response.Content.ReadFromJsonAsync<HotelDTO>();

            hotelDto.Id.ShouldBe(hotelId);
            hotelDto.Name.ShouldBe("Gloria Palace");
            hotelDto.Rooms.ShouldBeEmpty();
        }

        [Test]
        public async Task return_not_found_when_hotel_does_not_exist()
        {
            var uid = Guid.NewGuid().ToString();

            var response = await client.GetAsync($"/api/hotel/{uid}");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
