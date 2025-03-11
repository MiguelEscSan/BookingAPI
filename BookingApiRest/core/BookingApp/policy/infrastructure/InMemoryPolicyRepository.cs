using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.BookingApp.policy.domain;

namespace BookingApiRest.core.BookingApp.policy.infrastructure;
public class InMemoryPolicyRepository : PolicyRepository
{
    internal readonly Dictionary<PolicyType, List<Policy>> _policies = new Dictionary<PolicyType, List<Policy>>();
    public void Save(PolicyType policyType, Policy policy)
    {
        if(_policies.ContainsKey(policyType) is false)
        {
            _policies[policyType] = new List<Policy>();
        }
        _policies[policyType].Add(policy);
    }
}

