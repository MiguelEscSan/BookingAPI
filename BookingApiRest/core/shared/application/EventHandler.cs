using BookingApiRest.core.shared.domain;

namespace BookingApiRest.core.shared.application
{
    public interface IEventHandler
    {
        Task<Result<object>> Handle(DomainEvent domainEvent);
        string GetEventId();
    }

}
