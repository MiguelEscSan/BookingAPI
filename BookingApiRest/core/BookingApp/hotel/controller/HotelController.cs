

using BookingApiRest.core.BookingApp.hotel.application;
using BookingApiRest.core.BookingApp.hotel.controller.DTO.request;
using BookingApiRest.core.BookingApp.hotel.controller.DTO.response;
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
            _hotelService.AddHotel(request.Id, request.Name);
            return Ok();
        } catch (HotelAlreadyExistsException e) {
            return Conflict(e.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetHotel(string id)
    {
        try
        {
            Hotel hotel = _hotelService.findHotelBy(id);
            var hotelDto = new HotelDTO(hotel.Id, hotel.Name, hotel.Rooms);
            
            return Ok(hotelDto);
        }
        catch (HotelHasNotBeenFound e)
        {
            return NotFound(e.Message);

        }
    }

    [HttpPut("{id}/rooms")]
    public IActionResult SetRoom([FromBody] SetHotelRoomsDTO request, string id)
    {
        try
        {
            _hotelService.setRoom(id, request.RoomNumber, request.GetRoomTypeEnum());
            return Ok();
        }
        catch (HotelHasNotBeenFound e)
        {
            return NotFound(e.Message);
        }
    }

}
