using BookingApiRest.core.BookingApp.company.application.ports;
using System.ComponentModel.Design;
using BookingApiRest.core.BookingApp.company.domain;

namespace BookingApiRest.core.BookingApp.company.application;
public class CompanyService {

    private readonly EmployeeRepository _employeeRepository;

    public CompanyService(EmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }   

    public void AddEmployee(string companyId, string employeeId)
    {
        var Employee = new Employee(companyId, employeeId);

        _employeeRepository.Save(Employee);
    }
}

