using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.BookingApp.company.domain;
using BookingApiRest.core.BookingApp.policy.application;
using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.BookingApp.policy.domain;
using BookingApiRest.core.shared.application;
using BookingApiRest.core.shared.exceptions;
using BookingApiRest.Core.Shared.Domain;
using NSubstitute;
using Shouldly;

namespace BookingApiRest.Test.policy;
public class PolicyServiceShould
{
    private PolicyRepository _policyRepository;
    private PolicyService _policyService;
    private CompanyRepository _companyRepository;
    private EventBus _eventBus;

    private string companyId;
    private string employeeId;
    private RoomType roomType;
    private RoomType newRoomType;

    [SetUp]
    public void SetUp()
    {
        _eventBus = Substitute.For<EventBus>();
        _policyRepository = Substitute.For<PolicyRepository>();
        _companyRepository = Substitute.For<CompanyRepository>();

        _policyService = new PolicyService(_policyRepository, _eventBus);

        companyId = Guid.NewGuid().ToString();
        employeeId = Guid.NewGuid().ToString();
        roomType = RoomType.Standard;
        newRoomType = RoomType.Suite;
    }

    [Test]
    public void set_a_policy_for_a_company()
    {
        _policyService.SetCompanyPolicy(companyId, roomType);

        _policyRepository.Received().Save(Arg.Is<PolicyType>(type => type == PolicyType.Company),
                                          Arg.Is<Policy>(policy => policy.RoomType == roomType));
    }

    [Test]
    public void update_an_existing_company_policy()
    {
        _policyService.SetCompanyPolicy(companyId, roomType);
        _policyService.SetCompanyPolicy(companyId, newRoomType);

        _policyRepository.Received().Save(Arg.Is<PolicyType>(type => type == PolicyType.Company),
                                          Arg.Is<Policy>(policy => policy.Id == companyId && policy.RoomType == roomType));

        _policyRepository.Received().Save(Arg.Is<PolicyType>(type => type == PolicyType.Company),
                                          Arg.Is<Policy>(policy => policy.Id == companyId && policy.RoomType == newRoomType));
    }

    [Test]
    public void set_a_policy_for_an_employee()
    {
        _policyRepository.EmployeePolicyExists(employeeId).Returns(true);

        _policyService.SetEmployeePolicy(employeeId, roomType);

        _policyRepository.Received().Save(Arg.Is<PolicyType>(type => type == PolicyType.Employee),
                                          Arg.Is<Policy>(policy => employeeId == policy.Id && policy.RoomType == roomType));
    }

    [Test]
    public void not_allow_save_a_policy_for_an_employee_that_does_not_exist()
    {
        _policyRepository.EmployeePolicyExists(employeeId).Returns(false);

        Assert.Throws<EmployeeNotFoundException>(() => _policyService.SetEmployeePolicy(employeeId, roomType));
    }
}
