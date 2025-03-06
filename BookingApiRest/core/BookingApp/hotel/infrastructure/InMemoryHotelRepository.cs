using System.Collections.Generic;
using System.Linq;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApp.Hotel.Application.Ports;

namespace BookingApiRest.Infrastructure.Repositories;
public class InMemoryHotelRepository : HotelRepository {
    private readonly List<Hotel> _hotels = new List<Hotel>();

    public void Create(Hotel hotel)
    {
        throw new NotImplementedException();
    }

    public Hotel GetById(string id)
    {
        throw new NotImplementedException();
    }
}
