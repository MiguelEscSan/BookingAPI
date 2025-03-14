using BookingApiRest.core.BookingApp.policy.domain;
using BookingApiRest.core.BookingApp.policy.infrastructure;
using BookingApiRest.Core.Shared.Domain;
using Shouldly;

namespace BookingApiRest.Test.policy;
public class InMemoryPolicyRepositorySaveShould {

    private InMemoryPolicyRepository _inMemoryPolicyRepository;
    private Policy policy;
    [SetUp]
    public void SetUp()
    {
        _inMemoryPolicyRepository = new InMemoryPolicyRepository();

        policy = new Policy("1", RoomType.Standard);
    }

    [Test]
    public void save_a_policy_for_an_employee()
    {
        var policy = new Policy("1", RoomType.Standard);

        _inMemoryPolicyRepository.Save(PolicyType.Employee, policy);

        var result = _inMemoryPolicyRepository._employeesPolicies["1"];
        result.RoomType.ShouldBe(RoomType.Standard);
    }

    [Test]
    public void update_an_existing_policy()
    {
        var newPolicy = new Policy("1", RoomType.Suite);
        _inMemoryPolicyRepository.Save(PolicyType.Employee, policy);

        _inMemoryPolicyRepository.Save(PolicyType.Employee, newPolicy);

        var result = _inMemoryPolicyRepository._employeesPolicies["1"];
        result.RoomType.ShouldBe(RoomType.Suite);
    }

  
    [Test]
    public void save_a_policy_for_a_company()
    {
        _inMemoryPolicyRepository.Save(PolicyType.Company, policy);

        var result = _inMemoryPolicyRepository._companiesPolices["1"];
        result.RoomType.ShouldBe(RoomType.Standard);
    }

    [Test]
    public void update_an_existing_company_policy()
    {
        var newPolicy = new Policy("1", RoomType.Suite);
        _inMemoryPolicyRepository.Save(PolicyType.Company, policy);

        _inMemoryPolicyRepository.Save(PolicyType.Company, newPolicy);

        var result = _inMemoryPolicyRepository._companiesPolices["1"];
        result.RoomType.ShouldBe(RoomType.Suite);
    }
}
