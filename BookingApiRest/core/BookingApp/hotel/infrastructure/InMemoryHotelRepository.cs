using System.Collections.Generic;
using System.Linq;
using BookingApiRest.core.shared.exceptions;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApp.Hotel.Application.Ports;

namespace BookingApiRest.Infrastructure.Repositories;
public class InMemoryHotelRepository : HotelRepository {

    internal readonly List<Hotel> _hotels = new List<Hotel>();

    public void Create(Hotel hotel) {
        try {

            _hotels.Add(hotel);
            Console.WriteLine($"Hoteles {_hotels.ToString()}");

        } catch (Exception e) {
            throw new Exception("Error adding hotel", e);
        }
    }

    public void Update(Hotel updatableHotel)
    {
        try
        {
            var index = _hotels.FindIndex(hotel => hotel.Id == updatableHotel.Id);
            _hotels[index] = updatableHotel;
        }
        catch (NotFoundException e)
        {
            throw new NotFoundException(e.Message, e);
        }
    }

    public Hotel? GetById(string id) {
        return _hotels.FirstOrDefault(hotel => hotel.Id == id);
    }

    public bool Exists(string id) {
        return _hotels.Any(hotel => hotel.Id == id);
    }
}
