using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.BookingApp.policy.domain;
using BookingApiRest.core.shared.application;
using BookingApiRest.core.shared.domain;
using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.company.application.handler
{
    public class GetCompanyIdByEmployeeIdHandler : IEventHandler
    {
        private readonly CompanyRepository _companyRepository;

        public GetCompanyIdByEmployeeIdHandler(CompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public string GetEventId()
        {
            return "get-employee-company";
        }

        public async Task<Result<object>> Handle(DomainEvent domainEvent)
        {
            string employeeId = domainEvent.GetAggregateId();
            string companyId = _companyRepository.GetCompanyIdByEmployeeId(employeeId);

            return new Result<object>(companyId ?? "Company not found", companyId is not null);
        }
    }
}
