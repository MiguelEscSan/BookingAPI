using BookingApiRest.core.shared.domain;

namespace BookingApiRest.core.BookingApp.company.domain.events
{
    public class DeleteEmployee : DomainEvent
    {
        public DeleteEmployee(string aggregateId) : base(aggregateId, "delete-employee")
        {

        }
        public static DeleteEmployee From(Employee employee)
        {
            return new DeleteEmployee(employee.Id);
        }
    }
}
