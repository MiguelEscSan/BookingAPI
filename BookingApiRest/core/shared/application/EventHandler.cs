using BookingApiRest.core.shared.domain;

namespace BookingApiRest.core.shared.application
{
    public interface IEventHandler
    {
        void Handle(DomainEvent domainEvent);
        string GetEventId();
    }
}
