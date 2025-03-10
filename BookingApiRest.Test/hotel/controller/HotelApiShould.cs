using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BookingApiRest.core.BookingApp.hotel.controller.DTO;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApiRest.Infrastructure.Repositories;
using BookingApp.Hotel.Application.Ports;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using Shouldly;
using BookingApiRest.Core.Shared.Domain;
using BookingApiRest.core.BookingApp.hotel.controller.DTO.response;
using BookingApiRest.core.BookingApp.hotel.controller.DTO.request;


namespace BookingApiRest.Test.hotel.controller;

public class HotelApiShould
{
    private WebApplicationFactory<Program> factory;
    private HttpClient client;

    [SetUp]
    public void SetUp()
    {
        factory = new WebApplicationFactory<Program>();
        client = factory.CreateClient();

    }

    [TearDown]
    public void TearDown()
    {
        client.Dispose();
        factory.Dispose();
    }

    [Test]
    public async Task create_a_hotel()
    {
        var hotelName = "Gloria Palace";
        var uid = Guid.NewGuid().ToString();
        var body = new CreateHotelDTO
        {
            Id = uid,
            Name = hotelName
        };

        var response = await client.PostAsJsonAsync("/api/hotel", body);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        //var createdHotel = await response.Content.ReadFromJsonAsync<Hotel>();
        //createdHotel.Name.ShouldBe(hotelName);
        //createdHotel.Id.ShouldBe(uid);
    }

    [Test]
    public async Task not_allow_when_hotel_id_is_already_used() {
        var uid = Guid.NewGuid().ToString();
        var body = new CreateHotelDTO
        {
            Id = uid,
            Name = "Gloria Palace"
        };
        var body2 = new CreateHotelDTO
        {
            Id = uid,
            Name = "Gloria Palace"
        };

        var response = await client.PostAsJsonAsync("/api/hotel", body);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);


        response = await client.PostAsJsonAsync("/api/hotel", body);
        response.StatusCode.ShouldBe(HttpStatusCode.Conflict);
    }

    [Test]
    public async Task return_a_hotel() {
        var uid = Guid.NewGuid().ToString();
        var body = new CreateHotelDTO
        {
            Id = uid,
            Name = "Gloria Palace"
        };

        var response = await client.PostAsJsonAsync("/api/hotel", body);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        response = await client.GetAsync($"/api/hotel/{uid}");
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var hotelDto = await response.Content.ReadFromJsonAsync<HotelDTO>();

        hotelDto.Id.ShouldBe(uid);
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

    [Test]
    public async Task set_rooms()
    {
        var uid = Guid.NewGuid().ToString();
        var body = new CreateHotelDTO
        {
            Id = uid,
            Name = "Gloria Palace"
        };
        var response = await client.PostAsJsonAsync("/api/hotel", body);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        var SetHotelsRoomsBody = new SetHotelRoomsDTO
        {
            RoomNumber = 1,
            RoomType = RoomType.Standard.ToString()
        };

        var response2 = await client.PutAsJsonAsync($"/api/hotel/{uid}/rooms", SetHotelsRoomsBody);
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