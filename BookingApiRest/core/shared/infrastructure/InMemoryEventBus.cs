using BookingApiRest.core.shared.application;
using BookingApiRest.core.shared.domain;
using System;

namespace BookingApiRest.core.shared.infrastructure
{
    public class InMemoryEventBus : EventBus
    {
        private readonly Dictionary<string, List<IEventHandler>> _handlers = new Dictionary<string, List<IEventHandler>>() ;

        public void Publish(List<DomainEvent> events)
        {
            foreach (var domainEvent in events)
            {
                if (_handlers.TryGetValue(domainEvent.GetEventId(), out var eventHandlers))
                {
                    foreach (var handler in eventHandlers)
                    {
                        handler.Handle(domainEvent);
                    }
                }
            }
        }

        public async Task<Result<TResponse>> PublishAndWait<TRequest, TResponse>(TRequest request)
            where TRequest : DomainEvent
            where TResponse : class
        {

            if (_handlers.TryGetValue(request.GetEventId(), out var eventHandlers))
            {
                foreach (var handler in eventHandlers)
                {
                    if (handler is IEventHandler typedHandler)
                    {
                        var result = await typedHandler.Handle(request);
                        return new Result<TResponse>(result.Value as TResponse, result.IsSuccess);
                    }
                }
            }
            return new Result<TResponse>(null, false);
        }


        public void Subscribe(IEventHandler handler)
        {
            var eventId = handler.GetEventId();
            if (!_handlers.ContainsKey(eventId))
            {
                _handlers[eventId] = new List<IEventHandler>();
            }
            _handlers[eventId].Add(handler);
        }

    }
}

