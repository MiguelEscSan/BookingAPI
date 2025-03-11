using BookingApiRest.core.BookingApp.company.domain;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookingApiRest.Test.company.api;
public class CompanyApiDeleteShould
{
    private CustomWebApplicationFactory<Program> factory;
    private HttpClient client;
    private string employeeId;

    [SetUp]
    public void SetUp()
    {
        factory = new CustomWebApplicationFactory<Program>();
        client = factory.CreateClient();
        employeeId = Guid.NewGuid().ToString();
        factory.EmployeeRepository.Save(new Employee(Guid.NewGuid().ToString(), employeeId));

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
        factory.EmployeeRepository._employees.Count.ShouldBe(0);
    }

    [Test]
    public async Task not_allow_deleting_an_employee_that_does_not_exist()
    {
        var NonExistingEmployeeId = Guid.NewGuid().ToString();

        var response = await client.DeleteAsync($"/api/company/employee/{NonExistingEmployeeId}");

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

}

