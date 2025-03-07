using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BookingApiRest.core.BookingApp.hotel.controller.DTO;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using Shouldly;

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
        var createdHotel = await response.Content.ReadFromJsonAsync<Hotel>();
        createdHotel.Name.ShouldBe(hotelName);
        createdHotel.Id.ShouldBe(uid);
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
}