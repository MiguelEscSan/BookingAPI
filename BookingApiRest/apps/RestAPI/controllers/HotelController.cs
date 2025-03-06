using Microsoft.AspNetCore.Mvc;

namespace BookingApiRest.Controllers;

[ApiController]
[Route("api/hotel")]
public class HotelController : ControllerBase {

    [HttpPost]
    public void CreateHotel(HttpRequest request, HttpResponse response) {
        var command = new CreateHotelCommand();
        var dto = new CreateEventDTO(request.Body);

        var result = command.run(request.Body).then((result) => {
            if (result.isSuccess) {
                response.WriteAsJsonAsync(result);
            } else {
                var error = result.GetError();
                ErrorHandler.HandleError(error, response);
            }
        });
        
    }

}
