using BookingApiRest.core.shared.domain;

namespace BookingApiRest.core.shared.application
{
    public interface EventBus
    {
        void Publish(List<DomainEvent> events);
        void Subscribe(IEventHandler handler);

        Task<Result<TResponse>> PublishAndWait<TRequest, TResponse>(TRequest request)
            where TRequest : DomainEvent
            where TResponse : class;
    }
}
