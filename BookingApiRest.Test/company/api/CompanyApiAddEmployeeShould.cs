using BookingApiRest.core.BookingApp.company.controller.DTO.request;
using BookingApiRest.core.shared.exceptions;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace BookingApiRest.Test.company.controller;
public class CompanyApiAddEmployeeShould
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
            companyId = companyId,
            employeeId = employeeId

        };

        var response = await client.PostAsJsonAsync("/api/company/employee", body);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var employee = factory.EmployeeRepository._companies[companyId][0];
        employee.Id.ShouldBe(employeeId);
    }

    [Test]
    public async Task create_multiple_employees()
    {
        var employeeId = Guid.NewGuid().ToString();
        var companyId = Guid.NewGuid().ToString();
        var body = new CreateEmployeeDTO
        {
            companyId = companyId,
            employeeId = employeeId
        };

        var response = await client.PostAsJsonAsync("/api/company/employee", body);

        var employee = factory.EmployeeRepository._companies[companyId][0];

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        employee.Id.ShouldBe(employeeId);

        var employeeId2 = Guid.NewGuid().ToString();
        var body2 = new CreateEmployeeDTO
        {
            companyId = companyId,
            employeeId = employeeId2
        };

        response = await client.PostAsJsonAsync("/api/company/employee", body2);
        var employee2 = factory.EmployeeRepository._companies[companyId][1];

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        employee2.Id.ShouldBe(employeeId2);
    }
    [Test]
    public async Task not_allow_creating_employe_that_already_exist()
    {
        var employeeId = Guid.NewGuid().ToString();
        var companyId = Guid.NewGuid().ToString();
        var body = new CreateEmployeeDTO
        {
            companyId = companyId,
            employeeId = employeeId

        };
        var body2 = new CreateEmployeeDTO
        {
            companyId = companyId,
            employeeId = employeeId
        };

        var response = await client.PostAsJsonAsync("/api/company/employee", body);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        response = await client.PostAsJsonAsync("/api/company/employee", body);

        response.StatusCode.ShouldBe(HttpStatusCode.Conflict);
    }
    
}


