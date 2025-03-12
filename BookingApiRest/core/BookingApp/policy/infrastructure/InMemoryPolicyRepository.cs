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
        return _policies[PolicyType.Employee].ContainsKey(employeeId);
    }

    public bool CheckEmployeePolicy(string employeeId, RoomType roomType)
    {
        return _policies[PolicyType.Employee][employeeId].RoomType == roomType;
    }
}

