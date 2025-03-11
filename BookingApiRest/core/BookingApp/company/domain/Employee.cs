namespace BookingApiRest.core.BookingApp.company.domain;
public class Employee
{
    public string Id { get; }
    public string CompanyId { get; }
    public Employee(string employeeId, string companyId)
    {
        Id = employeeId;
        CompanyId = companyId;
    }
}

