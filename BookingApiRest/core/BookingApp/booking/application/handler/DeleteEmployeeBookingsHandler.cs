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

        public async Task<Result<object>> Handle(DomainEvent domainEvent)
        {
            string employeeId = domainEvent.GetAggregateId();

            this._bookingRepository.Delete(employeeId);

            return new Result<object>("Employee bookings deleted successfully", true);
        }

        public string GetEventId()
        {
            return "delete-employee";
        }
    }
}
