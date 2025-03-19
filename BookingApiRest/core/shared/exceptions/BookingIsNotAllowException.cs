namespace BookingApiRest.core.shared.exceptions
{
    public class BookingIsNotAllowException : Exception
    {
        public BookingIsNotAllowException() : base("Booking is not allow") { }

        public BookingIsNotAllowException(string message) : base(message) { }

        public BookingIsNotAllowException(string message, Exception innerException) : base(message, innerException) { }

    }
}
