namespace BookingApiRest.core.shared.domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RequestDomainEvent<TResponse> : DomainEvent
{
    private TaskCompletionSource<TResponse> _tcs = new TaskCompletionSource<TResponse>();

    public RequestDomainEvent(string aggregateId, string eventId) : base(aggregateId, eventId)
    {
    }

    public Task<TResponse> GetResponseTask() => _tcs.Task;

    public void SetResponse(TResponse response)
    {
        _tcs.TrySetResult(response);
    }

    public void SetException(Exception exception)
    {
        _tcs.TrySetException(exception);
    }
}
