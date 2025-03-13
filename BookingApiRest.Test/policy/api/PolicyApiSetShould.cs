using BookingApiRest.core.BookingApp.company.application;
using BookingApiRest.core.BookingApp.company.domain;
using BookingApiRest.core.BookingApp.policy.application;
using BookingApiRest.core.BookingApp.policy.controller.DTO.request;
using BookingApiRest.core.BookingApp.policy.domain;
using BookingApiRest.Core.Shared.Domain;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace BookingApiRest.Test.policy.api;
public class PolicyApiSetShould
{
    private CustomWebApplicationFactory<Program> factory;
    private HttpClient client;

    private string companyId = Guid.NewGuid().ToString();
    private string createdEmployeeId = Guid.NewGuid().ToString();

    [SetUp]
    public void SetUp()
    {
        factory = new CustomWebApplicationFactory<Program>();
        client = factory.CreateClient();
        var companyService = factory.Services.GetRequiredService<CompanyService>();
        companyService.AddEmployee(companyId, createdEmployeeId);
    }

    [TearDown]
    public void TearDown()
    {
        client.Dispose();
        factory.Dispose();
    }

    [Test]
    public async Task create_a_policy_for_a_company()
    {
        var RoomTypePolicy = RoomType.Standard.ToString();
        var CreateRoomTypePolicyBody = new CreatePolicyDTO
        {
            RoomType = RoomTypePolicy,
        };

        var response = await client.PutAsJsonAsync($"/api/policy/company/{companyId}", CreateRoomTypePolicyBody);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var policy = factory.PolicyRepository._companiesPolices[companyId];
        policy.RoomType.ToString().ShouldBe(RoomTypePolicy);
    }

    [Test]
    public async Task update_an_existing_company_policy()
    {
        var RoomTypePolicy = RoomType.Standard.ToString();
        var CreateRoomTypePolicyBody = new CreatePolicyDTO
        {
            RoomType = RoomTypePolicy,
        };
        await client.PutAsJsonAsync($"/api/policy/company/{companyId}", CreateRoomTypePolicyBody);
        var newRoomTypePolicy = RoomType.Suite.ToString();
        var UpdateRoomTypePolicyBody = new CreatePolicyDTO
        {
            RoomType = newRoomTypePolicy,
        };

        var response = await client.PutAsJsonAsync($"/api/policy/company/{companyId}", UpdateRoomTypePolicyBody);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var policy = factory.PolicyRepository._companiesPolices[companyId];
        policy.RoomType.ToString().ShouldBe(newRoomTypePolicy);
    }

    [Test]
    public async Task create_a_policy_for_a_employee()
    {
        var RoomTypePolicy = RoomType.Standard.ToString();
        var CreateRoomTypePolicyBody = new CreatePolicyDTO
        {
            RoomType = RoomTypePolicy,
        };

        var response = await client.PutAsJsonAsync($"/api/policy/employee/{createdEmployeeId}", CreateRoomTypePolicyBody);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var policy = factory.PolicyRepository._employeesPolicies[createdEmployeeId];
        policy.RoomType.ToString().ShouldBe(RoomTypePolicy);
    }

    [Test]
    public async Task not_allow_save_a_policy_for_a_non_existing_employee()
    {
        var randomId = Guid.NewGuid().ToString();
        var RoomTypePolicy = RoomType.Standard.ToString();
        var CreateRoomTypePolicyBody = new CreatePolicyDTO
        {
            RoomType = RoomTypePolicy,
        };

        var response = await client.PutAsJsonAsync($"/api/policy/employee/{randomId}", CreateRoomTypePolicyBody);

        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }


}

