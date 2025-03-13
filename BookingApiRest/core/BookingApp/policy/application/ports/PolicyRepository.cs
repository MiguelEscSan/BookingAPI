using BookingApiRest.core.BookingApp.policy.domain;
using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.policy.application.DTO;
public interface PolicyRepository {
    void Save(PolicyType policyType, Policy policy);

    bool EmployeePolicyExists(string employeeId);

    bool CheckEmployeePolicy(string employeeId, RoomType roomType);

    bool CheckCompanyPolicy(string companyId, RoomType roomType);

    bool IsEmployeePolicyDefault(string employeeId);

    void Delete(string employeeId);
}
