using BookingApiRest.core.BookingApp.policy.application;
using BookingApiRest.core.BookingApp.policy.application.requests;
using BookingApiRest.core.shared.domain;
using System.ComponentModel.Design;

namespace BookingApiRest.core.BookingApp.booking.application.requests
{
    
    public class IsBookingAllowRequest : DomainEvent
    {
        public IsBookingAllowRequest(string aggregateId, string roomType) : base(aggregateId, "is-booking-allow")
        {
            this.AddToPayload("roomType", roomType);
        }

        public static IsBookingAllowRequest From(string employeeId, string roomType)
        {
            return new IsBookingAllowRequest(employeeId, roomType);
        }
    }
}
