using BookingApiRest.core.BookingApp.company.domain;
using BookingApiRest.core.BookingApp.policy.application.DTO;
using BookingApiRest.core.BookingApp.policy.domain;
using BookingApiRest.Core.Shared.Domain;

namespace BookingApiRest.core.BookingApp.policy.infrastructure;
public class InMemoryPolicyRepository : PolicyRepository
{
    internal readonly Dictionary<string, Policy> _employeesPolicies = new Dictionary<string, Policy>();
    internal readonly Dictionary<string, Policy> _companiesPolices = new Dictionary<string, Policy>();

    public void Save(PolicyType policyType, Policy policy)
    {
        if (policyType == PolicyType.Employee)
        {
            _employeesPolicies[policy.Id] = policy;
        }
        else if (policyType == PolicyType.Company)
        {
            _companiesPolices[policy.Id] = policy;
        }
    }

    public bool EmployeePolicyExists(string employeeId)
    {
        return _employeesPolicies.ContainsKey(employeeId);
    }

    public bool IsEmployeePolicyDefault(string employeeId)
    {
        return _employeesPolicies[employeeId].RoomType == RoomType.All;
    }

    public bool CheckEmployeePolicy(string employeeId, RoomType roomType)
    {
        RoomType employeePolicyRoomType = _employeesPolicies[employeeId].RoomType;
        return employeePolicyRoomType == roomType;
    }

    public bool CheckCompanyPolicy(string companyId, RoomType roomType)
    {

        RoomType companyPolicyRoomType = _companiesPolices[companyId].RoomType;
        return companyPolicyRoomType == roomType || companyPolicyRoomType == RoomType.All;
    }

    public void Delete(string employeeId)
    {
        _employeesPolicies.Remove(employeeId);
    }
}

