using BookingApiRest.core.BookingApp.policy.controller.DTO.request;
using BookingApiRest.Core.Shared.Domain;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace BookingApiRest.Test.policy.api;
public class PolicyApiSetShould
{
    private CustomWebApplicationFactory<Program> factory;
    private HttpClient client;

    private string companyId;

    [SetUp]
    public void SetUp()
    {
        companyId = Guid.NewGuid().ToString();
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
    public async Task create_a_policy_for_a_company()
    {
        var RoomTypePolicy = RoomType.Standard.ToString();
        var CreateRoomTypePolicyBody = new CreatePolicyDTO
        {
            RoomType = RoomTypePolicy,
        };

        var response = await client.PutAsJsonAsync($"/api/policy/company/{companyId}", CreateRoomTypePolicyBody);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var policy = factory.PolicyRepository._policies[0];
        policy.RoomType.ToString().ShouldBe(RoomTypePolicy);

    }

}

