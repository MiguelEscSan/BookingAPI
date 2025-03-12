using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.BookingApp.policy.domain;

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

    public void Update(PolicyType policyType, Policy policy)
    {
        
    }   
}

