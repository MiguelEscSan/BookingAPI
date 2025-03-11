namespace BookingApiRest.core.shared.exceptions;
public class HotelAlreadyExistsException : Exception {
    public HotelAlreadyExistsException() : base("A conflict occurred with the current state of the resource.") { }

    public HotelAlreadyExistsException(string message) : base(message) { }

    public HotelAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
}

