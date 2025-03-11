
using BookingApiRest.core.BookingApp.policy.application;
using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.BookingApp.policy.domain;
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

        _policyRepository.Received().Save(companyId, Arg.Is<Policy>(policy => policy.RoomType == roomType && policy.PolicyType == PolicyType.Company));
    }
}

