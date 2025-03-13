using BookingApiRest.core.BookingApp.policy.domain;
using BookingApiRest.core.BookingApp.policy.infrastructure;
using BookingApiRest.Core.Shared.Domain;
using Shouldly;

namespace BookingApiRest.Test.policy.infrastructure;
public class InMemoryPolicyRepositorySaveShould {

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
        var result = _inMemoryPolicyRepository._policies[PolicyType.Company]["1"];
        result.RoomType.ShouldBe(RoomType.Standard);
    }

    [Test]
    public void update_an_existing_policy()
    {
        var policy = new Policy("1", RoomType.Standard);
        var newPolicy = new Policy("1", RoomType.Suite);
        _inMemoryPolicyRepository.Save(PolicyType.Company, policy);

        _inMemoryPolicyRepository.Save(PolicyType.Company, newPolicy);

        var result = _inMemoryPolicyRepository._policies[PolicyType.Company]["1"];
        result.RoomType.ShouldBe(RoomType.Suite);
    }

    [Test]
    public void save_a_policy_for_an_employee()
    {
        var policy = new Policy("1", RoomType.Standard);

        _inMemoryPolicyRepository.Save(PolicyType.Employee, policy);

        var result = _inMemoryPolicyRepository._policies[PolicyType.Employee]["1"];
        result.RoomType.ShouldBe(RoomType.Standard);
    }
}
