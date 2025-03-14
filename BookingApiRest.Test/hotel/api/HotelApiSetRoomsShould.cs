using BookingApiRest.core.BookingApp.hotel.controller.DTO.request;
using BookingApiRest.Core.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Microsoft.Extensions.DependencyInjection;
using BookingApp.Hotel.Application.Ports;
using BookingApiRest.Core.BookingApp.Hotel.Domain;

namespace BookingApiRest.Test.hotel
{
    public class HotelApiSetRoomsShould
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
        public async Task set_rooms()
        {
             var SetHotelsRoomsBody = new SetHotelRoomsDTO
            {
                RoomNumber = 1,
                RoomType = RoomType.Standard.ToString()
            };

            var response2 = await client.PutAsJsonAsync($"/api/hotel/{hotelId}/rooms", SetHotelsRoomsBody);

            response2.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Test]
        public async Task not_allow_to_set_rooms_to_non_existing_hotel()
        {
            var uid = Guid.NewGuid().ToString();
            var SetHotelsRoomsBody = new SetHotelRoomsDTO
            {
                RoomNumber = 1,
                RoomType = RoomType.Standard.ToString()
            };

            var response = await client.PutAsJsonAsync($"/api/hotel/{uid}/rooms", SetHotelsRoomsBody);

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }

    }
}
