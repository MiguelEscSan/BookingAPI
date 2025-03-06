using BookingApp.Hotel.Application.DTO;
using NUnit.Framework;
using Shouldly;

namespace BookingApiRest.Test;

public class HotelCommandShould {

    [Test]
    public void create_a_hotel() {
        var hotelName = "Gloria Palace";
        var uid = Guid.NewGuid();
        CreateHotelDTO createEventDTO = new CreateHotelDTO {
            UID = uid.ToString(), 
            HotelName = hotelName
        };
        var command = new CreateHotelCommand();

        var result = command.run(createEventDTO);
        
        result.isSuccess.ShouldBeTrue();
    }
}