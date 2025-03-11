namespace BookingApiRest.core.BookingApp.company.domain;
public class Employee
{
    public string Id { get; }
    public Employee(string employeeId)
    {
        Id = employeeId;
    }
}

