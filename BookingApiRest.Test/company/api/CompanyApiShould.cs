using BookingApiRest.core.BookingApp.company.controller.DTO.request;
using BookingApiRest.core.shared.exceptions;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace BookingApiRest.Test.company.controller;
public class CompanyApiShould
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
    public async Task create_an_employee()
    {
        var employeeId = Guid.NewGuid().ToString();
        var companyId = Guid.NewGuid().ToString();
        var body = new CreateEmployeeDTO
        {
            employeeId = employeeId,
            companyId = companyId
        };

        var response = await client.PostAsJsonAsync("/api/company/employee", body);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var employee = factory.EmployeeRepository._employees[0];
        employee.Id.ShouldBe(employeeId);
        employee.CompanyId.ShouldBe(companyId);

    }
}


