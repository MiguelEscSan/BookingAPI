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
        var policy = new Policy(companyId, roomType);
        _policyRepository.Save(PolicyType.Company, policy);
    }

    public void SetEmployeePolicy(string employeeId, RoomType roomType)
    {
        var policy = new Policy(employeeId, roomType);
        _policyRepository.Save(PolicyType.Employee, policy);
    }
}

