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

    [Test]
    public void check_if_an_employee_policy_exists()
    {
        var policy = new Policy("1", RoomType.Standard);
        _inMemoryPolicyRepository.Save(PolicyType.Employee, policy);

        var result = _inMemoryPolicyRepository.EmployeePolicyExists("1");
        
        result.ShouldBeTrue();
    }

    [Test]
    public void check_if_an_employee_policy_does_not_exist()
    {
        var result = _inMemoryPolicyRepository.EmployeePolicyExists("1");

        result.ShouldBeFalse();
    }

    [Test]
    public void check_if_employee_policy_is_correct()
    {
        var policy = new Policy("1", RoomType.Standard);
        _inMemoryPolicyRepository.Save(PolicyType.Employee, policy);

        var result = _inMemoryPolicyRepository.CheckEmployeePolicy("1", RoomType.Standard);

        result.ShouldBeTrue();
    }
}
