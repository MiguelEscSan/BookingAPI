using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.BookingApp.policy.domain;

namespace BookingApiRest.core.BookingApp.policy.infrastructure;
public class InMemoryPolicyRepository : PolicyRepository
{
    internal readonly List<Policy> _policies = new List<Policy>();
    public void Save(Policy policy)
    {
        throw new NotImplementedException();
    }
}

