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
using Microsoft.Extensions.DependencyInjection;



namespace BookingApiRest.Test.hotel;

public class HotelApiCreateShould
{
    private CustomWebApplicationFactory<Program> factory;
    private HttpClient client;

    [SetUp]
    public void SetUp()
    {
        factory = new CustomWebApplicationFactory<Program>();
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
        var hotel = factory.HotelRepository._hotels[0];
        hotel.Id.ShouldBe(uid);
        hotel.Name.ShouldBe(hotelName);
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