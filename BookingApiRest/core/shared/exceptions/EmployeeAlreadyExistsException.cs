namespace BookingApiRest.core.shared.exceptions
{
    public class EmployeeAlreadyExistsException : Exception
    {
        public EmployeeAlreadyExistsException() : base("The employee already exists") { }

        public EmployeeAlreadyExistsException(string message) : base(message) { }

        public EmployeeAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }

    }
}
