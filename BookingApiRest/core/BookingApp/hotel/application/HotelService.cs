using BookingApiRest.core.shared.exceptions;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApiRest.Core.Shared.Domain;
using BookingApp.Hotel.Application.Ports;

namespace BookingApiRest.core.BookingApp.hotel.application;
public class HotelService {

    private readonly HotelRepository _hotelRepository;

    public HotelService(HotelRepository hotelRepository) {
        _hotelRepository = hotelRepository;
    }


    public void AddHotel(string HotelId, string HotelName) {
        try
        {
            if (_hotelRepository.Exists(HotelId))
            {
                throw new ConflictException("Hotel ID already exists");
            }

            var newHotel = new Hotel(HotelId, HotelName);

            _hotelRepository.Create(newHotel);
        }
        catch (ConflictException e)
        {
            throw e;
        }
    }

    public Hotel findHotelBy(string id)
    {
        try
        {
            var hotel = _hotelRepository.GetById(id);
            if (hotel == null)
            {
                throw new NotFoundException($"Hotel with id {id} not found");
            }
            return hotel;
        }
        catch (NotFoundException e)
        {
            throw e;
        }
    }

    public void setRoom(string hotelId, int roomNumber, RoomType roomType)
    {
        var hotel = findHotelBy(hotelId);
        hotel.AddRoom(roomNumber, roomType);
        _hotelRepository.Update(hotel);
    }
}