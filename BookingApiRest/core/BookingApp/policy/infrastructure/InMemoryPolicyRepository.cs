using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.BookingApp.policy.domain;
using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.policy.infrastructure;
public class InMemoryPolicyRepository : PolicyRepository
{
    internal readonly Dictionary<PolicyType, Dictionary<string, Policy>> _policies = new Dictionary<PolicyType, Dictionary<string, Policy>>();

    public void Save(PolicyType policyType, Policy policy)
    {
        
        if (_policies.ContainsKey(policyType) is false)
        {
            _policies[policyType] = new Dictionary<string, Policy>();
        }
        _policies[policyType][policy.Id] = policy;
    }

    public bool EmployeePolicyExists(string employeeId)
    {
        return _policies.ContainsKey(PolicyType.Employee) &&
               _policies[PolicyType.Employee].ContainsKey(employeeId);
    }

    public bool IsEmployeePolicyDefault(string employeeId)
    {
        return _policies[PolicyType.Employee][employeeId].RoomType != RoomType.All;
    }

    public bool CheckEmployeePolicy(string employeeId, RoomType roomType)
    {
        var employePolicyRoomType = _policies[PolicyType.Employee][employeeId].RoomType;
        return employePolicyRoomType == roomType || employePolicyRoomType == RoomType.All;
    }

    public bool CheckCompanyPolicy(string companyId, RoomType roomType)
    {
        throw new NotImplementedException();
    }

    public void Delete(string employeeId)
    {
        if (_policies.ContainsKey(PolicyType.Employee))
        {
            _policies[PolicyType.Employee].Remove(employeeId);
        }
    }
}

