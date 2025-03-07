using BookingApiRest.core.shared.exceptions;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApp.Hotel.Application.Ports;

namespace BookingApiRest.core.BookingApp.hotel.application;
public class HotelService {

    private readonly HotelRepository _hotelRepository;

    public HotelService(HotelRepository hotelRepository) {
        _hotelRepository = hotelRepository;
    }


    public void AddHotel(string HotelId, string HotelName) {
        try {

            if (_hotelRepository.Exists(HotelId)) {
                throw new ConflictException("Hotel ID already exists");
            }

            var newHotel = new Hotel(HotelId, HotelName);

            _hotelRepository.Create(newHotel);
        } catch (Exception error) {
            throw new Exception("Cannot create a hotel", error);
        }
    }
}