using System.Net;
using System.Net.Http.Json;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using Shouldly;

namespace BookingApiRest.Test;

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
        var body = JsonContent.Create(new { 
            UID = uid, 
            Name = hotelName 
        });

        var response = await client.PostAsync("/api/hotels", body);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var createdHotel = await response.Content.ReadFromJsonAsync<Hotel>();
        createdHotel.Name.ShouldBe(hotelName);
        createdHotel.Id.ShouldBe(uid);
    }
}