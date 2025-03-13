using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.BookingApp.policy.domain;
using BookingApiRest.core.shared.application;
using BookingApiRest.core.shared.exceptions;
using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.policy.application;
public class PolicyService 
{
    private readonly PolicyRepository _policyRepository;
    private readonly EventBus _eventBus;
 
    public PolicyService(PolicyRepository policyRepository, EventBus eventBus)
    {
        _policyRepository = policyRepository;
        _eventBus = eventBus;
    }
    public void SetCompanyPolicy(string companyId, RoomType roomType)
    {
        var policy = new Policy(companyId, roomType);
        _policyRepository.Save(PolicyType.Company, policy);
    }

    public void SetEmployeePolicy(string employeeId, RoomType roomType)
    {
        if(_policyRepository.EmployeePolicyExists(employeeId) is false)
        {
            throw new EmployeeNotFoundException($"Employee with id {employeeId} not found");
        }
        var policy = new Policy(employeeId, roomType);
        _policyRepository.Save(PolicyType.Employee, policy);
    }

    public bool IsBookingAllowed(string employeeId, RoomType roomType)
    {
        if(_policyRepository.EmployeePolicyExists(employeeId) is false)
        {
            throw new EmployeeNotFoundException($"Employee with id {employeeId} not found");
        }

        if(_policyRepository.IsEmployeePolicyDefault(employeeId) is true)
        {
            return _policyRepository.CheckCompanyPolicy(employeeId, roomType);
        }
        return _policyRepository.CheckEmployeePolicy(employeeId, roomType);
        
    }
}

