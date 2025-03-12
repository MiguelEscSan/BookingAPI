namespace BookingApiRest.core.shared.domain;
using System;
using System.Collections.Generic;

public class DomainEvent
{
    public string EventId { get; }
    public string AggregateId { get; }
    public string OccurredOn { get; }
    private readonly Dictionary<string, string> _payload;

    public DomainEvent(string aggregateId, string eventId)
    {
        AggregateId = aggregateId;
        EventId = eventId;
        _payload = new Dictionary<string, string>();
        OccurredOn = DateTime.Now.ToString("yyyy-MM-dd");
    }

    public string GetAggregateId()
    {
        return AggregateId;
    }

    public string GetEventId()
    {
        return EventId;
    }

    public string GetOccurredOn()
    {
        return OccurredOn;
    }

    public Dictionary<string, string> GetPayload()
    {
        return _payload;
    }
    public void AddToPayload(string key, string value)
    {
        _payload[key] = value;
    }
}


