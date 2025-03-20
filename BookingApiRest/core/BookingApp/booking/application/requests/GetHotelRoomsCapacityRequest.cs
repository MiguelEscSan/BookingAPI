using BookingApiRest.core.BookingApp.policy.application;
using BookingApiRest.core.shared.domain;

namespace BookingApiRest.core.BookingApp.booking.application.requests
{
    public class GetHotelRoomsCapacityRequest : DomainEvent
    {
        public GetHotelRoomsCapacityRequest(string aggregateId, string roomType) : base(aggregateId, "hotel-rooms-capacity")
        {
            this.AddToPayload("roomType", roomType);
        }

        public static GetHotelRoomsCapacityRequest From(string hotelId, string roomType)
        {
            return new GetHotelRoomsCapacityRequest(hotelId, roomType);
        }
    }
}

