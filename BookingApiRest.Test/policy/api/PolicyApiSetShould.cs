namespace BookingApiRest.Test.policy.api;
public class PolicyApiSetShould
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
    public async Task create_a_policy_for_a_company()
    {
        
    }

}

