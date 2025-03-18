using BookingApiRest.core.shared.domain;

namespace BookingApiRest.core.BookingApp.company.application.ports.requests
{
    public class GetCompanyIdByEmployeeIdRequest : DomainEvent
    {
        public GetCompanyIdByEmployeeIdRequest(string aggregateId) : base(aggregateId, "get-employee-company")
        {
        }

        public static GetCompanyIdByEmployeeIdRequest From(string employeeId)
        {
            return new GetCompanyIdByEmployeeIdRequest(employeeId);
        }
    }
}

