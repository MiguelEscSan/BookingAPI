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
            var employeeId = domainEvent.GetAggregateId();

            this._bookingRepository.Delete(employeeId);

            var resultString = new Result<string>("Employee bookings deleted successfully", true);

            return new Result<object>(resultString.Value, resultString.IsSuccess);
        }

        public string GetEventId()
        {
            return "delete-employee";
        }
    }
}
