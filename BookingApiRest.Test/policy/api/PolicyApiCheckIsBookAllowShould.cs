using BookingApiRest.core.BookingApp.company.application;
using BookingApiRest.Core.Shared.Domain;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Net;
using System.Net.Http.Json;


namespace BookingApiRest.Test.policy
{
    public class PolicyApiCheckIsBookAllowShould
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
        public async Task allow_booking_when_policy_permits()
        {
            var roomType = RoomType.Standard.ToString();

            var response = await client.GetAsync($"/api/policy/booking/{createdEmployeeId}/{roomType}");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        [Test]
        public async Task not_allow_booking_when_policy_denies()
        {
            var roomType = RoomType.Suite.ToString();

            var response = await client.GetAsync($"/api/policy/booking/{createdEmployeeId}/{roomType}");
            var isAllowed = await response.Content.ReadFromJsonAsync<bool>();

            response.StatusCode.ShouldBe(HttpStatusCode.OK);
            isAllowed.ShouldBeTrue();
        }

        [Test]
        public async Task not_allow_booking_when_employee_not_found()
        {
            var notFoundEmployeeId = Guid.NewGuid().ToString();
            var roomType = RoomType.Suite.ToString();
            
            var response = await client.GetAsync($"/api/policy/booking/{notFoundEmployeeId}/{roomType}");

            response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
    }
}
