using BookingApiRest.core.shared.domain;

namespace BookingApiRest.core.BookingApp.company.domain.events
{
    public class NewEmployee : DomainEvent
    {
        public NewEmployee(string aggregateId) : base(aggregateId, "new-employee")
        {
            
        }

        public static NewEmployee From(Employee employee)
        {
            return new NewEmployee(employee.Id);
        }
    }
}
