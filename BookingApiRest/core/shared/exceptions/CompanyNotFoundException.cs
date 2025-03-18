namespace BookingApiRest.core.shared.exceptions
{
    public class CompanyNotFoundException : Exception
    {
        public CompanyNotFoundException() : base("The company doesnt exist") { }

        public CompanyNotFoundException(string message) : base(message) { }

        public CompanyNotFoundException(string message, Exception innerException) : base(message, innerException) { }

    }
}
