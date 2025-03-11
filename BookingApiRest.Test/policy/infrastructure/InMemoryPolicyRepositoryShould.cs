using BookingApiRest.core.BookingApp.policy.domain;
using BookingApiRest.core.BookingApp.policy.infrastructure;
using BookingApiRest.Core.Shared.Domain;
using Shouldly;

namespace BookingApiRest.Test.policy.infrastructure;
public class InMemoryPolicyRepositoryShould {

    private InMemoryPolicyRepository _inMemoryPolicyRepository;

    [SetUp]
    public void SetUp()
    {
        _inMemoryPolicyRepository = new InMemoryPolicyRepository();
    }

    [Test]
    public void save_a_single_policy()
    {
        var policy = new Policy("1", RoomType.Standard);

        _inMemoryPolicyRepository.Save(PolicyType.Company, policy);
        var result = _inMemoryPolicyRepository._policies[PolicyType.Company][0];
        result.RoomType.ShouldBe(RoomType.Standard);
    }
}
