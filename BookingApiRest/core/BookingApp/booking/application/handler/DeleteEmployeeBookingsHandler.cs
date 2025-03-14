using BookingApiRest.core.BookingApp.booking.application.ports;
using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.shared.application;
using BookingApiRest.core.shared.domain;

namespace BookingApiRest.core.BookingApp.booking.application.handler
{
    public class DeleteEmployeeBookingsHandler : IEventHandler
    {
        private readonly BookingRepository _bookingRepository;

        public DeleteEmployeeBookingsHandler(BookingRepository bookingRepository)
        {
            this._bookingRepository = bookingRepository;
        }

        public void Handle(DomainEvent domainEvent)
        {
            var employeeId = domainEvent.GetAggregateId();

            this._bookingRepository.Delete(employeeId);
        }

        public string GetEventId()
        {
            return "delete-employee";
        }
    }
}
