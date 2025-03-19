using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.BookingApp.policy.domain;
using BookingApiRest.core.shared.application;
using BookingApiRest.core.shared.domain;
using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.policy.application.handler
{
    public class DeleteEmployeeHandler : IEventHandler
    {
        private readonly PolicyRepository _policyRepository;

        public DeleteEmployeeHandler(PolicyRepository policyRepository)
        {
            this._policyRepository = policyRepository;
        }

        public async Task<Result<object>> Handle(DomainEvent domainEvent)
        {
            var employeeId = domainEvent.GetAggregateId();

            this._policyRepository.Delete(employeeId);

            var resultString = new Result<string>("Employee policies deleted successfully", true);

            return new Result<object>(resultString.Value, resultString.IsSuccess);
        }

        public string GetEventId()
        {
            return "delete-employee";
        }

        
    }
}
