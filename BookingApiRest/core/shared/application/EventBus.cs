using BookingApiRest.core.shared.domain;

namespace BookingApiRest.core.shared.application
{
    public interface EventBus
    {
        void Publish(List<DomainEvent> events);
        void Subscribe(IEventHandler handler);
    }
}
