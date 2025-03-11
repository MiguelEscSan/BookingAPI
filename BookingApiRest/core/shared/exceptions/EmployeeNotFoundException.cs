namespace BookingApiRest.core.shared.exceptions
{
    public class EmployeeNotFoundException: Exception
    {
        public EmployeeNotFoundException() : base("Employe has not been found") { }

        public EmployeeNotFoundException(string message) : base(message) { }

        public EmployeeNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

}
