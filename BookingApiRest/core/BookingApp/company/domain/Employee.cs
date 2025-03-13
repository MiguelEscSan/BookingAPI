using BookingApiRest.core.BookingApp.company.domain.events;
using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.company.domain;
public class Employee : AggregateRoot
{
    public string Id { get; }
    public string CompanyId { get; set; }

    public Employee(string id, string companyId) : base(id)
    {
        this.Id = id;
        this.CompanyId = companyId;
        this.RecordEvent(NewEmployee.From(this));
        
    }

    public void Delete()
    {
        this.RecordEvent(DeleteEmployee.From(this));
    }

}

