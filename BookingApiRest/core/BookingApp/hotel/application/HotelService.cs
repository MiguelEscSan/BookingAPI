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

    //public int GetHotelRoomCapacity(string hotelId, RoomType roomType)
    //{
    //    try
    //    {
    //        Hotel hotel = findHotelBy(hotelId);

    //        return hotel.Rooms[roomType];
    //    }
    //    catch (HotelHasNotBeenFound error)
    //    {
    //        throw error;
    //    } 
    //}
    public int GetHotelRoomCapacity(string hotelId, RoomType roomType)
    {
        Result<Hotel> hotelResult = findHotelBy(hotelId);

        if (!hotelResult.IsSuccess)
        {
            throw hotelResult.GetError();
        }

        Hotel hotel = hotelResult.GetValue();
        return hotel.Rooms[roomType];
    }

    public void AddHotel(string HotelId, string HotelName) {
        if (_hotelRepository.Exists(HotelId))
        {
            throw new HotelAlreadyExistsException("Hotel ID already exists");
        }

        Hotel newHotel = new Hotel(HotelId, HotelName);

        _hotelRepository.Save(newHotel);
    }

    //public Hotel findHotelBy(string id)
    //{
    //    Hotel hotel = _hotelRepository.GetById(id);
    //    if (hotel == null)
    //    {
    //        throw new HotelHasNotBeenFound($"Hotel with id {id} not found");
    //    }
    //    return hotel;
    //}
    public Result<Hotel> findHotelBy(string id)
    {
        Hotel hotel = _hotelRepository.GetById(id);
        if (hotel == null)
        {
            return Result<Hotel>.Fail(new HotelHasNotBeenFound());
        }
        return Result<Hotel>.Success(hotel);

    }

    //public void setRoom(string hotelId, int roomNumber, RoomType roomType)
    //{
    //    Hotel hotel = findHotelBy(hotelId);
    //    hotel.SetRoom(roomNumber, roomType);
    //    _hotelRepository.Update(hotel);
    //}
    public void setRoom(string hotelId, int roomNumber, RoomType roomType)
    {
        Result<Hotel> hotelResult = findHotelBy(hotelId);

        if (!hotelResult.IsSuccess)
        {
            throw hotelResult.GetError();
        }

        Hotel hotel = hotelResult.GetValue();
        hotel.SetRoom(roomNumber, roomType);
        _hotelRepository.Update(hotel);
    }
}