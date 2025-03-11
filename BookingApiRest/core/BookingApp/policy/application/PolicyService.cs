using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.BookingApp.policy.domain;
using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.policy.application;
public class PolicyService 
{
    private readonly PolicyRepository _policyRepository;
    public PolicyService(PolicyRepository policyRepository)
    {
        _policyRepository = policyRepository;
    }
    public void SetCompanyPolicy(string companyId, RoomType roomType)
    {
        var policy = new Policy(companyId, roomType, PolicyType.Company);
        _policyRepository.Save(companyId, policy);
    }
}

