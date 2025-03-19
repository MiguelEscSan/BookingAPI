using BookingApiRest.core.shared.domain;

namespace BookingApiRest.core.shared.application
{
    public interface EventBus
    {
        void Publish(List<DomainEvent> events);
        void Subscribe(IEventHandler handler);

        // Aseguramos que PublishAndWait esté utilizando correctamente los tipos genéricos
        Task<Result<TResponse>> PublishAndWait<TRequest, TResponse>(TRequest request)
            where TRequest : RequestDomainEvent<TResponse> // Aseguramos que TRequest extienda RequestDomainEvent<TResponse>
            where TResponse : class;
    }
}
