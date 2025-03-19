using BookingApiRest.core.shared.application;
using BookingApiRest.core.shared.domain;

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

        public Task<Result<TResponse>> PublishAndWait<TRequest, TResponse>(TRequest request)
            where TRequest : class
            where TResponse : class
        {
            throw new NotImplementedException();
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

