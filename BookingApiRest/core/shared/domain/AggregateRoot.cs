using BookingApiRest.core.shared.domain;

namespace BookingApiRest.Core.Shared.Domain;
public class AggregateRoot : Entity
{
    private List<DomainEvent> _events = new List<DomainEvent>();

    public AggregateRoot(string id) : base(id)
    {
    }

    public List<DomainEvent> PullDomainEvents()
    {
        var events = new List<DomainEvent>(this._events);
        this._events.Clear(); 
        return events;
    }

    public void RecordEvent(DomainEvent domainEvent)
    {
        this._events.Add(domainEvent); 
    }

    public void ClearEvents()
    {
        this._events.Clear(); 
    }
}

