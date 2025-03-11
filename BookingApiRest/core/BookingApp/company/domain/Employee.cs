namespace BookingApiRest.core.BookingApp.company.domain;
public class Employee
{
    public string Id { get; }
    public string CompanyId { get; }
    public Employee(string companyId, string employeeId)
    {
        Id = employeeId;
        CompanyId = companyId;
    }
}

