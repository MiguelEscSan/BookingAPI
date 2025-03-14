using BookingApiRest.core.BookingApp.company.domain;
using BookingApiRest.core.BookingApp.policy.domain;
using BookingApiRest.core.BookingApp.policy.infrastructure;
using BookingApiRest.Core.Shared.Domain;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApiRest.Test.policy;
public class InMemoryPolicyRepositoryCheckShould
{
    private InMemoryPolicyRepository _inMemoryPolicyRepository;
    private string employeeId;

    [SetUp]
    public void SetUp()
    {
        _inMemoryPolicyRepository = new InMemoryPolicyRepository();
        employeeId = Guid.NewGuid().ToString();
        var policy = new Policy(employeeId, RoomType.Standard);
        _inMemoryPolicyRepository.Save(PolicyType.Employee, policy);
    }

    [Test]
    public void check_if_an_employee_policy_exists()
    {
        var result = _inMemoryPolicyRepository.EmployeePolicyExists(employeeId);

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
        var roomPolicy = RoomType.Standard;

        var result = _inMemoryPolicyRepository.CheckEmployeePolicy(employeeId, roomPolicy);

        result.ShouldBeTrue();
    }

    [Test]
    public void check_if_employee_policy_is_not_correct()
    {
        var roomPolicy = RoomType.Suite;

        var result = _inMemoryPolicyRepository.CheckEmployeePolicy(employeeId, roomPolicy);

        result.ShouldBeFalse();
    }
}

