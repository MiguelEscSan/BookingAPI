using BookingApp.Hotel.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Hotel.Controllers;

[ApiController]
[Route("api/hotel")]
public class HotelController : ControllerBase {

    [HttpPost]
    public void CreateHotel(HttpRequest request, HttpResponse response) {
        
        
    }

}
