
using BookingApiRest;
using BookingApiRest.Core.BookingApp.Hotel.Domain;
using BookingApp.Hotel.Application.DTO;

public class CreateHotelCommand : Command<CreateHotelDTO, HotelDTO> {

    private HotelRepository hotelRepository;

    public CreateHotelCommand(HotelRepository hotelRepository) {
        this.hotelRepository = hotelRepository;
    }

    public Result<HotelDTO> Run(CreateHotelDTO request) {
        try {
            var hotel = new Hotel(request.UID, request.HotelName);
            hotelRepository.Save(hotel);
            return Result<HotelDTO>.Success(new HotelDTO {
                UID = hotel.Id,
                HotelName = hotel.Name
            });

        } catch (Exception error) {
            return Result<HotelDTO>.Fail(error);
        }
    }

}