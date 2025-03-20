using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.BookingApp.policy.domain;
using BookingApiRest.core.shared.application;
using BookingApiRest.core.shared.domain;
using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.policy.application.handler;

public class NewEmployeeHandler : IEventHandler
{

    private readonly PolicyRepository _policyRepository;
    public NewEmployeeHandler(PolicyRepository policyRepository)
    {
        this._policyRepository = policyRepository;
    }
   public async Task<Result<object>> Handle(DomainEvent domainEvent)
   {
        string employeeId = domainEvent.GetAggregateId();
        string companyId = domainEvent.GetPayload()["companyId"];

        Policy employeePolicy = new Policy(employeeId, RoomType.All);
        Policy companyPolicy = new Policy(companyId, RoomType.All);

        this._policyRepository.Save(PolicyType.Employee, employeePolicy);
        this._policyRepository.Save(PolicyType.Company, companyPolicy);

        return new Result<object>("Policies created successfully", true);
    }

    public string GetEventId()
    {
        return "new-employee";
    }
}
