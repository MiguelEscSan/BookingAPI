

using BookingApiRest.core.BookingApp.hotel.application;
using BookingApiRest.core.BookingApp.hotel.controller.DTO;
using BookingApiRest.core.shared.exceptions;
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

    public HotelController(HotelService hotelService) {
        _hotelService = hotelService;
    }

    [HttpPost]
    public IActionResult CreateHotel([FromBody] CreateHotelDTO request) {
        try {
            Hotel hotel = new Hotel(request.Id, request.Name);
            _hotelService.AddHotel(request.Id, request.Name);
            return Ok(hotel);
        } catch (ConflictException e) {
            return Conflict(e.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetHotel(string id)
    {
        try
        {
            Hotel hotel = _hotelService.findHotelBy(id);
            return Ok(hotel);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);

        }
    }

}
