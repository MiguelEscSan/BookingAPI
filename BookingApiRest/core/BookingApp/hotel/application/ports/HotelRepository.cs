namespace BookingApp.Hotel.Application.Ports;
using BookingApiRest.Core.BookingApp.Hotel.Domain;

public interface HotelRepository {
    void Save(Hotel hotel);
    void Update(Hotel hotel);
    Hotel? GetById(string id);

    bool Exists(string id);
}
