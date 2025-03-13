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

namespace BookingApiRest.Test.policy.infrastructure;
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

    [TestCase(RoomType.Standard, RoomType.Standard)]
    [TestCase(RoomType.All, RoomType.Standard)]
    public void check_if_employee_policy_is_correct(RoomType employeePolicyRoomType, RoomType bookingPolicy)
    {

        var result = _inMemoryPolicyRepository.CheckEmployeePolicy(employeeId, bookingPolicy);

        result.ShouldBeTrue();
    }

    [Test]
    public void check_if_employee_policy_is_not_correct()
    {
        var result = _inMemoryPolicyRepository.CheckEmployeePolicy(employeeId, RoomType.Suite);

        result.ShouldBeFalse();
    }
}

