using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.hotel.controller.DTO.request;
public class SetHotelRoomsDTO {
    public int RoomNumber { get; set; }
    public string RoomType { get; set; }

    public RoomType GetRoomTypeEnum()
    {
        if (Enum.TryParse<RoomType>(RoomType, true, out var result))
        {
            return result;
        }
        else
        {
            throw new ArgumentException($"Invalid room type: {RoomType}");
        }
    }
}

