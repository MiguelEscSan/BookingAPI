namespace BookingApiRest.core.shared.exceptions
{
    public class HotelHasNotBeenFound : Exception {
        public HotelHasNotBeenFound() : base("The requested resource was not found.") { }

        public HotelHasNotBeenFound(string message) : base(message) { }

        public HotelHasNotBeenFound(string message, Exception innerException) : base(message, innerException) { }

    }
}
