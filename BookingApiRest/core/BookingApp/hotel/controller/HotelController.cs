

using BookingApiRest.core.BookingApp.hotel.application;
using BookingApiRest.core.BookingApp.hotel.controller.DTO;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApiRest.Infrastructure.Repositories;
using BookingApp.Hotel.Application.Ports;
using Microsoft.AspNetCore.Mvc;

namespace BookingApiRest.Controllers;

[ApiController]
[Route("api/hotel")]
public class HotelController : ControllerBase {

    private readonly HotelRepository _hotelRepository;
    private readonly HotelService _hotelService;

    public HotelController() {
        _hotelRepository = new InMemoryHotelRepository();
        _hotelService = new HotelService(_hotelRepository);
    }

    [HttpPost]
    public IActionResult CreateHotel([FromBody] CreateHotelDTO request) {
        try {
            Hotel hotel = new Hotel(request.Id, request.Name);
            _hotelService.AddHotel(request.Id, request.Name);
            return Ok(hotel);
        } catch (Exception e) {
            throw new Exception("Error creating hotel", e);
        }
    }

}
