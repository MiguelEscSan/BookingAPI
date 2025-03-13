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
    private readonly CompanyRepository _companyRepository;

    public PolicyService(PolicyRepository policyRepository, EventBus eventBus, CompanyRepository companyRepository)
    {
        _policyRepository = policyRepository;
        _eventBus = eventBus;
        _companyRepository = companyRepository;
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
            var companyId = _companyRepository.GetCompanyIdByEmployeeId(employeeId);
            return _policyRepository.CheckCompanyPolicy(companyId, roomType);
        }
        return _policyRepository.CheckEmployeePolicy(employeeId, roomType);
        
    }
}

