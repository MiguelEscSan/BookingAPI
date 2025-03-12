using BookingApiRest.core.BookingApp.company.domain.events;
using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.company.domain;
public class Employee : AggregateRoot
{
    public string Id { get; }

    public Employee(string id) : base(id)
    {
        this.Id = id;
        this.RecordEvent(NewEmployee.From(this));
    }

}

