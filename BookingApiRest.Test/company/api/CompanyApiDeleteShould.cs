using BookingApiRest.core.BookingApp.booking.application;
using BookingApiRest.core.BookingApp.company.application;
using BookingApiRest.core.BookingApp.company.domain;
using BookingApiRest.core.BookingApp.hotel.application;
using BookingApiRest.core.BookingApp.policy.domain;
using BookingApiRest.Core.Shared.Domain;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookingApiRest.Test.company;
public class CompanyApiDeleteShould
{
    private CustomWebApplicationFactory<Program> factory;
    private HttpClient client;
    private string employeeId;
    private string hotelId;
    private string companyId;

    [SetUp]
    public void SetUp()
    {
        factory = new CustomWebApplicationFactory<Program>();
        client = factory.CreateClient();
        employeeId = Guid.NewGuid().ToString();
        companyId = Guid.NewGuid().ToString();
        hotelId = Guid.NewGuid().ToString();
        var companyService = factory.Services.GetRequiredService<CompanyService>();
        companyService.AddEmployee(companyId, employeeId);

        var hotelService = factory.Services.GetRequiredService<HotelService>();
        hotelService.AddHotel(hotelId, "Hotel Test");
        hotelService.setRoom(hotelId, 2, RoomType.Standard);

        var bookingService = factory.Services.GetRequiredService<BookingService>();
        bookingService.BookRoom(employeeId, hotelId, RoomType.Standard, DateTime.Now, DateTime.Now.AddDays(3));

    }

    [TearDown]
    public void TearDown()
    {
        client.Dispose();
        factory.Dispose();
    }

    [Test]
    public async Task delete_existing_employee()
    {
        var response = await client.DeleteAsync($"/api/company/employee/{employeeId}");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        factory.EmployeeRepository._companies[companyId].Count.ShouldBe(0);
    }

    [Test]
    public async Task not_allow_deleting_an_employee_that_does_not_exist()
    {
        var NonExistingEmployeeId = Guid.NewGuid().ToString();

        var response = await client.DeleteAsync($"/api/company/employee/{NonExistingEmployeeId}");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task delete_employee_policies_if_exists()
    {
        var response = await client.DeleteAsync($"/api/company/employee/{employeeId}");

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        factory.PolicyRepository._employeesPolicies.ContainsKey(employeeId).ShouldBeFalse();
    }

    [Test]
    public async Task delete_employee_bookings()
    {
        var response = await client.DeleteAsync($"/api/company/employee/{employeeId}");
        var allBookings = factory.BookingRepository._bookings.Values
                              .SelectMany(bookings => bookings)
                              .ToList();

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        allBookings.Any(b => b.EmployeeId == employeeId).ShouldBeFalse();
    }
}

