using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.BookingApp.policy.application.requests;
using BookingApiRest.core.BookingApp.policy.domain;
using BookingApiRest.core.shared.application;
using BookingApiRest.core.shared.exceptions;
using BookingApiRest.Core.Shared.Domain;
using System.Threading.Tasks;

namespace BookingApiRest.core.BookingApp.policy.application;
public class PolicyService 
{
    private readonly PolicyRepository _policyRepository;
    private readonly EventBus _eventBus;
    //private readonly CompanyRepository _companyRepository;

    public PolicyService(PolicyRepository policyRepository, EventBus eventBus)
    {
        _policyRepository = policyRepository;
        _eventBus = eventBus;
        //_companyRepository = companyRepository;
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

    public async Task<Result<BooleanResult>> IsBookingAllowed(string employeeId, RoomType roomType)
    {
        if (!_policyRepository.EmployeePolicyExists(employeeId))
        {
            return Result<BooleanResult>.Fail(new EmployeeNotFoundException($"Employee with id {employeeId} not found"));
        }

        if (_policyRepository.IsEmployeePolicyDefault(employeeId))
        {
            var companyIdResponse = await _eventBus.PublishAndWait<GetCompanyIdByEmployeeIdRequest, string>(
                new GetCompanyIdByEmployeeIdRequest(employeeId)
            );

            if (!companyIdResponse.IsSuccess)
            {
                return Result<BooleanResult>.Fail(new CompanyNotFoundException($"Company not found for employee with id {employeeId}"));
            }

            var companyId = companyIdResponse.Value;

            var isCompanyPolicyAllowed = _policyRepository.CheckCompanyPolicy(companyId, roomType);
            return Result<BooleanResult>.Success(new BooleanResult(isCompanyPolicyAllowed));
        }

        var isEmployeePolicyAllowed = _policyRepository.CheckEmployeePolicy(employeeId, roomType);
        return Result<BooleanResult>.Success(new BooleanResult(isEmployeePolicyAllowed));
    }
}

public class BooleanResult
{
    public bool IsSuccess { get; set; }

    public BooleanResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
}

