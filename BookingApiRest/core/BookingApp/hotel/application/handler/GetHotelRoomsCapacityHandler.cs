﻿using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.shared.application;
using BookingApiRest.core.shared.domain;
using BookingApiRest.Core.Shared.Domain;
using BookingApp.Hotel.Application.Ports;

namespace BookingApiRest.core.BookingApp.hotel.application.handler
{
    public class GetHotelRoomsCapacityHandler : IEventHandler
    {
        private readonly HotelService _hotelService;

        public GetHotelRoomsCapacityHandler(HotelService hotelService)
        {
            _hotelService = hotelService;
        }

        public string GetEventId()
        {
            return "hotel-rooms-capacity";
        }

        public async Task<Result<object>> Handle(DomainEvent domainEvent)
        {
            var hotelId = domainEvent.GetAggregateId();
            var roomType = domainEvent.GetPayload()["roomType"];
            var parsedRoomType = Enum.Parse<RoomType>(roomType);

            var roomsCapacity = _hotelService.GetHotelRoomCapacity(hotelId, parsedRoomType);

            if (roomsCapacity < 0)
            {
                return new Result<object>("Invalid capacity", false);
                
            }
            return new Result<object>(roomsCapacity, true);
        }
    }
}
