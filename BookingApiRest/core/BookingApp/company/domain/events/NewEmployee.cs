using BookingApiRest.core.shared.domain;

namespace BookingApiRest.core.BookingApp.company.domain.events
{
    public class NewEmployee : DomainEvent
    {
        public NewEmployee(string aggregateId, string companyId) : base(aggregateId, "new-employee")
        {
            this.AddToPayload("companyId", companyId);
        }

        public static NewEmployee From(Employee employee)
        {
            return new NewEmployee(employee.Id, employee.CompanyId);
        }
    }
}
