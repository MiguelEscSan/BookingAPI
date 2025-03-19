namespace BookingApiRest.core.shared.exceptions
{
    public class RoomsFullyBookedException : Exception
    {
        public RoomsFullyBookedException() : base("The hotel capacity is fully booked") { }

        public RoomsFullyBookedException(string message) : base(message) { }

        public RoomsFullyBookedException(string message, Exception innerException) : base(message, innerException) { }

    }
}
