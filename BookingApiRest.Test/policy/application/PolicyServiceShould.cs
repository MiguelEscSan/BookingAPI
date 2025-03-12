﻿
using BookingApiRest.core.BookingApp.policy.application;
using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.BookingApp.policy.domain;
using BookingApiRest.core.shared.exceptions;
using BookingApiRest.Core.Shared.Domain;
using NSubstitute;

namespace BookingApiRest.Test.policy.application;
public class PolicyServiceShould
{
    private PolicyRepository _policyRepository;
    private PolicyService _policyService;

    [SetUp]
    public void SetUp()
    {
        _policyRepository = Substitute.For<PolicyRepository>();
        _policyService = new PolicyService(_policyRepository);
    }

    [Test]
    public void set_a_policy_for_a_company()
    {
        var companyId = Guid.NewGuid().ToString();
        var roomType = RoomType.Standard;

        _policyService.SetCompanyPolicy(companyId, roomType);

        _policyRepository.Received().Save(Arg.Is<PolicyType>(type => type == PolicyType.Company), Arg.Is<Policy>(policy => policy.RoomType == roomType));
    }

    [Test]
    public void update_an_existing_company_policy()
    {
        var companyId = Guid.NewGuid().ToString();
        var roomType = RoomType.Standard;
        var newRoomType = RoomType.Suite;

        _policyService.SetCompanyPolicy(companyId, roomType);
        _policyService.SetCompanyPolicy(companyId, newRoomType);

        _policyRepository.Received().Save(Arg.Is<PolicyType>(type => type == PolicyType.Company), Arg.Is<Policy>(policy => policy.Id == companyId && policy.RoomType == roomType));
        _policyRepository.Received().Save(Arg.Is<PolicyType>(type => type == PolicyType.Company), Arg.Is<Policy>(policy => policy.Id == companyId && policy.RoomType == newRoomType));
    }

    [Test]
    public void set_a_policy_for_an_employee()
    {
        var employeeId = Guid.NewGuid().ToString();
        var roomType = RoomType.Standard;

        _policyService.SetEmployeePolicy(employeeId, roomType);

        _policyRepository.Received().Save(Arg.Is<PolicyType>(type => type == PolicyType.Employee), Arg.Is<Policy>(policy => employeeId == policy.Id && policy.RoomType == roomType));
    }

    [Test]
    public void not_allow_save_a_policy_for_an_employee_that_does_not_exist()
    {
        var employeeId = Guid.NewGuid().ToString();
        var roomType = RoomType.Standard;

        Assert.Throws<EmployeeNotFoundException>(() => _policyService.SetEmployeePolicy(employeeId, roomType));
    }
}

