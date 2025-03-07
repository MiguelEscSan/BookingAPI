namespace BookingApp.Hotel.Application.Ports;
using BookingApiRest.Core.BookingApp.Hotel.Domain;

public interface HotelRepository {
    void Create(Hotel hotel);
    Hotel GetById(string id);

    bool Exists(string id);
}
