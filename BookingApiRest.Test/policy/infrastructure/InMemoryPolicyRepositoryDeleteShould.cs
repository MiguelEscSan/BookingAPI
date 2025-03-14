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
public class InMemoryPolicyRepositoryDeleteShould
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
    public void delete_employee_policy()
    {
        _inMemoryPolicyRepository.Delete(employeeId);

        _inMemoryPolicyRepository._employeesPolicies.ContainsKey(employeeId).ShouldBeFalse();
    }
}

