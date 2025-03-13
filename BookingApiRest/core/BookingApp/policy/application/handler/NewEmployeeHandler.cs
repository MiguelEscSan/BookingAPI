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
   public void Handle(DomainEvent domainEvent)
    {
        var employeeId = domainEvent.GetAggregateId();
        var companyId = domainEvent.GetPayload()["companyId"];

        var employeePolicy = new Policy(employeeId, RoomType.All);
        var companyPolicy = new Policy(companyId, RoomType.All);

        this._policyRepository.Save(PolicyType.Employee, employeePolicy);
        this._policyRepository.Save(PolicyType.Company, companyPolicy);
    }

    public string GetEventId()
    {
        return "new-employee";
    }
}
