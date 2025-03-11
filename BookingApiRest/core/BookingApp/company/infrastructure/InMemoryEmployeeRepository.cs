using BookingApiRest.core.BookingApp.company.application.ports;
using BookingApiRest.core.BookingApp.company.domain;

namespace BookingApiRest.core.BookingApp.company.infrastructure;
public class InMemoryEmployeeRepository : EmployeeRepository
{
    internal readonly List<Employee> _employees = new List<Employee>();

    public void Save(Employee employee)
    {
        _employees.Add(employee);
    }

    public void Delete(string id)
    {
        _employees.RemoveAll(employee => employee.Id == id);
    }

    public bool Exists(string id)
    {
        return _employees.Any(employee => employee.Id == id);
    }    
}

