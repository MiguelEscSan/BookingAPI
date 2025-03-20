using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.shared.application;
using BookingApiRest.core.shared.application.results;
using BookingApiRest.core.shared.domain;
using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.policy.application.handler
{
    public class IsBookingAllowHandler : IEventHandler
    {
        private readonly PolicyService _policyService;

        public IsBookingAllowHandler(PolicyService policeService)
        {
            _policyService = policeService;
        }

        public string GetEventId()
        {
            return "is-booking-allow";
        }

        public async Task<Result<object>> Handle(DomainEvent domainEvent)
        {
            string employeeId = domainEvent.GetAggregateId();
            string roomType = domainEvent.GetPayload()["roomType"];
            RoomType parsedRoomType = Enum.Parse<RoomType>(roomType);

            Result<BooleanResult> result = await _policyService.IsBookingAllowed(employeeId, parsedRoomType);

            if (!result.IsSuccess)
            {
                return new Result<object>(result.GetErrorMessage(), false);
                
            }

            return new Result<object>(result.GetValue(), true); 
        }
    }
}
