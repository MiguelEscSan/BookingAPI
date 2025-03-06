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
            if(_hotelRepository.GetById(HotelId) != null) {
                throw new Exception("Hotel already exists");
            }

            var newHotel = new Hotel(HotelId, HotelName);

            _hotelRepository.Create(newHotel);
        } catch (Exception e) {
            throw new Exception("Error adding hotel", e);
        }
    }
}