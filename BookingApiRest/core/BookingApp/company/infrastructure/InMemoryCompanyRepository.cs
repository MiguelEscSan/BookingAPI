using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.BookingApp.company.domain;

namespace BookingApiRest.core.BookingApp.company.infrastructure;
public class InMemoryCompanyRepository : CompanyRepository
{
    internal readonly Dictionary<string, List<Employee>> _companies = new Dictionary<string, List<Employee>>();

    public void Save(string companyId, Employee employee)
    {
        if (_companies.ContainsKey(companyId) is false)
        {
            _companies[companyId] = new List<Employee>();
        }
        _companies[companyId].Add(employee);
    }

    public void Delete(string id)
    {
        _companies.Values.ToList().ForEach(employees => employees.RemoveAll(employee => employee.Id == id));
    }

    public bool Exists(string id)
    {
        return _companies.Values.SelectMany(employees => employees)
                                        .Any(employee => employee.Id == id);
    }

}

